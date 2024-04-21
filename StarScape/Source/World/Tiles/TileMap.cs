using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using StarScape.Source.Rendering;
using StarScape.Source.World.Ships;
using StarScape.Source.World.Tiles.Attributes;

namespace StarScape.Source.World.Tiles
{
	public class TileMap
	{
		private Dictionary<Tuple<int, int>, TileStack> tilemap;

		public TileMap()
		{
			tilemap = new Dictionary<Tuple<int, int>, TileStack>();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
            foreach (KeyValuePair<Tuple<int, int>, TileStack> kvp in tilemap)
            {

				Vector2 pos = new Vector2(kvp.Key.Item1 - 0.5f, kvp.Key.Item2 - 0.5f) * 64;

				if (pos.X - MainGame.ActiveCamera.Position.X <= -64 || pos.Y - MainGame.ActiveCamera.Position.Y <= -64) continue;
				if (pos.X >= MainGame.ActiveCamera.GetCameraBounds().X || pos.Y >= MainGame.ActiveCamera.GetCameraBounds().Y) continue;

				spriteBatch.Draw(kvp.Value.renderTarget, pos, Color.White);

            }
        }

		public Tile this[int x, int y, int layer]
		{
			get
			{
				Tuple<int, int> position = new Tuple<int, int>(x, y);
				if (tilemap.ContainsKey(position))
				{
					return tilemap[position][layer];
				}

				return null;
			}
			set
			{
				Tuple<int, int> position = new Tuple<int, int>(x, y);
				if (!tilemap.ContainsKey(position))
				{
					tilemap[position] = new TileStack();
				}

				tilemap[position][layer] = value;

			}
		}
	}
}
