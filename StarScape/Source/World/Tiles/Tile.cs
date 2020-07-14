using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.World.Tiles.Tops;

namespace StarScape.Source.World.Tiles
{
	public class Tile
	{
		public static Tile tileSpace = new Tile(-1, -1);

		public TileMap parentTileMap { get; internal set; }

		//the first index should be the bottom, so usually a hull if it's on a ship
		public List<Top> tops { get; private set; }

		public int xPos { get; set; }//{ get; private set; }
		public int yPos { get; set; }//{ get; private set; }

		public Tile(int x, int y)
		{
			this.xPos = x;
			this.yPos = y;
			tops = new List<Top>();
		}
		
		public void LoadFromFile() { }

		public void LoadContent()
		{

			foreach(Top t in tops)
			{
				if (t == null) continue;
				t.LoadContent();
			}
		}

		public void Draw(SpriteBatch batch)
		{
			foreach (Top t in tops)
			{
				if (t == null) continue;
				//if (t.TopName == "HullTile1") continue;
				t.Draw(batch);
			}
		}

		public void Update(GameTime gameTime)
		{
			foreach(Top t in tops)
			{
				if (t == null) continue;
				//Console.WriteLine(t.TopName);
				//if (t is TopFloor) ((TopFloor)t).Update(gameTime);
				t.Update(gameTime);
			}
		}

		public static void AddTop(Top top, ref Tile tile)
		{
			tile.tops.Add(top);
			top.parentTile = tile;
		}

		public Top GetTop(int i)
		{
			return tops.ElementAt(i);
		}

	}
}
