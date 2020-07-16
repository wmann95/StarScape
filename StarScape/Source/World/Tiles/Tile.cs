using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.World.Tiles.Atmospherics;
using StarScape.Source.World.Tiles.Tops;

namespace StarScape.Source.World.Tiles
{
	/// <summary>
	/// This class is essentially an interface between the tilemap and the tops in this tile. 
	/// </summary>
	public class Tile
	{
		public Atmosphere atmosphere;
		public int atmosphereTexID;

		public TileMap parentTileMap { get; internal set; }
		public List<Top> tops { get; private set; } //the first index should be the bottom, so usually a hull if it's on a ship.
		
		public int xPos { get; set; }//{ get; private set; }
		public int yPos { get; set; }//{ get; private set; }

		//there should never be a tile that has any internal coordinates of less than 0, therefore this can
		//be used as a checker kind of like checking if an object is null. If the current tile is tileSpace, do
		//stuff that would be done if it were actually space.
		public static Tile tileSpace = new TileSpace();
		
		public Tile(int x, int y)
		{
			this.xPos = x;
			this.yPos = y;
			tops = new List<Top>();

			atmosphere = new Atmosphere(this);
		}
		
		public override string ToString()
		{
			
			return "TilePosition: <" + xPos + ", " + yPos + ">";
		}

		public void LoadFromFile() { } // Placeholder for getting tile information from a save file or what have you. Haven't gotten into that yet, hence the emptyness.

		/// <summary>
		/// This method is called during the load phase.
		/// </summary>
		public void LoadContent()
		{
			foreach(Top t in tops)
			{
				if (t == null) continue;
				t.LoadContent();
			}
			
			atmosphereTexID = LoadHelper.LoadTexture("AtmosphereOverlay");
		}

		/// <summary>
		/// This method is called during the draw phase.
		/// </summary>
		/// <param name="batch"></param>
		public void Draw(SpriteBatch batch)
		{
			foreach (Top t in tops)
			{
				if (t == null) continue;
				//if (t.TopName == "WallTile1") continue;
				t.Draw(batch);
			}

			if (true) //if(showAtmospherics == true) at some point.
			{
				float pressureColor = atmosphere.airPressure / Atmosphere.AtmosphericPressure;

				Color color;// = new Color(255, 255, 255);
				float opacity = 0f;
				//Console.WriteLine(pressureColor);

				if (pressureColor > 1)
				{
					color = new Color(1 / pressureColor, 1 / pressureColor, 1f);
				}
				else
				{
					color = new Color(1f, pressureColor, pressureColor);
				}

				batch.Draw(LoadHelper.GetTexture(atmosphereTexID), new Vector2(xPos, yPos) * 64 + parentTileMap.parentShip.Position, color * 0.5f);

			}
		}

		/// <summary>
		/// this method is called during the update loop.
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime)
		{
			atmosphere.Update(gameTime);

			foreach(Top t in tops)
			{
				if (t == null) continue;
				//Console.WriteLine(t.TopName);
				//if (t is TopFloor) ((TopFloor)t).Update(gameTime);
				t.Update(gameTime);
			}
			
		}

		/// <summary>
		/// This method adds any given top to any given tile, hence the static keyword.
		/// </summary>
		/// <param name="top"></param>
		/// <param name="tile"></param>
		public static void AddTop(Top top, ref Tile tile)
		{
			if(top is TopWall)
			{

			}
			
			tile.tops.Add(top);
			top.parentTile = tile;
		}

		public static void RemoveTop(Top top, ref Tile tile)
		{
			if(tile.tops.Contains(top))	tile.tops.Remove(top);

		}

		/// <summary>
		/// Gives an easier method to get a top via index instead of having to type out tile.tops.ElementAt(i). That just becomes tile.GetTop(i).
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		public Top GetTop(int i)
		{
			return tops.ElementAt(i);
		}

	}
}
