//using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StarScape.Source.World.Tiles.Tops.Attributes;

namespace StarScape.Source.World.Tiles.Tops
{
	/// <summary>
	/// This class is the top that adds a floor tile to the rooms hull.
	/// </summary>
	public class TileFloor : Tile
	{

		public TileFloor(int x, int y) : base(x, y)
		{
		}

		public override int TileLayer { get { return 5; } }

		public override string GetTexture()
		{
			return "FloorTile1";
		}
		
	}
}
