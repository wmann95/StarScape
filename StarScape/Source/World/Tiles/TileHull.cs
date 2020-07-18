

namespace StarScape.Source.World.Tiles.Tops
{
	/// <summary>
	/// Essentially a placeholder tile that designates a tile as part of the ship.
	/// </summary>
	public class TileHull : Tile
	{

		public TileHull(int x, int y) : base (x, y, 1)
		{
			//this.AddAttribute(new AttAirPressure()); // This is just a test thing... hulls won't come with added air pressure on installing.
		}
		
		public override string GetTexture()
		{
			return "HullTile1";
		}
		
	}
}
