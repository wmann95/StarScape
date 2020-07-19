using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.World.Tiles.Atmospherics;

namespace StarScape.Source.World.Tiles.Machinery
{
	public class MachineOxyGen : Tile, IMachinery
	{

		public override Texture2D TileTexture { get { return LoadHelper.LoadTexture(null); } }
		public override bool DoesTextureHaveTransparency { get { return true; } }

		//TODO: Add a texture to this machine.
		//public new static int TextureID = LoadHelper.LoadTexture("HullTile1");

		public bool IsMachineOn { get; private set; }

		Atmosphere localAtmosphere;

		private float airProduction = 80f;
		private float minAirPressure = 80f;
		private float turnOffPressure = Atmosphere.AtmosphericPressure;

		private long turnOnTime = 0;
		private int stayOnDelay = 5000;

		public MachineOxyGen(int x, int y) : base (x, y, 6)
		{
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			//Debug.Log(xPos + " : "  + yPos);

			if(localAtmosphere == null || localAtmosphere != ParentTileMap.GetAtmosphere(xPos, yPos))
			{
				localAtmosphere = ParentTileMap.GetAtmosphere(xPos, yPos);
				//Debug.Log("Local Atmosphere: " + localAtmosphere.xPos + ", " + localAtmosphere.yPos);
			}

			if (localAtmosphere.airPressure <= minAirPressure)
			{
				IsMachineOn = true;
				turnOnTime = Time.gameTime;
			}
			if (localAtmosphere.airPressure >= turnOffPressure && Time.gameTime - turnOnTime >= stayOnDelay)
			{
				IsMachineOn = false;

			}

			if (IsMachineOn) localAtmosphere.ChangePressure(airProduction);
		}
	}
}
