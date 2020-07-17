using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarScape.Source.World.Tiles
{
	/// <summary>
	/// This Class is one of my current projects. It essentially requires some hands-on work in the base Top class. This class will encompass all doors, from locked security doors to airlocks.
	/// </summary>
	public class TileDoor : Tile
	{

		public override int TileLayer { get { return 6; } }

		enum DoorType { BasicDoor }

		//Deprecated: When I wrote this, I realized I could do this in the LoadHelper class to help optimize loading, and this is now obsolete.
		//Dictionary<int, Texture2D> doorTextures = new Dictionary<int, Texture2D>();

		public TileDoor(int x, int y) : base(x, y)
		{
			
		}
		
		public override string GetTexture()
		{
			return "TopFloor1";
		}
	}
}
