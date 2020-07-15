﻿using System;
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
using StarScape.Source.World.Tiles.Tops;

namespace StarScape.Source.World
{
	public class World
	{

		List<Ship> ships = new List<Ship>();

		public World()
		{
			//map = new TileMap();

			//player = new Player(0,0);
			//ships.Add(new ShipBartox(new Vector2(200f, 300f)));
			ships.Add(new ShipCalax(new Vector2(200f, 100f)));
		}
		

		public void Load()
		{
			foreach (Ship ship in ships)
			{
				ship.LoadContent();
				//player.Load();
			}
		}

		public void Draw(GameTime gameTime, SpriteBatch batch)
		{
			//map.Draw(batch);
			foreach (Ship ship in ships)
			{
				ship.Draw(batch);
				//player.Load();
			}
			//batch.Draw(player.texture, new Vector2(player.xPos * 64, player.yPos * 64), Color.White);
		}

		Vector2 mouseRightClickedPosition = new Vector2();
		Vector2 cameraOriginalPosition = new Vector2();
		bool mouseRightFlag = false;

		public void Update(ref Camera2D cam, GameTime gameTime)
		{
			//Mouse.GetState();
			//Keyboard.GetState();

			if (Keyboard.WasKeyTyped(Keys.Z))
			{
				if (Keyboard.IsKeyPressed(Keys.LeftShift))
				{
					
						cam.Zoom *= 2;
						cam.Position += (new Vector2(MainGame.screenWidth, MainGame.screenHeight) / (float)Math.Exp(cam.Zoom / 2));
					
				}
				else
				{
					if (cam.Zoom > .1f)
					{
						cam.Zoom /= 2;
						cam.Position -= (new Vector2(MainGame.screenWidth, MainGame.screenHeight) / (float)Math.Exp(cam.Zoom * 2));
					}
				}
			}

			if (Mouse.MouseButtonDown(Mouse.MouseButton.Left))
			{
				mouseRightFlag = true;
				mouseRightClickedPosition = cam.Position + Mouse.GetState().Position.ToVector2() / cam.Zoom;// cam.Position; //// - cam.Position;
				//cameraOriginalPosition = cam.Position;

				Debug.WriteLine("Mouse Clicked Position: " + mouseRightClickedPosition);
			}

			if (Mouse.IsButtonPressed(Mouse.MouseButton.Left))
			{
				if (mouseRightFlag)
				{
					cam.Position = (mouseRightClickedPosition - Mouse.GetState().Position.ToVector2() / cam.Zoom);// - cameraOriginalPosition;
				}
			}

			if (Mouse.MouseButtonUp(Mouse.MouseButton.Left))
			{
				Debug.WriteLine("Mouse Released Position: " + Mouse.GetState().Position.ToVector2());
				mouseRightFlag = false;
				//cameraOriginalPosition = cam.Position;
				cam.Position = (mouseRightClickedPosition - Mouse.GetState().Position.ToVector2() / cam.Zoom);
				
			}

			//player.Update(gameTime);
			foreach (Ship ship in ships)
			{
				ship.Update(gameTime);
				//player.Load();
			}
		}
	}
}