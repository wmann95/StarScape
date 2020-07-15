﻿
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.World.Tiles.Tops.Attributes;

namespace StarScape.Source.World.Tiles.Tops
{
	//A top is a thing that makes up a tile. It goes *on top of* the tile, and is interactable, usually.
	public abstract class Top
	{
		public Tile parentTile { get; internal set; }

		List<Attribute> attributes = new List<Attribute>();

		public int textureID { get; private set; }
		//public bool debugRenderFlag { get; set; }
		public string TopName { get; protected set; }

		string texName;
		
		public Top(string tex)
		{
			TopName = texName = tex;
		}

		public Top()
		{

		}

		public virtual void LoadContent()
		{
			
			textureID = LoadHelper.LoadTexture(texName);
		}


		public virtual void Update(GameTime gameTime)
		{
			foreach(Attribute att in attributes)
			{
				att.Update(gameTime);
			}
			//if(TopName == "FloorTile1") System.Console.WriteLine("TestTop");
		}

		public virtual void Draw(SpriteBatch batch)
		{
			batch.Draw(LoadHelper.GetTexture(textureID), (new Vector2(parentTile.xPos, parentTile.yPos) * 64) + parentTile.parentTileMap.parentShip.Position, Color.White);
		}

		public bool hasAttribute<T>()
		{
			for(int i = 0; i < attributes.Count; i++)
			{
				if (attributes.ElementAt(i) is T) return true;
			}

			return false;
		}

		public bool hasAttribute<T>(T t)
		{
			for (int i = 0; i < attributes.Count; i++)
			{
				if (attributes.ElementAt(i) is T) return true;
			}

			return false;
		}

		public void AddAttribute(Attribute att)
		{
			if (hasAttribute(att)) return;

			attributes.Add(att);
			att.parentTop = this;
		}

		public Attribute getAttribute<T>()
		{
			for(int i = 0; i < attributes.Count(); i++)
			{
				if (attributes.ElementAt(i) is T) return attributes.ElementAt(i);
			}

			return null;
		}

	}
}