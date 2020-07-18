using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.World.Tiles.Atmospherics;
using StarScape.Source.World.Tiles.Attributes;

namespace StarScape.Source.World.Tiles
{
	/// <summary>
	/// This class is essentially an interface between the tilemap and the tops in this tile. 
	/// </summary>
	public abstract class Tile : ITile
	{
		//public Atmosphere atmosphere;

		public abstract Texture2D TileTexture { get; }
		
		public TileMap ParentTileMap { get; set; }
		//public List<Top> tops { get; private set; } //the first index should be the bottom, so usually a hull if it's on a ship.

		public List<IAttribute> Attributes { get; private set; }

		public int xPos { get; set; }//{ get; private set; }
		public int yPos { get; set; }//{ get; private set; }
		public int TileLayer { get; private set; }
		
		public Tile(int x, int y, int tileLayer)
		{
			this.xPos = x;
			this.yPos = y;
			//tops = new List<Top>();
			Attributes = new List<IAttribute>();

			TileLayer = tileLayer;

			//atmosphere = new Atmosphere(this);
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
			/*foreach(Top t in tops)
			{
				if (t == null) continue;
				t.LoadContent();
			}
			*/
			//TileTextureID = LoadHelper.LoadTexture(GetTexture());
			
		}

		/// <summary>
		/// This method is called during the draw phase.
		/// </summary>
		/// <param name="batch"></param>
		public virtual void Draw(SpriteBatch batch)
		{
			if (TileTexture != null)
			{
				batch.Draw(TileTexture/*Get the texture from the ID*/,
					(new Vector2(xPos, yPos) * 64 /*each tile texture is 64px wide.*/) + ParentTileMap.Position /*add the ship position offset*/,
					Color.White /*this is used for tinting.*/);
			}

			//if (t == null) continue;
			//if (t.TopName == "WallTile1") continue;
			//t.Draw(batch);

			if (true) //if(showAtmospherics == true) at some point.
			{
				
			}
		}
		
		public virtual void Update(GameTime gameTime) {}

		public IAttribute GetAttribute<Attribute>()
		{
			foreach(IAttribute att in Attributes)
			{
				if (att is Attribute) return (IAttribute)att;
			}
			return null;
		}

		public bool HasAttribute<IAttribute>()
		{
			foreach (IAttribute att in Attributes)
			{
				if (att is IAttribute) return true;
			}
			return false;
		}

		public void AddAttribute<Attribute>(Attribute att)
		{
			if (HasAttribute<Attribute>()) return;

			Attributes.Add((IAttribute)att);
		}

		IAttribute IAttributable.GetAttribute<IAttribute>()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// This method adds any given top to any given tile, hence the static keyword.
		/// </summary>
		/// <param name="top"></param>
		/// <param name="tile"></param>
		//public static void AddTop(Top top, ref Tile tile)
		//{
		//	if(top is TopWall)
		//	{

		//	}

		//	tile.tops.Add(top);
		//	top.parentTile = tile;
		//}

		//public static void RemoveTop(Top top, ref Tile tile)
		//{
		//	if(tile.tops.Contains(top))	tile.tops.Remove(top);

		//}

		/// <summary>
		/// Gives an easier method to get a top via index instead of having to type out tile.tops.ElementAt(i). That just becomes tile.GetTop(i).
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		//public Top GetTop(int i)
		//{
		//	return tops.ElementAt(i);
		//}

	}
}
