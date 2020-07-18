

namespace StarScape.Source.World.Tiles.Tops
{
	/// <summary>
	/// This class is the top that adds a floor tile to the rooms hull.
	/// </summary>
	public class TileFloor : Tile
	{
		

		public TileFloor(int x, int y) : base(x, y, 5)
		{
		}
		
		public override string GetTexture()
		{
			return "FloorTile1";
		}
		
	}
}
