using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.Rendering;
using StarScape.Source.World.Tiles.Attributes;

namespace StarScape.Source.World.Tiles
{
	public abstract class Tile : ITile
	{

		public abstract Texture2D TileTexture { get; }
		public abstract bool DoesTextureHaveTransparency { get; }

		public HashSet<IAttribute> Attributes => new HashSet<IAttribute>();

		public abstract int Layer { get; }

		public Tile()
		{
		}
		
		public virtual void Update(GameTime gameTime) {}

	}
}
