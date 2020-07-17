using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.World.Tiles.Atmospherics;

namespace StarScape.Source.World.Tiles
{
	public class TileSpace : Tile
	{
		public static float AtmosphereEscapeRate = 5000;

		public TileSpace() : base(-1, -1)
		{
		}

		public override int TileLayer
		{
			get
			{
				return 0;
			}
		}

		public override void Draw(SpriteBatch batch)
		{
			//Don't render anything, it's space..
		}
	}
}
