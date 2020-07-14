using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.World.Tiles;

namespace StarScape.Source.World.Ships
{
	public abstract class Ship
	{

		public List<TileMap> shipTilemaps = new List<TileMap>();

		public bool isDirty { get; private set; }

		public Vector2 Position;

		public Ship(Vector2 pos)
		{
			shipTilemaps.Add(CreateTileMap());
			Position = pos;
		}

		public abstract TileMap CreateTileMap();

		public void LoadContent()
		{
			foreach(TileMap map in shipTilemaps)
			{
				map.LoadContent();
			}
		}

		public void Draw(SpriteBatch batch)
		{
			foreach (TileMap map in shipTilemaps)
			{
				map.Draw(batch);
			}
		}
		
		public virtual void Update(GameTime gameTime)
		{
			foreach (TileMap map in shipTilemaps)
			{
				map.Update(gameTime);
			}

		}

		public Tile[][] BuildRoom(int xSize, int ySize)
		{
			Tile[][] buffer = new Tile[xSize][];
			for(int x = 0; x < xSize; x++)
			{
				buffer[x] = new Tile[ySize];
			}

			for (int i = 0; i < xSize; i++)
			{
				for(int j = 0; j < ySize; j++)
				{
					buffer[i][j] = new Tile(i, j);

					if(i == 0 || i == xSize - 1 || j == 0 || j == ySize - 1)
					{
					
					}
				}
			}

			return buffer;
		}
	}
}
