using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using StarScape.Source.EventSystem.Events;

namespace StarScape.Source.Types.Rooms
{
	public class Room
	{


		public Room()
		{
		}

		public static void OnTilePlaced(object sender, ShipEvent e)
		{
			//if(e.newTile.IsAirtight)
			//{
			//	//Room room = roomCheck(e);
			//}
		}

		private static void roomCheck(ShipEvent e)
		{
			//Dictionary<Tuple<int, int>, bool> checked_positions = new Dictionary<Tuple<int, int>, bool>();
			//Queue<Tuple<int, int>> positions = new Queue<Tuple<int, int>>();
			//positions.Enqueue(e.tile_position);

			//while (positions.Count > 0)
			//{
			//	Tuple<int, int> current_position = positions.Dequeue();

			//	if (!e.tileMap.ContainsKey(current_position))
			//	{
			//		return null;
			//	}
			//}
		}
	}
}
