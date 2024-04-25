using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using StarScape.Source.TileSystem;
using StarScape.Source.Types;

namespace StarScape.Source.EventSystem.Events
{
    public class ShipEvent
	{
		public readonly TileMap tileMap;
		public readonly Tile newTile;
		public readonly Tuple<int, int> tile_position;
		public readonly int layer;

		public ShipEvent(TileMap tileMap, Tile tile, Tuple<int, int> tile_position, int layer)
		{
			this.tileMap = tileMap;
			this.newTile = tile;
			this.tile_position = tile_position;
			this.layer = layer;
		}
	}
}
