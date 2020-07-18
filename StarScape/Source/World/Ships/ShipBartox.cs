using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using StarScape.Source.World.Tiles;

namespace StarScape.Source.World.Ships
{
	/// <summary>
	/// This is meant to be a small ship that I used when making the first attempts at air. This is, in essence, a deprecated class.
	/// </summary>
	public class ShipBartox : Ship
	{

		public ShipBartox(Vector2 pos) : base(pos)
		{

		}

		public override string GetShipName()
		{
			return "Bartox";
		}

		public override TileMap CreateTileMap()
		{
			TileMap temp = new TileMap(10, 10);

			GenerateShip(temp);

			return temp;
		}

		private void GenerateShip(in TileMap map)
		{
			int shipWidth = map.GetXSize();
			int shipHeight = map.GetYSize();

			for(int x = 0; x < shipWidth; x++)
			{
				for(int y = 0; y < shipWidth; y++)
				{

					if(x == 0 || x == shipWidth - 1 || y == 0 || y == shipHeight - 1)
					{
						
						//map.PlaceTile(new TileHull(x, y), true);
						//map.PlaceTile(new TileWall(x, y), true);
					}
					else
					{

						//map.PlaceTile(new TileHull(x, y), true);
						//map.PlaceTile(new TileFloor(x, y), true);
					}

				}
			}
		}
		

		long timer = 0;
		long clock = 0;

		public override void Update(GameTime gameTime)
		{
			timer = Time.gameTime;
			base.Update(gameTime);
			timer = Time.gameTime - timer;

			if (Time.gameTime - clock >= 1000)
			{
				clock = Time.gameTime;

				Debug.Log(clock, true);


			}

		}
	}
	
}
