using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StarScape.Source.EventSystem.Events;
using StarScape.Source.Rendering;
using StarScape.Source.TileSystem;
using StarScape.Source.Types;
using StarScape.Source.Types.Rooms;

namespace StarScape.Source.World.Ships
{
    /// <summary>
    /// Blueprint for all Ship classes.
    /// </summary>
    public abstract class Ship : IUpdatable
	{
		public event EventHandler<ShipEvent> OnShipChanged;

		public TileMap tilemap = new TileMap();

		public Vector2 Position;

		public string Name { get; private set; }

		public Ship(Vector2 pos)
		{
			CreateTileMap();
			Position = pos;
			OnShipChanged += Room.OnTilePlaced;
		}

		public new string ToString()
		{
			return Name;
		}

		public virtual void Update(GameTime gameTime)
		{
			//shipTilemap.Update(gameTime);
			//shipTilemap.Position = this.Position;
		}

		public void LoadContent()
		{
			//shipTilemap.LoadContent();
		}

		public void Draw(SpriteBatch batch)
		{
			tilemap.Draw(batch);
		}

		private void CreateTileMap()
		{

		}

		public Vector2 MousePositionToTilePosition(Vector2 pos)
		{
			return (pos + MainGame.ActiveCamera.Position) / 64f;
		}

		public void AddTile(Vector2 position, Tile tile)
		{
			int x = (int)MathF.Round(position.X);
			int y = (int)MathF.Round(position.Y);
			TileLayer z = tile.layer;

			tilemap[x, y, z] = tile;
		}


	}
}
