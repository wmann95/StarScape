

using Microsoft.Xna.Framework.Graphics;

namespace StarScape.Source.World.Tiles
{
	/// <summary>
	/// This class is the top that adds a floor tile to the rooms hull.
	/// </summary>
	public class TileFloor : Tile
	{

		public override Texture2D TileTexture { get { return LoadHelper.LoadTexture("FloorTile1"); } }
		public override bool DoesTextureHaveTransparency { get { return false; } }

		public TileFloor(int x, int y) : base(x, y, 5)
		{
		}
		
		
	}
}
