using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.Rendering;
using StarScape.Source.World.Ships;
using StarScape.Source.World.Tiles.Atmospherics;
using StarScape.Source.World.Tiles.Attributes;

namespace StarScape.Source.World.Tiles
{
	/// <summary>
	/// This class is a sort of List<T>() specifically for tiles.
	/// </summary>
	public class TileMap : IDrawable
	{

		Dictionary<int, Dictionary<int, List<ITile>>> tilemap = new Dictionary<int, Dictionary<int, List<ITile>>>();

		Atmosphere[][] atmosphereMap;

		public Texture2D AtmosphereTexture { get { return LoadHelper.LoadTexture("AtmosphereOverlay"); } }

		public Vector2 Position { get; set; }

		public int MaxHeightOfTileMap
		{
			get
			{
				return 32;
			}
		}

		/// <summary>
		/// TileMap constructor that takes in two ints, the width and height, and the parent ship.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="ship"></param>
		public TileMap()
		{
/*
			atmosphereMap = new Atmosphere[x][];
			for (int i = 0; i < x; i++)
			{
				atmosphereMap[i] = new Atmosphere[y];
				
				for(int j = 0; j < atmosphereMap[i].Length; j++)
				{
					atmosphereMap[i][j] = new Atmosphere(this, i, j);
				}
			}
*/
		}

		public ref Atmosphere GetAtmosphere(int x, int y)
		{
			return ref atmosphereMap[x][y];
		}

		public void SetAtmospheresDirty()
		{
			for(int i = 0; i < atmosphereMap.Length; i++)
			{
				for (int j = 0; j < atmosphereMap[0].Length; j++)
				{
					atmosphereMap[i][j].setDirty();
				}
			}
		}

		public ITile GetTile(int xPos, int yPos, int zLayer)
		{
			if (tilemap.ContainsKey(yPos))
			{
				if (tilemap[yPos].ContainsKey(xPos)){
					if (zLayer < tilemap[yPos][xPos].Count)
					{
						return tilemap[yPos][xPos][zLayer];
					}
				}
			}

			return null;
		}

		/// <summary>
		/// This method is called during the load phase.
		/// </summary>
		public void LoadContent()
		{
/*
			foreach(ITile[][] xIndex in tileMap)
			{
				foreach(ITile[] yIndex in xIndex)
				{
					foreach (ITile zIndex in yIndex)
					{
						if (zIndex == null) continue;
						zIndex.LoadContent();
					}
				}
			}
*/
		}

		public void Draw(SpriteBatch batch)
		{

			foreach(KeyValuePair<int, Dictionary<int, List<ITile>>> row in tilemap)
			{
				foreach (KeyValuePair<int, List<ITile>> column in row.Value)
				{

				}
			}
			/*
			for(int x = 0; x < GetXSize(); x++) {

				for(int y = 0; y < GetYSize(); y++) {

					float xPosition = x * 64 + Position.X; 
					float yPosition = y * 64 + Position.Y;

					if (xPosition - World.GameCamera.Position.X <= -64 || yPosition - World.GameCamera.Position.Y <= -64) continue;
					if (xPosition >= World.GameCamera.GetCameraBounds().X || yPosition >= World.GameCamera.GetCameraBounds().Y) continue;
					//if (yPosition - (World.GameCamera.GetCameraBounds().Y) >= 116 / World.GameCamera.Zoom) continue;

					int[] layerArray = new int[MaxHeightOfTileMap];
					int layersToRenderCount = 0;

					for (int z = MaxHeightOfTileMap - 1; z >= 0; z--)
					{
						if (tileMap[x][y][z] == null) continue;

						if (tileMap[x][y][z].DoesTextureHaveTransparency)
						{
							layerArray[layersToRenderCount++] = z;
						}
						else
						{
							layerArray[layersToRenderCount++] = z;
							break;
						}
					}

					for (int i = layersToRenderCount - 1; i >= 0; i--)
					{


						tileMap[x][y][layerArray[i]].Draw(batch);
					}


				}

			}

			for(int x = 0; x < atmosphereMap.Length; x++) {
				for(int y = 0; y < atmosphereMap[0].Length; y++) {

					float xPosition = x * 64 + Position.X;
					float yPosition = y * 64 + Position.Y;

					if (xPosition - World.GameCamera.Position.X <= -64 || yPosition - World.GameCamera.Position.Y <= -64) continue;
					if (xPosition >= World.GameCamera.GetCameraBounds().X || yPosition >= World.GameCamera.GetCameraBounds().Y) continue;

					float pressureColor = atmosphereMap[x][y].airPressure / Atmosphere.AtmosphericPressure;

					Color color;// = new Color(255, 255, 255);
					float opacity = 0.5f;
					//Console.WriteLine(pressureColor);

					if (pressureColor > 1)
					{
						color = new Color(1 / pressureColor, 1 / pressureColor, 1f);
					}
					else
					{
						color = new Color(1f, pressureColor, pressureColor);
					}

					//if (atmos.isTileAtmosphereDirty) color = Color.Purple;

					batch.Draw(AtmosphereTexture, new Vector2(atmosphereMap[x][y].xPos, atmosphereMap[x][y].yPos) * 64 + Position, color * opacity);
				}
			}
			*/
		}

		long updateClock = 0;

		Stopwatch watch = new Stopwatch();

		/// <summary>
		/// This method is called during the update loop.
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime)
		{
			watch.Reset();
			watch.Start();

			foreach (ITile[][] xIndex in tileMap)
			{
				foreach (ITile[] yIndex in xIndex)
				{
					foreach (ITile zIndex in yIndex)
					{
						if (zIndex == null) continue;
						zIndex.Update(gameTime);
					}
				}
			}

			foreach (Atmosphere[] aArray in atmosphereMap)
			{
				foreach (Atmosphere atmos in aArray)
				{
					atmos.Update(gameTime);
				}
			}

			watch.Stop();
			if(Time.gameTime - updateClock >= 500)
			{
				updateClock = 0;
				//Debug.Log(watch.Elapsed);
			}
		}
		
		/// <summary>
		/// set the given tile to null, which, for the intents and purposes of the air pressure and other mechanics, acts as a "space" tile. (removes any air rapidly.)
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void RemoveTile(int x, int y, int z)
		{
			if(z == 0 || z == 1) // If you're removing the base ground or the hull, just return space.
			{
				tileMap[x][y][z] = new TileSpace();
				atmosphereMap[x][y].setDirty();
				//Debug.Log("i is: " + z + ", Create space at: " + x + ", " + y);
				return;
			}
			//Console.WriteLine("This tile: " + tiles[x][y].ToString());

			tileMap[x][y][z] = null;

			atmosphereMap[x][y].setDirty();
		}

		public void RemoveAllTilesAt(int x, int y)
		{
			for (int i = 0; i < MaxHeightOfTileMap; i++)
			{
				RemoveTile(x, y, i);
			}

			//SetAtmospheresDirty();
		}
		
		public void PlaceTile(ITile tile, bool replaceIfNeeded = false)
		{
			if (tile == null) throw new ArgumentException("Tile Cannot Be Null");
			if (tile is TileSpace) RemoveAllTilesAt(tile.xPos, tile.yPos);

			if (GetTile(tile.xPos, tile.yPos, tile.TileLayer) == null || replaceIfNeeded)
			{
				//Debug.Log(tile.TileLayer);
				tileMap[tile.xPos][tile.yPos][tile.TileLayer] = tile;
				tileMap[tile.xPos][tile.yPos][tile.TileLayer].ParentTileMap = this;

				if (tile.HasAttribute<AttAirtight>())
				{

					atmosphereMap[tile.xPos][tile.yPos].SetAirtight(true);
				}
				else
				{
					atmosphereMap[tile.xPos][tile.yPos].setDirty();
				}

			}
		}

		/// <summary>
		/// essentially overwrites any of the tile slots on the tilemap via a new tile array and position of the top left-hand corner on the original tilemap.
		/// </summary>
		/// <param name="inTiles"></param>
		/// <param name="xPos"></param>
		/// <param name="yPos"></param>
		public void PasteTiles(ITile[][][] inTiles, int xPos, int yPos)
		{

			//Console.WriteLine("TileMap xPos  = {0} || TileMap yPos = {1}", (xPos), (yPos));
			//Console.WriteLine("TileMap width  = {0} || TileMap height = {1}", (tiles.Length), (tiles[0].Length));
			//Console.WriteLine("TileMap inTiles.width  = {0} || TileMap inTiles.height = {1}", (inTiles.Length), (inTiles[0].Length));

			for (int i = 0; i < inTiles.Length; i++)
			{
				for(int j = 0; j < inTiles[i].Length; j++)
				{
					for (int k = 0; k < inTiles[i][j].Length; k++)
					{

						if (i + xPos < 0 || i + xPos >= tileMap.Length | j + yPos < 0 || j + yPos >= tileMap[i].Length)
						{
							Debug.Log("PlaceTiles Error: Input points out-of-bounds.", true);
							continue;
						}
						
						if (inTiles[i][j][k] == null) continue;

						inTiles[i][j][k].xPos = i + xPos;
						inTiles[i][j][k].yPos = j + yPos;

						//Debug.Log(inTiles[i][j][k].xPos);
						//Debug.Log(inTiles[i][j][k].yPos);

						PlaceTile(inTiles[i][j][k], true);

						if (tileMap[i + xPos][j + yPos][k] == null) continue;

						tileMap[i + xPos][j + yPos][k].xPos = i + xPos;
						tileMap[i + xPos][j + yPos][k].yPos = j + yPos;
						tileMap[i + xPos][j + yPos][k].ParentTileMap = this;
					}
				}
			}


		}

		public ITile[][][] GetTiles(int x1, int y1, int x2, int y2)
		{
			ITile[][][] buffer = new ITile[x2 - x1 + 1][][];
			for(int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = new ITile[y2 - y1 + 1][];

				for(int j = 0; j < buffer[i].Length; j++)
				{
					buffer[i][j] = new ITile[MaxHeightOfTileMap];
				}
			}

			for(int i = 0; i < buffer.Length; i++)
			{
				for (int j = 0; j < buffer[0].Length; j++)
				{
					for(int k = 0; k < MaxHeightOfTileMap; k++)
					{
						buffer[i][j][k] = tileMap[x1 + i][y1 + j][k];
					}
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
		public Point GetNeighborPosition(int xPos, int yPos, int neighbor)
		{
			//Console.WriteLine(tile.xPos);
			
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
			if (xPos + xOffset < 0 || xPos + xOffset >= GetXSize())
			{
				return new Point(-1, -1);
			}
			else if (yPos + yOffset < 0 || yPos + yOffset >= GetYSize())
			{
				return new Point(-1, -1);
			}

			return new Point(xPos + xOffset, yPos + yOffset);
		}
		

	}
}
