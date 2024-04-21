using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.World.Tiles.Attributes;

namespace StarScape.Source.World.Tiles
{
	public class TileWall : Tile
	{

		public override Texture2D TileTexture { get { return LoadHelper.LoadTexture("WallTile1"); } }

		public override bool DoesTextureHaveTransparency { get { return false; } }

		public override int Layer { get { return 10; } }

		public TileWall()
		{
			//AddAttribute(new AttAirtight());
		}

	}
}
