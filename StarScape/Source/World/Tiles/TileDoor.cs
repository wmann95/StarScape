using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarScape.Source.World.Tiles
{
	public class TileDoor : Tile
	{
		public override Texture2D TileTexture { get { return LoadHelper.LoadTexture(null); } }
		public override bool DoesTextureHaveTransparency { get { return true; } }

		public override int Layer { get { return 5; } }

		enum DoorType { BasicDoor }

		public TileDoor()
		{
			
		}
		
	}
}
