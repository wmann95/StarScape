using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StarScape.Source.Rendering;
using StarScape.Source.World.Ships;
using StarScape.Source.World.Tiles.Tops;

namespace StarScape.Source.World
{
	public class World
	{

		List<Ship> ships = new List<Ship>();

		public World()
		{
			//map = new TileMap();

			//player = new Player(0,0);
			ships.Add(new ShipBartox(new Vector2(200f, 300f)));
			ships.Add(new ShipCalax(new Vector2(200f, -100f)));
		}
		

		public void Load()
		{
			foreach (Ship ship in ships)
			{
				ship.LoadContent();
				//player.Load();
			}
		}

		public void Draw(GameTime gameTime, SpriteBatch batch)
		{
			//map.Draw(batch);
			foreach (Ship ship in ships)
			{
				ship.Draw(batch);
				//player.Load();
			}
			//batch.Draw(player.texture, new Vector2(player.xPos * 64, player.yPos * 64), Color.White);
		}

		public void Update(GameTime gameTime)
		{

			//player.Update(gameTime);
			foreach (Ship ship in ships)
			{
				ship.Update(gameTime);
				//player.Load();
			}
		}
	}
}
