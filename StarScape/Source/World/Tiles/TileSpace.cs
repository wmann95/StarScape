using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.World.Tiles.Attributes;

namespace StarScape.Source.World.Tiles
{
	public class TileSpace : ITile
	{

		public TileMap ParentTileMap { get { return null; } set { } }

		public int TileTextureID { get { return -1;  } }

		public string TextureName { get { return "TileSpace"; } }

		public int xPos { get; set; }//{ get; private set; }
		public int yPos { get; set; }//{ get; private set; }

		public int TileLayer { get; private set; }

		public List<IAttribute> Attributes { get; private set; }

		public Texture2D TileTexture { get { return null; } }

		public bool DoesTextureHaveTransparency { get { return true; } }

		public TileSpace()
		{
			Attributes = new List<IAttribute>();
			TileLayer = 0;
		}

		public TileSpace(int x, int y)
		{
			xPos = x;
			yPos = y;

			Attributes = new List<IAttribute>();
			TileLayer = 0;
		}

		public int GetTileLayer()
		{
			return 0;
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
