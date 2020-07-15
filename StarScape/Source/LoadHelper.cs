using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace StarScape.Source
{
	public static class LoadHelper
	{

		static Dictionary<int, Texture2D> textures = new Dictionary<int, Texture2D>();
		static int textureIndex = 0;

		public static int LoadTexture(string name)
		{

			Texture2D t = MainGame.contentManager.Load<Texture2D>(name);
			
			textures.Add(textureIndex, t);

			return textureIndex++;
		}

		public static Texture2D GetTexture(int loadID)
		{
			return textures[loadID];
		}

	}
}
