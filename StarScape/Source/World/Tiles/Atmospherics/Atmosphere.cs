using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarScape.Source.World.Tiles.Atmospherics
{
	public class Atmosphere
	{
		Tile parentTile;

		public bool debugFlag { get; set; }

		public static readonly float AtmosphericPressure = 101.325f;
		static readonly float DecayFactor = 0.05f;

		public float airPressure { get; private set; }
		private float previousAirPressure { get; set; }
		private bool canChangePressure = true;
		private bool isTileAtmosphereDirty = false;

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

		//int indexInt = 0;
		//long clockTime;
		//bool flag = true;

		public void ChangePressure(float amount)
		{
			if (!canChangePressure) return;

			airPressure += amount;
		}

		public void SetAirtight()
		{
			this.airPressure = 0;
			this.previousAirPressure = 0;
			this.canChangePressure = false;
		}

		public void setDirty()
		{
			isTileAtmosphereDirty = true;
		}
		
		public void Update(GameTime gameTime)
		{
			//Console.WriteLine(airPressure + " : " + previousAirPressure);
			if (previousAirPressure != airPressure || isTileAtmosphereDirty)
			{
				//previousAirPressure = airPressure;
				for (int i = 0; i < 8; i++)
				{

					//Console.WriteLine(this.parentTile.xPos + " : " + this.parentTile.yPos);
					Tile tile = parentTile.parentTileMap.GetNeighborOfTile(parentTile, i);

					if (tile.atmosphere.canChangePressure == false) continue;
					if(tile is TileSpace)
					{
						if(airPressure >= 0)
						{
							ChangePressure(-DecayFactor * TileSpace.AtmosphereEscapeRate);
							tile.atmosphere.ChangePressure(DecayFactor * TileSpace.AtmosphereEscapeRate);
						}
						continue;
					}

					float deltaP = airPressure - tile.atmosphere.airPressure;

					//Console.WriteLine(deltaP);

					//Debug.WriteLine(airPressure + " : " + tile.atmosphere.airPressure, debugFlag);
					//Debug.WriteLine(deltaP, debugFlag);

					if (Math.Abs(deltaP) <= 0.01f)
					{
						airPressure = tile.atmosphere.airPressure;
					}
					else
					{
						ChangePressure(-DecayFactor * deltaP);
						tile.atmosphere.ChangePressure(DecayFactor * deltaP);
					
					}

						//Console.WriteLine(airPressure + " : " + aap.airPressure);

				}

				isTileAtmosphereDirty = false;
			}
		}
	}
}
