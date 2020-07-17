using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using StarScape.Source.World.Tiles.Atmospherics;
using StarScape.Source.World.Tiles.Tops;

namespace StarScape.Source.World.Tiles.Machinery
{
	public class MachineOxyGen : Top, IMachinery
	{
		public bool IsMachineOn { get; private set; }

		private float airProduction = 25f;
		private float minAirPressure = 70f;
		private float turnOffPressure = Atmosphere.AtmosphericPressure;

		private long turnOnTime = 0;
		private int stayOnDelay = 5000;

		public MachineOxyGen()
		{
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			//if (parentTile.atmosphere.airPressure <= minAirPressure)
			//{
			//	IsMachineOn = true;
			//	turnOnTime = Time.gameTime;
			//}
			//if (parentTile.atmosphere.airPressure >= turnOffPressure && Time.gameTime - turnOnTime >= stayOnDelay)
			//{
			//	IsMachineOn = false;
//
			//}
//
			//if (IsMachineOn) parentTile.atmosphere.ChangePressure(airProduction);
		}
	}
}
