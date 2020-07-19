

using Microsoft.Xna.Framework.Graphics;

namespace StarScape.Source.World.Tiles
{
	/// <summary>
	/// Essentially a placeholder tile that designates a tile as part of the ship.
	/// </summary>
	public class TileHull : Tile
	{

		public override Texture2D TileTexture { get { return LoadHelper.LoadTexture("HullTile1"); } }
		public override bool DoesTextureHaveTransparency { get { return false; } }

		public TileHull(int x, int y) : base (x, y, 1)
		{
			//this.AddAttribute(new AttAirPressure()); // This is just a test thing... hulls won't come with added air pressure on installing.
		}
		
	}
}
