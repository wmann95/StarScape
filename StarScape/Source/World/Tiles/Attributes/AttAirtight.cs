using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarScape.Source.World.Tiles.Attributes
{
	/// <summary>
	/// Placeholder attribute that will be used for walls and hulls to make sure the air pressure on the tile knows it's not connected directly to space.
	/// </summary>
	public class AttAirtight : IAttribute
	{

		public void Update(GameTime gameTime)
		{
			//parentTop.parentTile.atmosphere.SetAirtight();
		}
	}
}
