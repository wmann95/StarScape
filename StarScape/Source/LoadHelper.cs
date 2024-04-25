using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StarScape.Source
{
	public static class LoadHelper
	{
		/// <summary>
		/// The content manager caches all loaded content, so manual caching is unnecessary.
		/// </summary>
		public static ContentManager Content;

		public static readonly Dictionary<string, string> JsonVariables = new Dictionary<string, string>() {
			{"id", "id"},
			{"name", "name"},
			{"desc", "description"},
			{"texture", "texture_name"},
			{"layer", "layer"}
		};


		public static Texture2D LoadTexture(string name)
		{
			if (name == null)
			{
				return Content.Load<Texture2D>("textures/UndefinedTexture");
			}

			return Content.Load<Texture2D>("textures/" + name);
		}
	}
}
