using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using StarScape.Source.World.Tiles.Tops.Attributes;

namespace StarScape.Source.World.Tiles.Atmospherics
{
	/// <summary>
	/// This class replaces AttAirPressure and will be the method of handling atmospheres and gasses (atleast on tiles) for the future. Every tile made will come with an atmosphere
	/// object attached, which that tile set as the atmosphere's parent.
	/// </summary>
	public class Atmosphere
	{
		TileMap ParentTileMap;
		public int xPos { get; set; }
		public int yPos { get; set; }
		
		public static readonly float AtmosphericPressure = 101.325f; // Atmospheric Pressure Constant in Kilopascals (KPa)
		static readonly float DecayFactor = 0.25f; // The rate of decay that air undergoes during pressure calculations.

		public float airPressure { get; private set; } // this tiles air pressure.
		private float previousAirPressure { get; set; } // the air pressure of the previous update.
		public bool canChangePressure { get; set; } // Can this tile's atmosphere change pressure? If the tile is solid (like a wall), probably not.
		public bool isTileAtmosphereDirty { get; private set; } // Tells the atmosphere that something happened that requires it to update its calculations.

		public Atmosphere(TileMap tileMap, int x, int y)
		{
			ParentTileMap = tileMap;
			xPos = x;
			yPos = y;
			airPressure = AtmosphericPressure;
			previousAirPressure = airPressure;

			canChangePressure = true;

			isTileAtmosphereDirty = true;
		}

		public Atmosphere(TileMap tileMap, int x, int y, float initialPressure)
		{
			ParentTileMap = tileMap;
			xPos = x;
			yPos = y;
			airPressure = initialPressure;
			previousAirPressure = airPressure;
			
			canChangePressure = true;

			isTileAtmosphereDirty = true;
		}

		public Atmosphere(int x, int y, float initialPressure)
		{
			ParentTileMap = null;
			xPos = x;
			yPos = y;
			airPressure = initialPressure;

			canChangePressure = true;
			isTileAtmosphereDirty = false;
		}

		public Atmosphere(float initialPressure)
		{
			ParentTileMap = null;
			xPos = 0;
			yPos = 0;
			airPressure = initialPressure;

			canChangePressure = true;
			isTileAtmosphereDirty = false;
		}
		
		/// <summary>
		/// Changes this atmosphere's pressure.
		/// </summary>
		/// <param name="amount"></param>
		public void ChangePressure(float amount)
		{
			if (!canChangePressure) return;

			previousAirPressure = airPressure;
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

				for (int i = 0; i < ParentTileMap.MaxHeightOfTileMap; i++)
				{
					if (ParentTileMap.GetTile(xPos, yPos, i) == null) continue;
					if (ParentTileMap.GetTile(xPos, yPos, i).HasAttribute<AttAirtight>())
					{
						//Debug.Log("Neighbor is airtight.");
						this.canChangePressure = false;
					}
				}

				for (int i = 0; i < 8; i++)
				{
					Atmosphere neighborAtmos = ParentTileMap.GetNeighborAtmos(xPos, yPos, i);

					if (neighborAtmos == null || !neighborAtmos.canChangePressure) continue;
					
					float deltaP = airPressure - neighborAtmos.airPressure;
					
					totalChange += -DecayFactor * deltaP;
					neighborAtmos.ChangePressure(DecayFactor * deltaP);
					// lets the neighbor know it should recalculate things, as this tile has changed and is attached to it.
						
					neighborAtmos.setDirty();

					if (Math.Abs(deltaP) <= 0.001f) this.isTileAtmosphereDirty = false;
				}

				if (canChangePressure) {
					ChangePressure(totalChange);
				}
				 // Air calculations complete, this tile is clean.

				/*
				if (previousAirPressure != airPressure || isTileAtmosphereDirty)
				{
					float totalChange = 0; // the total air pressure change as summed from all neighbors.

					for(int i = 0; i < ParentTileMap.MaxHeightOfTileMap; i++)
					{
						if (ParentTileMap.GetTile(xPos, yPos, i) == null) continue;
						if (ParentTileMap.GetTile(xPos, yPos, i).HasAttribute<AttAirtight>())
						{
							Debug.Log("Neighbor is airtight.");
							this.canChangePressure = false;
						}
					}

					for (int i = 0; i < 8; i++)
					{
						Point neighborPos = ParentTileMap.GetNeighborPosition(xPos, yPos, i);


						if(neighborPos == new Point(-1, -1))
						{
							totalChange += -DecayFactor * 1000f;//TileSpace.AtmosphereEscapeRate;
							continue;
						}
						else if (ParentTileMap.GetTile(xPos, yPos, 0) is TileSpace)
						{
							totalChange += -DecayFactor * 1000f;//TileSpace.AtmosphereEscapeRate;
							continue;
						}

						ref Atmosphere neighborAtmos = ref ParentTileMap.GetAtmosphere(neighborPos.X, neighborPos.Y);
						Debug.Log("xPos: " + xPos + ", yPos: " + yPos + ", Neighbor index: " + i + ", Neighbor Pressure: " + neighborAtmos.airPressure);


						float deltaP = airPressure - neighborAtmos.airPressure;

						if (!(Math.Abs(deltaP) <= 0.01f))
						//{
						//	airPressure = tile.atmosphere.airPressure;
						//}
						//else
						{
							totalChange += -DecayFactor * deltaP;
							neighborAtmos.setDirty(); // lets the neighbor know it should recalculate things, as this tile has changed and is attached to it.

						}

					}

					//Debug.Log(totalChange);
					if(canChangePressure) ChangePressure(totalChange);

					isTileAtmosphereDirty = false; // Air calculations complete, this tile is clean.

				}
				*/
			}
		}
	}
}
