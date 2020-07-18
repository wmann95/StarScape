using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.World.Tiles;
using StarScape.Source.World.Tiles.Tops;

namespace StarScape.Source.World.Ships
{
	/// <summary>
	/// Blueprint for all Ship classes.
	/// </summary>
	public abstract class Ship : IUpdatable, IDrawable
	{
		//The base tilemap that the ship is built onto.
		public TileMap shipTilemap;

		public Vector2 Position; //Position "in space", if you will. It's the position in the world relative to the world coordinate system.

		/// <summary>
		/// This constructor does some of the backend stuff so that a developer working on ships doesn't need to do the small things like call CreateTileMap for each ship.
		/// </summary>
		/// <param name="pos"></param>
		public Ship(Vector2 pos)
		{
			shipTilemap = CreateTileMap();
			OptimizeShip();
			Position = pos;
			shipTilemap.SetAtmospheresDirty();
		}

		public new string ToString()
		{
			return GetShipName();
		}

		public abstract string GetShipName();

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
			shipTilemap.Position = this.Position;
		}

		/// <summary>
		/// This method is a tool that allows a creator to create generic rooms based on the tilemap being edited, the coordinates on that tilemap where the room will be built, and the width and height of the room.
		/// </summary>
		/// <param name="tileMap"></param>
		/// <param name="xPos"></param>
		/// <param name="yPos"></param>
		/// <param name="xSize"></param>
		/// <param name="ySize"></param>
		public static void BuildRoom(TileMap tileMap, int xPos, int yPos, int xSize, int ySize)
		{

			TileMap buffer = new TileMap(xSize, ySize);
			

			for (int i = 0; i < xSize; i++)
			{
				for (int j = 0; j < ySize; j++)
				{
					buffer.PlaceTile(new TileHull(i, j), true);
					//Tile.AddTop(new TopHull(), ref buffer[i][j]); //set the new tile to have a base hull (which all actual parts of the ship should have).

					if (i == 0 || i == xSize - 1 || j == 0 || j == ySize - 1)
					{
						//Tile.AddTop(new TopWall(), ref buffer[i][j]); //set the outside perimeter to be walls
						buffer.PlaceTile(new TileWall(i, j), true);
					}
					else
					{
						//Tile.AddTop(new TopFloor(), ref buffer[i][j]); //Tile isn't a perimeter tile, add a floor.
						buffer.PlaceTile(new TileFloor(i, j), true);
					}
				}
			}

			//Console.WriteLine("xPos = {0} || yPos = {1}", (xPos), (yPos));
			tileMap.PasteTiles(buffer.ToArray(), xPos, yPos); //place the new tiles onto the tilemap.


		}

		/// <summary>
		/// WIP method for adding doors.
		/// </summary>
		/// <param name="tileMap"></param>
		/// <param name="xPos"></param>
		/// <param name="yPos"></param>
		/// <param name="rotation"></param>
		public static void BuildDoor(in TileMap tileMap, int xPos, int yPos, int rotation)
		{
			//List<Top> tops = tileMap.GetTile(xPos, yPos).tops;
			//bool hullFlag = false;
			//bool wallFlag = false;

			//foreach(Top t in tops)
			//{
			//	if(t is TopHull)
			//	{
			//		hullFlag = true;
			//	}
			//	if (t is TopWall)
			//	{
			//		wallFlag = true;
			//	}

			//}

			//if (wallFlag)
			//{
				//tileMap.GetTile(xPos, yPos).tops.Remove<TopWall>();
			//}

			//if (hullFlag)
			//{
			//	tileMap.AddTop(xPos, yPos, new TopDoor());
			//}

		}

		/// <summary>
		/// This is currently a WIP method that will optimize the ships tilemap to be fitted with no empty tiles outside of the smallest box to cover all non-null tiles in the tilemap.
		/// </summary>
		private void OptimizeShip()
		{
			Stopwatch timer = new Stopwatch();
			timer.Start();

			int lowestX = shipTilemap.GetXSize();
			int lowestY = shipTilemap.GetYSize();
			int highestX = 0;
			int highestY = 0;
			

			// remove all empty tiles
			for (int x = 0; x < shipTilemap.GetXSize(); x++)
			{
				for (int y = 0; y < shipTilemap.GetYSize(); y++)
				{
					bool tileNotEmptyFlag = false;
					for(int z = 0; z < shipTilemap.MaxHeightOfTileMap; z++)
					{
						if (shipTilemap.GetTile(x, y, z) != null && !(shipTilemap.GetTile(x, y, z) is TileSpace))
						{
							tileNotEmptyFlag = true;
							break;
						}
					}

					if (!tileNotEmptyFlag)
					{
						shipTilemap.RemoveAllTilesAt(x, y);
					}
					else
					{
						if (lowestX >= x) lowestX = x;
						if (lowestY >= y) lowestY = y;
						if (highestX <= x) highestX = x;
						if (highestY <= y) highestY = y;
					}
				}
			}

			//find the bounds of the actual ship
			for (int x = 0; x < shipTilemap.GetXSize(); x++)
			{
				for (int y = 0; y < shipTilemap.GetYSize(); y++)
				{
					for (int z = 0; z < shipTilemap.MaxHeightOfTileMap; z++)
					{
						if (shipTilemap.GetTile(x, y, z) != null)
						{
							//if (lowestX >= x) lowestX = x;
							//if (lowestY >= y) lowestY = y;
							//if (highestX <= x) highestX = x;
							//if (highestY <= x) highestY = y;
						}
					}
				}
			}

			Console.WriteLine("Ship: " + GetShipName());
			Console.WriteLine("Low X: " + lowestX);
			Console.WriteLine("Low Y: " + lowestY);
			Console.WriteLine("High X: " + highestX);
			Console.WriteLine("High Y: " + highestY);
			
			int newXSize = highestX - lowestX + 1; 
			int newYSize = highestY - lowestY + 1;

			TileMap buffer = new TileMap(newXSize, newYSize); // Make a new tilemap based on the optimized box.
			buffer.PasteTiles(shipTilemap.GetTiles(lowestX, lowestY, highestX, highestY), 0, 0); // get the current unoptimized tilemaps tiles and place them onto the buffer.
			
			this.shipTilemap = buffer; // set this tilemap to the optimized tilemap.
			
			Console.WriteLine("Ship: " + GetShipName());
			Console.WriteLine("X Size: " + shipTilemap.GetXSize());
			Console.WriteLine("Y Size: " + shipTilemap.GetYSize());

			buffer = null; // Dispose of the buffer so that the trash collector can take care of the empty object.

			timer.Stop();

			Console.WriteLine("Optimization Ended after " + timer.ElapsedMilliseconds + " milliseconds.");
		}
	}
}
