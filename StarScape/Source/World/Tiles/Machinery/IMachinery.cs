using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarScape.Source.World.Tiles.Machinery
{
	interface IMachinery : ITile
	{
		bool IsMachineOn { get; }
		
	}
}
