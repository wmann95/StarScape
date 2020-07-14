using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarScape.Source.World.Tiles.Tops.Attributes;

namespace StarScape.Source.World.Tiles.Tops
{
	public class TopWall : Top
	{
		public TopWall() : base("WallTile1")
		{
			AddAttribute(new AttAirtight());
		}
	}
}
