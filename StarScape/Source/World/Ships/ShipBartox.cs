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
	/// <summary>
	/// This is meant to be a small ship that I used when making the first attempts at air. This is, in essence, a deprecated class.
	/// </summary>
	public class ShipBartox : Ship
	{

		public ShipBartox(Vector2 pos) : base(pos)
		{

		}

		public override TileMap CreateTileMap()
		{
			TileMap temp = new TileMap(1, 1, this);

			BuildTops(temp);

			return temp;
		}

		private void BuildTops(in TileMap map)
		{
			int shipWidth = map.GetWidth();
			int shipHeight = map.GetHeight();

			for(int y = 0; y < shipHeight; y++)
			{
				for(int x = 0; x < shipWidth; x++)
				{

					if(x == 0 || x == shipWidth - 1 || y == 0 || y == shipHeight - 1)
					{
						map.AddTop(x, y, new TopHull());
						map.AddTop(x, y, new TopWall());
					}
					else
					{

						map.AddTop(x, y, new TopHull());
						TopFloor floor = new TopFloor();
						//if (x == 1 && y == 1) floor.setDebug(true);

						map.AddTop(x, y, floor);
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
			//Debug.WriteLine(clock, true);

			if (Time.gameTime - clock >= 100)
			{
				clock = Time.gameTime;
				


			}

		}
	}
	
}
