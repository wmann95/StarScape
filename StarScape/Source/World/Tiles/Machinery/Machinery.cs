using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarScape.Source.World.Tiles.Tops;

namespace StarScape.Source.World.Tiles.Machinery
{
	public class Machinery : Top
	{
		protected bool isMachineOn = false;

		public Machinery()
		{
			MaxTopCountPerTile = 1;
		}
		
	}
}
