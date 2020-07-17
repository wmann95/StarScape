using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarScape.Source.World.Tiles.Tops.Attributes;

namespace StarScape.Source.World.Tiles.Tops
{
	/// <summary>
	/// Essentially a placeholder class that keeps the air on the inside of the ship... not currently being worked into the atmospherics calculations.
	/// </summary>
	public class TileWall : Tile
	{

		public override int TileLayer { get { return 5; } }

		public TileWall(int x, int y) : base(x, y)
		{
			AddAttribute(new AttAirtight());
		}

		public override string GetTexture()
		{
			return "WallTile1";
		}
	}
}
