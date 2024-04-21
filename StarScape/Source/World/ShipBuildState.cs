using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using StarScape.Source.EventSystem.Events;
using StarScape.Source.Input;
using StarScape.Source.Rendering;
using StarScape.Source.World.Ships;
using StarScape.Source.World.Tiles;

namespace StarScape.Source.World
{
    public class ShipBuildState
	{
		Ship active_ship;

		Vector2 mouseRightClickedPosition = new Vector2();
		bool mouseRightFlag = false;

		public ShipBuildState()
		{

			active_ship = new ShipBuild();
			MainGame.MouseManager.OnMouseButtonPressed += this.OnMouseButtonPressed;
			MainGame.MouseManager.OnMouseButtonDown += this.OnMouseButtonDown;
			MainGame.MouseManager.OnMouseButtonReleased += this.OnMouseButtonReleased;
		}

		public void OnMouseButtonPressed(object sender, MouseEvent e)
		{
			if (e.mouseButton == MouseButton.Left)
			{
				Tile tile = new TileHull();
				Vector2 tilePosition = active_ship.MousePositionToTilePosition(e.position);
				active_ship.AddTile(new Vector2(tilePosition.X, tilePosition.Y), tile);
			}

			if (e.mouseButton == MouseButton.Middle)
			{
				Tile tile = new TileFloor();
				Vector2 tilePosition = active_ship.MousePositionToTilePosition(e.position);
				active_ship.AddTile(new Vector2(tilePosition.X, tilePosition.Y), tile);
			}

			if (e.mouseButton == MouseButton.Right)
			{
				mouseRightFlag = true;
				mouseRightClickedPosition = MainGame.ActiveCamera.Position + e.position / MainGame.ActiveCamera.Zoom;
			}
		}

		public void OnMouseButtonDown(object sender, MouseEvent e)
		{
			if(e.mouseButton == MouseButton.Right) {
				if (mouseRightFlag)
				{
					MainGame.ActiveCamera.Position = (mouseRightClickedPosition - e.position / MainGame.ActiveCamera.Zoom);
				}
			}
			
		}
		public void OnMouseButtonReleased(object sender, MouseEvent e)
		{
			if(e.mouseButton == MouseButton.Right)
			{
				mouseRightFlag = false;
				MainGame.ActiveCamera.Position = (mouseRightClickedPosition - e.position / MainGame.ActiveCamera.Zoom);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			active_ship.Draw(spriteBatch);
		}

		public void Update(GameTime gameTime)
		{
			Keyboard.GetState();

			//Zoom logic
			if (Keyboard.WasKeyTyped(Keys.Z))
			{
				if (Keyboard.IsKeyPressed(Keys.LeftShift))
				{
					Vector2 oldSize = MainGame.ActiveCamera.GetCameraBounds() - MainGame.ActiveCamera.Position;
					MainGame.ActiveCamera.Zoom *= 1.5f;
					Vector2 newSize = MainGame.ActiveCamera.GetCameraBounds() - MainGame.ActiveCamera.Position;
					MainGame.ActiveCamera.Position += (oldSize - newSize) / 2;
				}
				else
				{
					if (MainGame.ActiveCamera.Zoom > MainGame.ActiveCamera.MaxZoom)
					{
						Vector2 oldSize = MainGame.ActiveCamera.GetCameraBounds() - MainGame.ActiveCamera.Position;
						MainGame.ActiveCamera.Zoom /= 1.5f;

						Vector2 newSize = MainGame.ActiveCamera.GetCameraBounds() - MainGame.ActiveCamera.Position;

						MainGame.ActiveCamera.Position += (oldSize - newSize) / 2;
					}
				}
				Debug.Log("Camera zoom: " + MainGame.ActiveCamera.Zoom);
			}

		}
	}
}
