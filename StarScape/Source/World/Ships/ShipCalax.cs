using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using StarScape.Source.World.Tiles;
using StarScape.Source.World.Tiles.Machinery;
using StarScape.Source.World.Tiles.Tops;
using StarScape.Source.World.Tiles.Tops.Attributes;

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
			TileMap temp = new TileMap(50, 50);

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

			int scale = 1;

			Ship.BuildRoom(map, 1 * scale, 0, 4 * scale, 10 * scale);
			Ship.BuildRoom(map, 4 * scale, 3 * scale, 9 * scale, 4 * scale);
			Ship.BuildRoom(map, 4 * scale, 0, 5 * scale, 4 * scale);
			Ship.BuildRoom(map, 8 * scale, 0, 5 * scale, 4 * scale);
			Ship.BuildRoom(map, 4 * scale, 6 * scale, 5 * scale, 4 * scale);
			Ship.BuildRoom(map, 8 * scale, 6 * scale, 5 * scale, 4 * scale);

			for (int i = 0; i < map.GetXSize(); i++)
			{
				for (int j = 0; j < map.GetYSize(); j++)
				{
					for (int k = 0; k < map.MaxHeightOfTileMap; k++)
					{
						if (map.GetTile(i, j, k) != null) Debug.Log(map.GetTile(i, j, k));
					}
				}
			}

			if (map.GetTile(0, 0, TileWall.TileLayer) != null)
			{
				map.RemoveTile(0, 0, TileWall.TileLayer);
			}

			//map.AddTop(2 * scale, 1 * scale, new MachineOxyGen());
			//map.AddTop(5 * scale, 1 * scale, new MachineOxyGen());
			//map.AddTop(9 * scale, 1 * scale, new MachineOxyGen());

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

				Debug.Log(shipTilemap.GetTile(0, 0, TileWall.TileLayer));

				shipTilemap.RemoveAllTilesAt(1, 1);
			}

			//Tile tile = shipTilemap.GetNeighborOfTile(shipTilemap.GetTile(10, 2), 0);
			//Console.WriteLine(tile);

		}
	}
}
