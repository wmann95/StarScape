
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

		public Texture2D texture { get; private set; }
		//public bool debugRenderFlag { get; set; }
		public string TopName { get; protected set; }

		string texName;
		
		public Top(string tex)
		{
			TopName = texName = tex;
			
		}

		public void LoadContent()
		{
			
			texture = LoadHelper.Load<Texture2D>(texName);
		}

		public void AddAttribute(Attribute att)
		{
			if (hasAttribute(att)) return;
			
			attributes.Add(att);
			att.parentTop = this;
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
			batch.Draw(texture, (new Vector2(parentTile.xPos, parentTile.yPos) * 64) + parentTile.parentTileMap.parentShip.Position, Color.White);
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
