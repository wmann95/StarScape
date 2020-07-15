using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.World.Tiles;
using StarScape.Source.World.Tiles.Tops;

namespace StarScape.Source.World.Ships
{
	public abstract class Ship
	{

		public TileMap shipTilemap;

		public bool isDirty { get; private set; }

		public Vector2 Position;

		public Ship(Vector2 pos)
		{
			shipTilemap = CreateTileMap();
			RemoveEmptyTiles();
			Position = pos;
		}

		public abstract TileMap CreateTileMap();

		public void LoadContent()
		{
			shipTilemap.LoadContent();
		}

		public void Draw(SpriteBatch batch)
		{
			shipTilemap.Draw(batch);
		}
		
		public virtual void Update(GameTime gameTime)
		{
			shipTilemap.Update(gameTime);
		}

		public static void BuildRoom(in TileMap tileMap, int xPos, int yPos, int xSize, int ySize)
		{
			
			Tile[][] buffer = new Tile[xSize][];
			for(int x = 0; x < xSize; x++)
			{
				buffer[x] = new Tile[ySize];
			}
			//Console.WriteLine("buffer xSize = {0} || buffer ySize = {1}", (xSize), (ySize));

			for (int i = 0; i < xSize; i++)
			{
				for(int j = 0; j < ySize; j++)
				{
					buffer[i][j] = new Tile(i + xPos, j + yPos);
					Tile.AddTop(new TopHull(), ref buffer[i][j]);

					if (i == 0 || i == xSize - 1 || j == 0 || j == ySize - 1)
					{
						Tile.AddTop(new TopWall(), ref buffer[i][j]);

					}
				}
			}

			Console.WriteLine("xPos = {0} || yPos = {1}", (xPos), (yPos));
			tileMap.PlaceTiles(buffer, xPos, yPos);

		}

		private void RemoveEmptyTiles()
		{
			int lowestX = 0;
			int lowestY = 0;
			int highestX = shipTilemap.GetWidth();
			int highestY = shipTilemap.GetHeight();

			for (int i = 0; i < shipTilemap.GetWidth(); i++)
			{

				for (int j = 0; j < shipTilemap.GetHeight(); j++)
				{
					if (shipTilemap.GetTile(i, j).tops.Count == 0)
					{
						shipTilemap.RemoveTile(i, j);
					}
				}
			}

			for (int i = 0; i < shipTilemap.GetWidth(); i++)
			{
				for (int j = 0; j < shipTilemap.GetHeight(); j++)
				{
					if (shipTilemap.GetTile(i, j) != null && lowestX == 0) { lowestX = i; }
					//if (shipTilemap.GetTile(i, j) != null && lowestY == 0) { lowestX = i; }
				}
			}

			Console.WriteLine(lowestX);

		}
	}
}
