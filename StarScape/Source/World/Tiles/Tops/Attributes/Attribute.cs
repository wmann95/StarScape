using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarScape.Source.World.Tiles.Tops.Attributes
{
	public abstract class Attribute
	{
		public Top parentTop { get; internal set; }

		public abstract void Update(GameTime gameTime);

		//public static Attribute AirPressure = new AttAirPressure();
		//public static Attribute Airtight = new AttAirtight();
		//public static Attribute AirPressure = new AttAirPressure();


	}
}
