using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using StarScape.Source.World.Tiles;
using StarScape.Source.World.Tiles.Tops;
using StarScape.Source.World.Tiles.Tops.Attributes;

namespace StarScape.Source.World.Ships
{
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
			//Ship.BuildRoom(map, 4, 5, 10, 4);
			//Ship.BuildRoom(map, 4, 3, 10, 4);

			//			for (int y = 0; y < shipHeight; y++)
			//			{
			//				for (int x = 0; x < shipWidth; x++)
			//				{
			//
			//					if (x == 0 || x == shipWidth - 1 || y == 0 || y == shipHeight - 1)
			//					{
			//						map.InitializeTop(x, y, new TopHull());
			//						map.InitializeTop(x, y, new TopWall());
			//					}
			//					else
			//					{
			//
			//						map.InitializeTop(x, y, new TopHull());
			//						TopFloor floor = new TopFloor();
			//						//if (x == 1 && y == 1) floor.setDebug(true);
			//
			//						map.InitializeTop(x, y, floor);
			//					}
			//
			//				}
			//			}
		}
		
		long timer = 0;
		long clock = 0;

		public override void Update(GameTime gameTime)
		{
			timer = Time.gameTime;
			base.Update(gameTime);
			timer = Time.gameTime - timer;
			//Debug.WriteLine(clock, true);

			Position += new Vector2((float)(50f * gameTime.ElapsedGameTime.TotalSeconds), (float)(25f * gameTime.ElapsedGameTime.TotalSeconds));

			if (Time.gameTime - clock >= 100)
			{
				clock = Time.gameTime;

				//Console.WriteLine("Changing Air Pressure");


			}

		}
	}
}
