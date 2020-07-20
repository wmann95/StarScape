using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StarScape.Source.Rendering;
using StarScape.Source.World.Ships;

namespace StarScape.Source.World
{
	public class World
	{
		//List of all ships in the current world.
		List<Ship> ships = new List<Ship>();

		public static Camera2D GameCamera;

		public World(ref Camera2D cam)
		{
			GameCamera = cam;

			//ships.Add(new ShipBartox(new Vector2(0, 0)));
			ships.Add(new ShipCalax(new Vector2(0, 0))); // make a new Calax ship at the specified point.


			Debug.Log("Cam Position: " + GameCamera.Position + "  Cam Bounds: " + new Vector2(MainGame.screenWidth / GameCamera.Zoom, MainGame.screenHeight / GameCamera.Zoom) + " Camera Zoom: " + GameCamera.Zoom);
		}
		
		/// <summary>
		/// Go through all the ships and let them load.
		/// </summary>
		public void Load()
		{
			foreach (Ship ship in ships)
			{
				ship.LoadContent();
				//player.Load();
			}
		}


		/// <summary>
		/// Go through all the ships and let them render.
		/// </summary>
		public void Draw(GameTime gameTime, SpriteBatch batch)
		{
			foreach (Ship ship in ships)
			{
				ship.Draw(batch);
				//player.Load();
			}
			//batch.Draw(player.texture, new Vector2(player.xPos * 64, player.yPos * 64), Color.White);

			
		}

		Vector2 mouseRightClickedPosition = new Vector2();
		bool mouseRightFlag = false;

		/// <summary>
		/// Go through all the ships and let them update.
		/// </summary>
		public void Update(GameTime gameTime)
		{
			//Mouse.GetState();
			//Keyboard.GetState();

			//Zoom logic
			if (Keyboard.WasKeyTyped(Keys.Z))
			{
				if (Keyboard.IsKeyPressed(Keys.LeftShift))
				{
					GameCamera.Zoom *= 2;
					GameCamera.Position += (new Vector2(MainGame.screenWidth, MainGame.screenHeight) / (float)Math.Exp(GameCamera.Zoom / 2));
				}
				else
				{
					if (GameCamera.Zoom > GameCamera.MaxZoom)
					{
						GameCamera.Zoom /= 2;
						GameCamera.Position -= (new Vector2(MainGame.screenWidth, MainGame.screenHeight) / (float)Math.Exp(GameCamera.Zoom * 2));
					}
				}
			}

			//Camera drag movement.
			if (Mouse.MouseButtonDown(Mouse.MouseButton.Left))
			{
				mouseRightFlag = true;
				mouseRightClickedPosition = GameCamera.Position + Mouse.GetState().Position.ToVector2() / GameCamera.Zoom;

				Debug.Log("Mouse Clicked Position: " + mouseRightClickedPosition);
			}

			if (Mouse.IsButtonPressed(Mouse.MouseButton.Left))
			{
				if (mouseRightFlag)
				{
					GameCamera.Position = (mouseRightClickedPosition - Mouse.GetState().Position.ToVector2() / GameCamera.Zoom);
				}
			}

			if (Mouse.MouseButtonUp(Mouse.MouseButton.Left))
			{
				//Debug.Log("Mouse Released Position: " + Mouse.GetState().Position.ToVector2());
				mouseRightFlag = false;
				GameCamera.Position = (mouseRightClickedPosition - Mouse.GetState().Position.ToVector2() / GameCamera.Zoom);
				
			}
			
			foreach (Ship ship in ships)
			{
				ship.Update(gameTime);
			}

			//Debug.Log(cam.Position);
		}
	}
}
