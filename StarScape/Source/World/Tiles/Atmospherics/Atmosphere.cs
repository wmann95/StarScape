using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarScape.Source.World.Tiles.Atmospherics
{
	/// <summary>
	/// This class replaces AttAirPressure and will be the method of handling atmospheres and gasses (atleast on tiles) for the future. Every tile made will come with an atmosphere
	/// object attached, which that tile set as the atmosphere's parent.
	/// </summary>
	public class Atmosphere
	{
		public Tile parentTile; // Tile this atmosphere is attached to.
		
		public static readonly float AtmosphericPressure = 101.325f; // Atmospheric Pressure Constant in Kilopascals (KPa)
		static readonly float DecayFactor = 0.05f; // The rate of decay that air undergoes during pressure calculations.

		public float airPressure { get; private set; } // this tiles air pressure.
		private float previousAirPressure { get; set; } // the air pressure of the previous update.
		private bool canChangePressure = true; // Can this tile's atmosphere change pressure? If the tile is solid (like a wall), probably not.
		private bool isTileAtmosphereDirty = false; // Tells the atmosphere that something happened that requires it to update its calculations.

		public Atmosphere(Tile tile)
		{
			parentTile = tile;
			airPressure = AtmosphericPressure;
			previousAirPressure = airPressure;
		}

		public Atmosphere(Tile tile, float initialPressure)
		{
			parentTile = tile;
			airPressure = initialPressure;
			previousAirPressure = airPressure;
		}
		
		/// <summary>
		/// Changes this atmosphere's pressure.
		/// </summary>
		/// <param name="amount"></param>
		public void ChangePressure(float amount)
		{
			if (!canChangePressure) return;

			airPressure += amount;
		}

		/// <summary>
		/// Tells this atmosphere to not change pressure. Also tells other atmospheres to skip this atmosphere for calculations.
		/// </summary>
		public void SetAirtight()
		{
			this.airPressure = AtmosphericPressure;
			this.previousAirPressure = AtmosphericPressure;
			this.canChangePressure = false;
		}

		/// <summary>
		/// Set this atmosphere to require updating.
		/// </summary>
		public void setDirty()
		{
			isTileAtmosphereDirty = true;
		}
		
		/// <summary>
		/// Essentially the same code that was in AttAirPressure. If this atmosphere needs updating (the air pressure changed or was set to dirty) it will go through a simple
		/// decay/growth function, depending on the difference between this tile's pressure and the tiles around it.
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime)
		{
			if (previousAirPressure != airPressure || isTileAtmosphereDirty)
			{
				float totalChange = 0; // the total air pressure change as summed from all neighbors.

				for (int i = 0; i < 8; i++)
				{
					Tile tile = parentTile.parentTileMap.GetNeighborOfTile(parentTile, i);

					if (tile.atmosphere.canChangePressure == false) continue; // skips this neighbor if it's a wall or something.
					if(tile is TileSpace) // if the neighbor is TileSpace, follow this block and continue to the next neighbor.
					{
						if(airPressure >= 0)
						{
							totalChange += -DecayFactor * TileSpace.AtmosphereEscapeRate;
						}
						continue;
					}

					float deltaP = airPressure - tile.atmosphere.airPressure;

					if (!(Math.Abs(deltaP) <= 0.01f))
					//{
					//	airPressure = tile.atmosphere.airPressure;
					//}
					//else
					{
						totalChange += -DecayFactor * deltaP;
						tile.atmosphere.setDirty(); // lets the neighbor know it should recalculate things, as this tile has changed and is attached to it.
					
					}

				}

				ChangePressure(totalChange);

				isTileAtmosphereDirty = false; // Air calculations complete, this tile is clean.
			}
		}
	}
}
