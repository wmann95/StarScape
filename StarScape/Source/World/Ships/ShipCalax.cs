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

		public override TileMap CreateTileMap()
		{
			TileMap temp = new TileMap(20, 10, this);

			BuildTops(temp);

			return temp;
		}

		/// <summary>
		/// This is where the ship generation will be held. A decent example of how a predetermined ship can be built using the BuildRoom tool.
		/// </summary>
		/// <param name="map"></param>
		private void BuildTops(in TileMap map)
		{
			int shipWidth = map.GetWidth();
			int shipHeight = map.GetHeight();

			Ship.BuildRoom(map, 1, 0, 4, shipHeight);
			Ship.BuildRoom(map, 4, 3, 9, 4);
			Ship.BuildRoom(map, 4, 0, 5, 4);
			Ship.BuildRoom(map, 8, 0, 5, 4);
			Ship.BuildRoom(map, 4, 6, 5, 4);
			Ship.BuildRoom(map, 8, 6, 5, 4);

			if(map.GetTile(4, 4).GetTop(1) is TopWall)
			{
				map.GetTile(4, 4).tops.RemoveAt(1);
			}
			if (map.GetTile(4, 5).GetTop(1) is TopWall)
			{
				map.GetTile(4, 5).tops.RemoveAt(1);
			}
			if (map.GetTile(6, 3).GetTop(1) is TopWall)
			{
				map.GetTile(6, 3).tops.RemoveAt(1);
			}
			if (map.GetTile(6, 6).GetTop(1) is TopWall)
			{
				map.GetTile(6, 6).tops.RemoveAt(1);
			}
			if (map.GetTile(10, 3).GetTop(1) is TopWall)
			{
				map.GetTile(10, 3).tops.RemoveAt(1);
			}
			if (map.GetTile(10, 6).GetTop(1) is TopWall)
			{
				map.GetTile(10, 6).tops.RemoveAt(1);
			}

			map.AddTop(2, 1, new MachineOxyGen());
			map.AddTop(5, 1, new MachineOxyGen());
			map.AddTop(9, 1, new MachineOxyGen());

			//Ship.BuildRoom(map, 4, 5, 10, 4);
			//Ship.BuildRoom(map, 4, 3, 10, 4);

			//			for (int y = 0; y < shipHeight; y++)
			//			{
			//				for (int x = 0; x < shipWidth; x++)
			//				{
			//
			//					if (x == 0 || x == shipWidth - 1 || y == 0 || y == shipHeight - 1)
			//					{
			//						map.AddTop(x, y, new TopHull());
			//						map.AddTop(x, y, new TopWall());
			//					}
			//					else
			//					{
			//
			//						map.AddTop(x, y, new TopHull());
			//						TopFloor floor = new TopFloor();
			//						//if (x == 1 && y == 1) floor.setDebug(true);
			//
			//						map.AddTop(x, y, floor);
			//					}
			//
			//				}
			//			}
		}
		
		long timer = 0;
		long clock = 0;
		bool flag = true;

		public override void Update(GameTime gameTime)
		{
			timer = Time.gameTime;
			base.Update(gameTime);
			timer = Time.gameTime - timer;
			//Debug.WriteLine(clock, true);

			//Position += new Vector2((float)(50f * gameTime.ElapsedGameTime.TotalSeconds), (float)(25f * gameTime.ElapsedGameTime.TotalSeconds));

			if (Time.gameTime - clock >= 3000 && flag)
			{
				//clock = Time.gameTime;

				//Console.WriteLine("Changing Air Pressure");

				flag = false;
				shipTilemap.RemoveTile(11, 8);
				shipTilemap.RemoveTile(7, 8);
				shipTilemap.RemoveTile(3, 8);
			}

		}
	}
}
