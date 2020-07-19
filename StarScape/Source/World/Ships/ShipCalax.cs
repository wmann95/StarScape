using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using StarScape.Source.World.Tiles;
using StarScape.Source.World.Tiles.Machinery;

namespace StarScape.Source.World.Ships
{
	/// <summary>
	/// This is the current ship I'm testing stuff about ship generation on as well as ship systems.
	/// </summary>
	public class ShipCalax : Ship
	{
		
		public ShipCalax(Vector2 pos) : base(pos)
		{
		}

		public override string GetShipName()
		{
			return "Calax";
		}

		int boxSize = 100;

		public override TileMap CreateTileMap()
		{
			TileMap temp = new TileMap(boxSize, boxSize);

			GenerateShip(temp);

			return temp;
		}

		/// <summary>
		/// This is where the ship generation will be held. A decent example of how a predetermined ship can be built using the BuildRoom tool.
		/// </summary>
		/// <param name="map"></param>
		private void GenerateShip(TileMap map)
		{
			int shipWidth = map.GetXSize();
			int shipHeight = map.GetYSize();

			int scale = boxSize;

			BuildRoom(map, 0, 0, 1 * scale, 1 * scale);
			//BuildRoom(map, 4 * scale, 3 * scale, 9 * scale, 4 * scale);
			//BuildRoom(map, 4 * scale, 0, 5 * scale, 4 * scale);
			//BuildRoom(map, 8 * scale, 0, 5 * scale, 4 * scale);
			//BuildRoom(map, 4 * scale, 6 * scale, 5 * scale, 4 * scale);
			//BuildRoom(map, 8 * scale, 6 * scale, 5 * scale, 4 * scale);


			for (int i = 1; i < boxSize - 1; i++)
			{
				map.PlaceTile(new TileSpace(i, i), true);
			}
			//map.RemoveTile(19, 10, 5);


		}
		
		long timer = 0;
		long clock = 0;
		bool flag = true;

		public override void Update(GameTime gameTime)
		{
			timer = Time.gameTime;
			base.Update(gameTime);
			timer = Time.gameTime - timer;


			if (Time.gameTime - clock >= 5000)
			{
				clock = Time.gameTime;
				shipTilemap.DrawOptimizationTest = !shipTilemap.DrawOptimizationTest;
			}


			//Tile tile = shipTilemap.GetNeighborOfTile(shipTilemap.GetTile(10, 2), 0);
			//Console.WriteLine(tile);

		}
	}
}
