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
		
		public static Texture2D LoadTexture(string name)
		{

			if (name == null) return MainGame.contentManager.Load<Texture2D>("UndefinedTexture");

			return MainGame.contentManager.Load<Texture2D>(name); // load the texture based on it's name into a temporary texture variable.
		}
		
	}
}
