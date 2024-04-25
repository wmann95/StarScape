using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Newtonsoft.Json.Converters;

using StarScape.Source.Types;


namespace StarScape.Source.TileSystem
{
	[Serializable]
	public enum TileLayer
	{
		Space,
		Hull,
		Pipe,
		Wire,
		Floor,
		Room
	}

	public class TileStack
	{
		private Dictionary<TileLayer, Tile> tiles;

		public RenderTarget2D renderTarget;
		private SpriteBatch spriteBatch;

		private static GraphicsDevice graphicsDevice;

		static TileStack()
		{
			graphicsDevice = MainGame.graphics.GraphicsDevice;
		}

		public TileStack()
		{
			tiles = new Dictionary<TileLayer, Tile>();
			renderTarget = new RenderTarget2D(graphicsDevice, 64, 64);
		}

		~TileStack()
		{
			tiles.Clear();
			renderTarget = null;
		}


		public Tile this[TileLayer layer]
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
				else if (tiles.ContainsKey(layer))
				{
					if (tiles[layer].id == value.id) return;
				}

				tiles[layer] = value;

				UpdateStack();
			}
		}

		private void UpdateStack()
		{

			Redraw();
		}

		private void Redraw()
		{
			ImmutableArray<Tile> buffer = tiles.ToList().OrderBy(kvp => (int)kvp.Key).Select(kvp => kvp.Value).ToImmutableArray();

			MainGame.graphics.GraphicsDevice.SetRenderTarget(renderTarget);
			spriteBatch = new SpriteBatch(graphicsDevice);

			spriteBatch.Begin();

			for (int i = 0; i < buffer.Length; i++)
			{
				spriteBatch.Draw(buffer[i].Texture, Vector2.Zero, Color.White);
			}

			spriteBatch.End();

			MainGame.graphics.GraphicsDevice.SetRenderTarget(null);
		}

	}

}
