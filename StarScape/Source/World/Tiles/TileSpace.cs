using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.World.Tiles.Atmospherics;
using StarScape.Source.World.Tiles.Tops.Attributes;

namespace StarScape.Source.World.Tiles
{
	public class TileSpace : ITile
	{
		public static float AtmosphereEscapeRate = 5000;

		public TileMap ParentTileMap { get { return null; } set { } }

		public int TileTextureID { get { return -1;  } }

		public string TextureName { get { return "TileSpace"; } }

		public int xPos { get; set; }//{ get; private set; }
		public int yPos { get; set; }//{ get; private set; }

		public int TileLayer { get { return 0; } }

		public List<IAttribute> Attributes { get; private set; }

		public TileSpace()
		{
			Attributes = new List<IAttribute>();
		}

		public void AddAttribute<IAttribute>(IAttribute attribute)
		{}

		public void Draw(SpriteBatch batch)
		{}

		public void Update(GameTime gameTime)
		{}

		public IAttribute GetAttribute<IAttribute>()
		{
			return default;
		}

		public string GetTexture()
		{
			return "TileSpace";
		}

		public bool HasAttribute<IAttribute>()
		{
			return false;
		}

		public void LoadContent()
		{

		}

	}
}
