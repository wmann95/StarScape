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
	/// <summary>
	/// This class is a sort of List<T>() specifically for tiles.
	/// </summary>
	public class TileMap
	{
		public Ship parentShip { get; private set; } //which ship is this tilemap attached to?

		Tile[][] tiles;

		/// <summary>
		/// TileMap constructor that takes in two ints, the width and height, and the parent ship.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="ship"></param>
		public TileMap(int x, int y, Ship ship)
		{
			parentShip = ship;

			//Initializes the x-dimension of the tilemap and creates a y-dimension in each of those indexes.
			tiles = new Tile[x][];
			for (int i = 0; i < x; i++)
			{
				tiles[i] = new Tile[y];
			}
			
			//Initializes all of the empty tiles of the new tilemap and set's their parent to this tilemap.
			for(int i = 0; i < tiles.Length; i++)
			{
				for(int j = 0; j < tiles[i].Length; j++)
				{
					tiles[i][j] =  new Tile(i, j);
				}
			}
		}

		/// <summary>
		/// Puts a top into a tile using the Tiles AddTop method.
		/// </summary>
		/// <param name="xPos"></param>
		/// <param name="yPos"></param>
		/// <param name="top"></param>
		public virtual void AddTop(int xPos, int yPos, Top top)
		{
			Tile.AddTop(top, ref tiles[xPos][yPos]);
		}

		public virtual void RemoveTop<T>(int xPos, int yPos)
		{
			//List<Top> tops = tiles[xPos][yPos].tops;
			
		}
		public virtual void RemoveTop(int xPos, int yPos, Top top)
		{

		}

		public int GetWidth()
		{
			return tiles.Length;
		}

		public int GetHeight()
		{
			return tiles[0].Length;
		}

		public Tile GetTile(int xPos, int yPos)
		{
			return tiles[xPos][yPos];
		}

		/// <summary>
		/// Sets the tilemap coordinate to the new tile and sets that tile's parent to this tilemap.
		/// </summary>
		/// <param name="xPos"></param>
		/// <param name="yPos"></param>
		/// <param name="tile"></param>
		private void SetTile(int xPos, int yPos, ref Tile tile)
		{
			tiles[xPos][yPos] = tile;
			tiles[xPos][yPos].parentTileMap = this;
			//tiles[xPos][yPos].atmosphere = tile.atmosphere;
			//tiles[xPos][yPos].atmosphere.parentTile = tiles[xPos][yPos];
		}

		/// <summary>
		/// This method is called during the load phase.
		/// </summary>
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

		/// <summary>
		/// This method is called during the draw phase.
		/// </summary>
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

		/// <summary>
		/// This method is called during the update loop.
		/// </summary>
		/// <param name="gameTime"></param>
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

		/// <summary>
		/// set the given tile to null, which, for the intents and purposes of the air pressure and other mechanics, acts as a "space" tile. (removes any air rapidly.)
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void RemoveTile(int x, int y)
		{
			if (tiles[x][y] == null) return;

			//Console.WriteLine("This tile: " + tiles[x][y].ToString());

			for(int i = 0; i < 8; i++)
			{
				//Console.WriteLine("xPos: " + x + ", yPos: " + y + ", neighborID: " + i);
				Tile neighbor = GetNeighborOfTile(tiles[x][y], i);
				//Console.WriteLine("Neighbor: " + i + ", " + neighbor.ToString());

				if (GetNeighborOfTile(tiles[x][y], i) is TileSpace) continue;
				GetNeighborOfTile(tiles[x][y], i).atmosphere.setDirty();
			}

			tiles[x][y] = null;
		}
		
		/// <summary>
		/// essentially overwrites any of the tile slots on the tilemap via a new tile array and position of the top left-hand corner on the original tilemap.
		/// </summary>
		/// <param name="inTiles"></param>
		/// <param name="xPos"></param>
		/// <param name="yPos"></param>
		public void PlaceTiles(Tile[][] inTiles, int xPos, int yPos)
		{

			//Console.WriteLine("TileMap xPos  = {0} || TileMap yPos = {1}", (xPos), (yPos));
			//Console.WriteLine("TileMap width  = {0} || TileMap height = {1}", (tiles.Length), (tiles[0].Length));
			//Console.WriteLine("TileMap inTiles.width  = {0} || TileMap inTiles.height = {1}", (inTiles.Length), (inTiles[0].Length));

			for (int i = 0; i < inTiles.Length; i++)
			{
				for(int j = 0; j < inTiles[i].Length; j++)
				{
					if(i + xPos < 0 || i + xPos >= tiles.Length | j + yPos < 0 || j + yPos >= tiles[0].Length)
					{
						Debug.WriteLine("PlaceTiles Error: Input points out-of-bounds.", true);
						continue;
					}

					SetTile(i + xPos, j + yPos, ref inTiles[i][j]);
					tiles[i + xPos][j + yPos].xPos = i + xPos;
					tiles[i + xPos][j + yPos].yPos = j + yPos;
					tiles[i + xPos][j + yPos].parentTileMap = this;

					//Console.WriteLine("TileMap Add Tile To x={0} || TileMap Add Tile To y={1} ||| ", (i + xPos), (j + yPos), tiles[i + xPos]);
				}
			}


		}

		public Tile[][] GetTiles(int x1, int y1, int x2, int y2)
		{
			Tile[][] buffer = new Tile[x2 - x1 + 1][];
			for(int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = new Tile[y2 - y1 + 1];
			}

			for(int i = 0; i < buffer.Length; i++)
			{
				for (int j = 0; j < buffer[0].Length; j++)
				{
					buffer[i][j] = tiles[x1 + i][y1 + j];
				}
			}

			return buffer;
		}

		/// <summary>
		/// This is a method that takes in the tile you want to check neighbors for and an integer that tells which neighbor you want to get. an input of '0' means the tile right above, '1' is the tile one above and to the right, and continues on clockwise up until 7.
		/// </summary>
		/// <param name="tile"></param>
		/// <param name="neighbor"></param>
		/// <returns></returns>
		public ref Tile GetNeighborOfTile(Tile tile, int neighbor)
		{
			//Console.WriteLine(tile.xPos);

			int xPos = tile.xPos, yPos = tile.yPos;
			int xOffset = 0, yOffset = 0;
			int i = neighbor % 8; // makes it so that, even if the neighbor int is 8 or above, it will still return the proper neighbor.
			
			//switch statement of the neighbor int.
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

			//if the tile checked is off of the tilemap bounds, take it to mean that the tile is out in space.
			if (xPos + xOffset < 0 || xPos + xOffset >= tiles.Length)
			{
				return ref Tile.tileSpace;
			}
			else if (yPos + yOffset < 0 || yPos + yOffset >= tiles[0].Length)
			{
				return ref Tile.tileSpace;
			}
			else if(tiles[xPos + xOffset][yPos + yOffset] == null)
			{
				return ref Tile.tileSpace;
			}

			return ref tiles[xPos + xOffset][yPos + yOffset];
		}

	}
}
