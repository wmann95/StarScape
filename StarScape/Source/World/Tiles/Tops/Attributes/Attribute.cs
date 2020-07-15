using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarScape.Source.World.Tiles.Tops.Attributes
{
	/// <summary>
	/// Blueprint for all attributes. It's intentionally a very simple class.
	/// </summary>
	public abstract class Attribute
	{
		public Top parentTop { get; internal set; }

		public abstract void Update(GameTime gameTime);
		
	}
}
