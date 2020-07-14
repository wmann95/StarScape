using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.World.Ships;
using StarScape.Source.World.Tiles.Tops;

namespace StarScape.Source.World.Tiles
{
	public class TileMap
	{
		public Ship parentShip { get; private set; }

		Tile[][] tiles;

		public TileMap(int x, int y, Ship ship)
		{
			parentShip = ship;

			tiles = new Tile[x][];
			for (int i = 0; i < x; i++)
			{
				tiles[i] = new Tile[y];
			}
			
			for(int i = 0; i < tiles.Length; i++)
			{
				for(int j = 0; j < tiles[i].Length; j++)
				{
					tiles[i][j] = new Tile(i , j);

					tiles[i][j].parentTileMap = this;
				}
			}
		}

		public virtual void InitializeTop(int xPos, int yPos, Top top)
		{
			Tile.AddTop(top, ref tiles[xPos][yPos]);
		}

		public int GetWidth()
		{
			return tiles.Length;
		}

		public int GetHeight()
		{
			return tiles[0].Length;
		}

		public Tile getTile(int xPos, int yPos)
		{
			return tiles[xPos][yPos];
		}

		public void LoadContent()
		{
			foreach(Tile[] tArray in tiles)
			{
				foreach(Tile t in tArray)
				{
					if (t == null) continue;
					t.LoadContent();
				}
			}
		}

		public void Draw(SpriteBatch batch)
		{
			foreach (Tile[] tArray in tiles)
			{
				foreach (Tile t in tArray)
				{
					if (t == null) continue;
					t.Draw(batch);
				}
			}
		}

		public void Update(GameTime gameTime)
		{
			foreach (Tile[] tArray in tiles)
			{
				foreach (Tile t in tArray)
				{
					if (t == null) continue;
					t.Update(gameTime);
				}
			}
		}
		

		public ref Tile GetNeighborOfTile(Tile tile, int neighbor)
		{
			int xPos = tile.xPos, yPos = tile.yPos;
			int xOffset = 0, yOffset = 0;
			int i = neighbor % 8;

			

			switch (i)
			{
				case 0:
				{
					xOffset = 0;
					yOffset = -1;
					break;
				}
				case 1:
				{
					xOffset = 1;
					yOffset = -1;
					break;
				}
				case 2:
				{
					xOffset = 1;
					yOffset = 0;
					break;
				}
				case 3:
				{
					xOffset = 1;
					yOffset = 1;
					break;
				}
				case 4:
				{
					xOffset = 0;
					yOffset = 1;
					break;
				}
				case 5:
				{
					xOffset = -1;
					yOffset = 1;
					break;
				}
				case 6:
				{
					xOffset = -1;
					yOffset = 0;
					break;
				}
				case 7:
				{
					xOffset = -1;
					yOffset = -1;
					break;
				}
			}

			//Console.WriteLine(xOffset + " : " + yOffset);

			if (xPos + xOffset < 0 || xPos + xOffset >= tiles.Length)
			{
				return ref Tile.tileSpace;
			}
			else if (yPos + yOffset < 0 || yPos + yOffset >= tiles.Length)
			{
				return ref Tile.tileSpace;
			}

			return ref tiles[xPos + xOffset][yPos + yOffset];
		}

	}
}
