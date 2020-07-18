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

		public override TileMap CreateTileMap()
		{
			TileMap temp = new TileMap(20, 20);

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

			int scale = 20;

			BuildRoom(map, 0, 0, 1 * scale, 1 * scale);
			//BuildRoom(map, 4 * scale, 3 * scale, 9 * scale, 4 * scale);
			//BuildRoom(map, 4 * scale, 0, 5 * scale, 4 * scale);
			//BuildRoom(map, 8 * scale, 0, 5 * scale, 4 * scale);
			//BuildRoom(map, 4 * scale, 6 * scale, 5 * scale, 4 * scale);
			//BuildRoom(map, 8 * scale, 6 * scale, 5 * scale, 4 * scale);

			//map.RemoveTile(19, 9, 5);
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


			if (Time.gameTime - clock >= 3000 && flag)
			{

				flag = false;

				//Debug.Log(shipTilemap.GetTile(0, 0, 5));
				
				shipTilemap.RemoveAllTilesAt(9, 9);
				shipTilemap.RemoveAllTilesAt(9, 10);
				shipTilemap.RemoveAllTilesAt(10, 9);
				shipTilemap.RemoveAllTilesAt(10, 10);

				shipTilemap.PlaceTile(new MachineOxyGen(2, 2), false);
				shipTilemap.PlaceTile(new MachineOxyGen(17, 2), false);
				shipTilemap.PlaceTile(new MachineOxyGen(2, 17), false);
				shipTilemap.PlaceTile(new MachineOxyGen(17, 17), false);
			}

			//Tile tile = shipTilemap.GetNeighborOfTile(shipTilemap.GetTile(10, 2), 0);
			//Console.WriteLine(tile);

		}
	}
}
