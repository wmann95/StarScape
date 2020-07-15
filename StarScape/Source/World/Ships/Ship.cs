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
	/// <summary>
	/// Blueprint for all Ship classes.
	/// </summary>
	public abstract class Ship
	{

		public TileMap shipTilemap;

		//public bool isDirty { get; private set; }

		public Vector2 Position; //Position "in space", if you will.

		/// <summary>
		/// This constructor does some of the backend stuff so that a developer working on ships doesn't need to do the small things like call CreateTileMap for each ship.
		/// </summary>
		/// <param name="pos"></param>
		public Ship(Vector2 pos)
		{
			shipTilemap = CreateTileMap();
			//RemoveEmptyTiles();
			Position = pos;
		}

		/// <summary>
		/// This is a required method of all Ship classes that handles the generation of the ship.
		/// </summary>
		/// <returns></returns>
		public abstract TileMap CreateTileMap();

		/// <summary>
		/// This method is called during the load phase.
		/// </summary>
		public void LoadContent()
		{
			shipTilemap.LoadContent();
		}

		/// <summary>
		/// This method is called during the draw loop.
		/// </summary>
		/// <param name="batch"></param>
		public void Draw(SpriteBatch batch)
		{
			shipTilemap.Draw(batch);
		}
		
		/// <summary>
		/// This method is called during the update loop.
		/// </summary>
		/// <param name="gameTime"></param>
		public virtual void Update(GameTime gameTime)
		{
			shipTilemap.Update(gameTime);
		}

		/// <summary>
		/// This method is a tool that allows a creator to create generic rooms based on the tilemap being edited, the coordinates on that tilemap where the room will be built, and the width and height of the room.
		/// </summary>
		/// <param name="tileMap"></param>
		/// <param name="xPos"></param>
		/// <param name="yPos"></param>
		/// <param name="xSize"></param>
		/// <param name="ySize"></param>
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
					buffer[i][j] = new Tile(i + xPos, j + yPos); //initialize the new tile
					Tile.AddTop(new TopHull(), ref buffer[i][j]); //set the new tile to have a base hull (which all actual parts of the ship should have).

					if (i == 0 || i == xSize - 1 || j == 0 || j == ySize - 1)
					{
						Tile.AddTop(new TopWall(), ref buffer[i][j]); //set the outside perimeter to be walls

					}
				}
			}

			//Console.WriteLine("xPos = {0} || yPos = {1}", (xPos), (yPos));
			tileMap.PlaceTiles(buffer, xPos, yPos); //place the new tiles onto the tilemap.

		}

		/// <summary>
		/// This is currently a WIP method that will optimize the ships tilemap to be fitted with no empty tiles outside of the smallest box to cover all non-null tiles in the tilemap.
		/// </summary>
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
