using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StarScape.Source
{
	public static class LoadHelper
	{
		public static ContentManager contentManager;

		private static Dictionary<string, Texture2D> loaded_textures = new Dictionary<string, Texture2D>();
		

		public static Texture2D LoadTexture(string name)
		{
			if (name == null)
			{
				if (!loaded_textures.ContainsKey("undefined"))
				{
					Texture2D undefined = contentManager.Load<Texture2D>("textures/UndefinedTexture");
					loaded_textures["undefined"] = undefined;
				}

				return loaded_textures["undefined"];
			}

			if (loaded_textures.ContainsKey(name)) {  return loaded_textures[name]; }


			Texture2D texture = contentManager.Load<Texture2D>("textures/" + name);
			loaded_textures[name] = texture;

			Debug.Log("Texture loaded");

			return loaded_textures[name];
		}
		
	}
}
