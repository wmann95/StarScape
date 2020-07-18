using StarScape.Source.World.Tiles.Attributes;

namespace StarScape.Source.World.Tiles.Tops
{
	/// <summary>
	/// Essentially a placeholder class that keeps the air on the inside of the ship... not currently being worked into the atmospherics calculations.
	/// </summary>
	public class TileWall : Tile
	{
		
		public TileWall(int x, int y) : base(x, y, 5)
		{
			AddAttribute(new AttAirtight());
		}
		

		public override string GetTexture()
		{
			return "WallTile1";
		}
	}
}
