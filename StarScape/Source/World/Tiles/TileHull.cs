

using Microsoft.Xna.Framework.Graphics;

namespace StarScape.Source.World.Tiles
{
	public class TileHull : Tile
	{

		public override Texture2D TileTexture { get { return LoadHelper.LoadTexture("HullTile1"); } }
		public override bool DoesTextureHaveTransparency { get { return false; } }
		public override int Layer { get { return 1; } }

		public TileHull()
		{
		}

		long clock = 0;

		bool toggle = false;

	}
}
