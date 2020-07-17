using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace StarScape.Source
{
	/// <summary>
	/// This class makes it easier to load and register textures that can be retrieved via that textures ID
	/// </summary>
	public static class LoadHelper
	{

		static Dictionary<int, Texture2D> textures = new Dictionary<int, Texture2D>(); // Keeps textures easily accessible with a integer id used to look up the texture.
		static int textureIndex = 0; //keeps track of the next ID to be registered.

		public static int LoadTexture(string name)
		{

			Texture2D t = MainGame.contentManager.Load<Texture2D>(name); // load the texture based on it's name into a temporary texture variable.

			if (textures.ContainsValue(t)) return textures.ToDictionary(pair => pair.Value, pair => pair.Key)[t]; //checks to see if the textures already been loaded, and if it has, return the ID of that texture.

			textures.Add(textureIndex, t); //Texture wasn't found already, create a new entry.

			return textureIndex++; //return the texture ID that the texture was stored under and increments the index up to get ready for the next load texture load call.
		}

		/// <summary>
		/// Gets the texture from the dictionary from the TextureID
		/// </summary>
		/// <param name="TextureID"></param>
		/// <returns>Texture2D</returns>
		public static Texture2D GetTexture(int textureID)
		{
			if (textureID >= textureIndex) return null; //Since the textureIndex increments with every load, there must be entries for each integer below the textureIndex,
														//therefore, if the textureID used is above or equal to the texture index, a correlated texture must not exist.
														//This should be more efficient than trying to see if the dictionary contains a key because it doesn't have to check all of the keys
														//to find a match, but rather just the textureIndex.
			
			return textures[textureID]; //return the texture.
		}


		
	}
}
