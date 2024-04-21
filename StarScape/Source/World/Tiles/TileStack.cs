using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarScape.Source.World.Tiles
{
	internal class TileStack
	{
		private Dictionary<int, Tile> tiles;

		public RenderTarget2D renderTarget;
		private SpriteBatch spriteBatch;

		private GraphicsDevice graphicsDevice;

		public TileStack()
		{
			tiles = new Dictionary<int, Tile>();
			graphicsDevice = MainGame.graphics.GraphicsDevice;
			renderTarget = new RenderTarget2D(graphicsDevice, 64, 64);
		}

		public Tile this[int layer]
		{
			get
			{
				try
				{
					return tiles[layer];
				}
				catch
				{
					return null;
				}
			}
			set
			{
				if (value == null) { tiles.Remove(layer); }
				else if (layer < 0 || layer > 12) { return; }
				else if (tiles.ContainsKey(layer))
				{
					if (tiles[layer].GetType() == value.GetType()) return;
				}

				tiles[layer] = value;
				Debug.Log(layer);

				ImmutableArray<Tile> buffer = tiles.ToList().OrderBy( kvp => -kvp.Key ).Select(kvp => kvp.Value).ToImmutableArray();

				MainGame.graphics.GraphicsDevice.SetRenderTarget(renderTarget);
				spriteBatch = new SpriteBatch(graphicsDevice);

				spriteBatch.Begin();

				for(int i = 0; i < buffer.Length; i++)
				{
					spriteBatch.Draw(buffer[i].TileTexture, Vector2.Zero, Color.White);
					if (!buffer[i].DoesTextureHaveTransparency) break;
				}

				spriteBatch.End();

				MainGame.graphics.GraphicsDevice.SetRenderTarget(null);
			}
		}

	}

}
