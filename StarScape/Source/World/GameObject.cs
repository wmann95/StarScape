using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;

using StarScape.Source.TileSystem;

namespace StarScape.Source.World
{
	public static class GameObjects
	{
		private static Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>();

		public static GameObject instantiate(string id)
		{
			return (GameObject)objects[id].Clone();
		}

		public static void RegisterGameObject(string id, GameObject obj)
		{
			objects[id] = obj;
		}

		public static void PrintRegistered()
		{
			foreach (KeyValuePair<string, GameObject> kvp in objects)
			{
				Debug.Log(string.Format("id: '{0}' type: '{1}'", kvp.Key, kvp.Value.GetType()));
			}
		}
	}
	public class GameObject : ICloneable
	{
		private Texture2D _texture = null;
		public Texture2D Texture => _texture ?? LoadHelper.LoadTexture("unknown");

		private string _texture_name = "unknown";
		public string texture
		{
			get
			{
				return _texture_name;
			}
			set
			{
				_texture_name = value;
				_texture = LoadHelper.LoadTexture(_texture_name);
			}
		}

		public TileLayer layer { get; set; }
		public string id { get; set; }
		public string name { get; set; }
		public string desc { get; set; }

		public object Clone()
		{
			return this.MemberwiseClone();
		}
	}
}
