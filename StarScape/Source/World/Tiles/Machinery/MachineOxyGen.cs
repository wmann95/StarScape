using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using StarScape.Source.World.Tiles.Atmospherics;

namespace StarScape.Source.World.Tiles.Machinery
{
	public class MachineOxyGen : Machinery
	{
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

			if (parentTile.atmosphere.airPressure <= minAirPressure)
			{
				isMachineOn = true;
				turnOnTime = Time.gameTime;
			}
			if (parentTile.atmosphere.airPressure >= turnOffPressure && Time.gameTime - turnOnTime >= stayOnDelay)
			{
				isMachineOn = false;

			}

			if (isMachineOn) parentTile.atmosphere.ChangePressure(airProduction);
		}
	}
}
