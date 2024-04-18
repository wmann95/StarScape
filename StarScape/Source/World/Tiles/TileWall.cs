using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.World.Tiles.Attributes;

namespace StarScape.Source.World.Tiles
{
	/// <summary>
	/// Essentially a placeholder class that keeps the air on the inside of the ship... not currently being worked into the atmospherics calculations.
	/// </summary>
	public class TileWall : Tile
	{

		public override Texture2D TileTexture { get { return LoadHelper.LoadTexture("WallTile1"); } }
		public override bool DoesTextureHaveTransparency { get { return false; } }

		public TileWall(int x, int y) : base(x, y, 5)
		{
			AddAttribute(new AttAirtight());
		}


		public override void Draw(SpriteBatch batch)
		{
			base.Draw(batch);
		}
	}
}
