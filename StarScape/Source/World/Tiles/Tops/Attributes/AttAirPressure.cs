using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarScape.Source.World.Tiles.Tops.Attributes
{


	public class AttAirPressure : Attribute
	{
		public bool debugFlag { get; set; }

		public static readonly float AtmosphericPressure = 101.325f;
		static readonly float DecayFactor = 0.05f;

		public float airPressure { get; private set; }
		private float previousAirPressure { get; set; }

		public AttAirPressure()
		{
			airPressure = AtmosphericPressure;
			previousAirPressure = airPressure;
		}

		public AttAirPressure(float initialPressure)
		{
			airPressure = initialPressure;
			previousAirPressure = airPressure;
		}

		//int indexInt = 0;
		//long clockTime;
		//bool flag = true;

		public void ChangePressure(float amount)
		{
			airPressure += amount;
		}

		public override void Update(GameTime gameTime)
		{
			//Console.WriteLine(airPressure + " : " + previousAirPressure);
			if (previousAirPressure != airPressure)
			{
				//previousAirPressure = airPressure;
				for (int i = 0; i < 8; i++)
				{
					Tile tile = parentTop.parentTile.parentTileMap.GetNeighborOfTile(parentTop.parentTile, i);
					
					foreach(Top t in tile.tops)
					{
						if (t.HasAttribute<AttAirPressure>() || tile == Tile.tileSpace)
						{
							AttAirPressure aap = ((AttAirPressure)t.getAttribute<AttAirPressure>());
							if ( aap != null)
							{
								float deltaP = airPressure - aap.airPressure;

								Debug.WriteLine(airPressure + " : " + aap.airPressure, debugFlag);
								Debug.WriteLine(deltaP, debugFlag);

								if (Math.Abs(deltaP) <= 0.01f) {
									airPressure = aap.airPressure;
								}
								else
								{
									ChangePressure(-DecayFactor * deltaP);
									aap.ChangePressure(DecayFactor * deltaP);

								}

								//Console.WriteLine(airPressure + " : " + aap.airPressure);

							}
							else if (tile == Tile.tileSpace)
							{

							}


						}
					}
				}
			}

			//previousAirPressure = airPressure;

			/*if(flag)
			{
				clockTime = Time.gameTime;
				Console.WriteLine(clockTime);
				flag = false;
			}
			else
			{
				//Console.WriteLine(Time.gameTime);
				if (Time.gameTime - clockTime >= 1000)
				{
					clockTime = Time.gameTime;

					Tile tile = parentTop.parentTile.parentTileMap.GetNeighborOfTile(parentTop.parentTile, indexInt);
					indexInt++;

					Console.WriteLine(tile.xPos + ", " + tile.yPos);
				}
			}
			*/



		}
	}
}
