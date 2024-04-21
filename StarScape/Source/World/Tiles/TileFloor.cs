

using Microsoft.Xna.Framework.Graphics;

namespace StarScape.Source.World.Tiles
{
	public class TileFloor : Tile
	{

		public override Texture2D TileTexture { get { return LoadHelper.LoadTexture("FloorTile1"); } }

		public override bool DoesTextureHaveTransparency { get { return false; } }

		public override int Layer { get { return 3; } }

		public TileFloor()
		{
		}
	}
}
