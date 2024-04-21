using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.World.Tiles.Attributes;

namespace StarScape.Source.World.Tiles
{
	public interface ITile : IUpdatable, IDrawable, IAttributable
	{
		bool DoesTextureHaveTransparency { get; }
		
	}
}
