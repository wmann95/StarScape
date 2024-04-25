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
using StarScape.Source.Types;
using StarScape.Source.World;
using StarScape.Source.World.Ships;

using Keyboard = StarScape.Source.Input.Keyboard;

namespace StarScape.Source.GameState
{
    public class ShipBuildState : GameState
    {
        Ship active_ship;

        Vector2 mouseRightClickedPosition = new Vector2();
        bool mouseRightFlag = false;

        public ShipBuildState()
        {
            active_ship = new ShipBuild();
            MainGame.MouseManager.OnMouseButtonPressed += OnMouseButtonPressed;
            MainGame.MouseManager.OnMouseButtonDown += OnMouseButtonDown;
            MainGame.MouseManager.OnMouseButtonReleased += OnMouseButtonReleased;
        }

        public void OnMouseButtonPressed(object sender, MouseEvent e)
        {
            if (e.mouseButton == MouseButton.Left)
            {
                Tile tile = (Tile)GameObjects.instantiate("floor1");
                Vector2 tilePosition = active_ship.MousePositionToTilePosition(e.position);
                active_ship.AddTile(new Vector2(tilePosition.X, tilePosition.Y), tile);
            }

            if (e.mouseButton == MouseButton.Middle)
			{
				Tile tile = (Tile)GameObjects.instantiate("hull1");
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
            if (e.mouseButton == MouseButton.Right)
            {
                if (mouseRightFlag)
                {
                    MainGame.ActiveCamera.Position = mouseRightClickedPosition - e.position / MainGame.ActiveCamera.Zoom;
                }
            }

        }
        public void OnMouseButtonReleased(object sender, MouseEvent e)
        {
            if (e.mouseButton == MouseButton.Right)
            {
                mouseRightFlag = false;
                MainGame.ActiveCamera.Position = mouseRightClickedPosition - e.position / MainGame.ActiveCamera.Zoom;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            active_ship.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
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
