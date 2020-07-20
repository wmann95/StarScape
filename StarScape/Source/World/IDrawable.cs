﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.Rendering;

namespace StarScape.Source.World
{
	public interface IDrawable
	{
		void Draw(SpriteBatch batch);
	}
}
