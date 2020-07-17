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
	/// <summary>
	/// This is an abstract class that is the blueprint for all tops. A top is seen to be an object that is inside a tile, which the player should be able to do things with.
	/// Each top can have associated attributes of type Attribute that allows for more standardized actions and effects, instead of having to hardcode them into each topp type
	/// </summary>
	public abstract class Top
	{
		public Tile parentTile { get; internal set; }
		
		List<IAttribute> attributes = new List<IAttribute>();

		public int textureID { get; private set; } // an int that represents the index in the LoadHelper.textures list that allows you to retrieve the wanted texture.
		//public bool debugRenderFlag { get; set; }
		public string TopName { get; protected set; }

		protected static int MaxTopCountPerTile { get; set; }

		string texName;
		
		/// <summary>
		/// This constructor calls the abstract method GetTexture and will store it for when the LoadContent method is called.
		/// </summary>
		/// <param name="tex"></param>
		public Top()
		{
			TopName = texName = GetTexture();
		}

		public virtual string GetTexture()
		{
			return "UndefinedTexture";
		}

		/// <summary>
		/// Returns the total amount of this top that can fit in a given tile
		/// </summary>
		/// <returns></returns>
		public static int GetMaxCountPerTile()
		{
			return MaxTopCountPerTile;
		}

		/// <summary>
		/// This method is called during the load phase.
		/// </summary>
		public virtual void LoadContent()
		{
			textureID = LoadHelper.LoadTexture(texName); // ask the LoadHelper to load the texture of name texName and set the textureID to the returned index.

			//This was debug stufff to check if my Load code was working properly.

			//int testID = LoadHelper.LoadTexture(texName);
			//Debug.WriteLine("Original TexID: " + textureID + ", duplicated test id: " + testID);
		}

		/// <summary>
		/// This method is called during the Update loop.
		/// </summary>
		/// <param name="gameTime"></param>
		public virtual void Update(GameTime gameTime)
		{
			foreach(IAttribute att in attributes)
			{
				att.Update(gameTime);
			}
			//if(TopName == "FloorTile1") System.Console.WriteLine("TestTop");
		}

		/// <summary>
		/// This method is called during the draw loop.
		/// </summary>
		/// <param name="batch"></param>
		public virtual void Draw(SpriteBatch batch)
		{
			//batch.Draw(LoadHelper.GetTexture(textureID)/*Get the texture from the ID*/,
			//	(new Vector2(parentTile.xPos, parentTile.yPos) * 64 /*each tile texture is 64px wide.*/) + parentTile.parentTileMap.parentShip.Position /*add the ship position offset*/,
			//	Color.White /*this is used for tinting.*/);
		}

		/// <summary>
		/// Check if this top has an attribute. This only require inputing the Type of attribute you're looking for via the type parameter T.
		/// </summary>
		/// <example> top.HasAttribute</example>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public bool HasAttribute<T>()
		{
			for(int i = 0; i < attributes.Count; i++)
			{
				if (attributes.ElementAt(i) is T) return true;
			}

			return false;
		}

		/// <summary>
		/// Check if this top has an attribute. Used primarily to make sure the top doesn't already include the attribute trying to be added.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public bool HasAttribute<T>(T t)
		{
			for (int i = 0; i < attributes.Count; i++)
			{
				if (attributes.ElementAt(i) is T) return true;
			}

			return false;
		}

		/// <summary>
		/// Check if this top has the attribute already and if not, add the attribute and set the attributes parent to this.
		/// </summary>
		/// <param name="att"></param>
		public void AddAttribute(IAttribute att)
		{
			if (HasAttribute(att)) return;

			attributes.Add(att);
			att.parentTop = this;
		}

		/// <summary>
		/// Since each top should only have one instance of an attribute (right now at least), find the instance of the requested attribute and return it.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public IAttribute getAttribute<IAttribute>()
		{
			for(int i = 0; i < attributes.Count(); i++)
			{
				if (attributes.ElementAt(i) is IAttribute) return (IAttribute)(attributes.ElementAt(i));
			}

			return default;
		}

	}
}
