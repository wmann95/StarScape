﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.World.Tiles.Tops.Attributes;

namespace StarScape.Source.World.Tiles.Tops
{
	/// <summary>
	/// Essentially a placeholder tile that designates a tile as part of the ship.
	/// </summary>
	public class TopHull : Top
	{

		public TopHull()
		{
			//this.AddAttribute(new AttAirPressure()); // This is just a test thing... hulls won't come with added air pressure on installing.
		}


		public override string GetTexture()
		{
			return "HullTile1";
		}

		public override void Update(GameTime gameTime)
		{}
	}
}
