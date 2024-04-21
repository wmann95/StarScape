using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using StarScape.Source.EventSystem.Events;
using StarScape.Source.World;

namespace StarScape.Source.Input
{
	public enum MouseButton
	{
		Left,
		Middle,
		Right
	}

	public class Mouse
	{

		public event EventHandler<MouseEvent> OnMouseButtonPressed;
		public event EventHandler<MouseEvent> OnMouseButtonDown;
		public event EventHandler<MouseEvent> OnMouseButtonReleased;

		MouseState previousState;
		MouseState currentState;

		/// <summary>
		/// Checks if states have changed and sends out mouse events.
		/// </summary>
		/// <returns></returns>
		public void Update()
		{
			previousState = currentState;
			currentState = Microsoft.Xna.Framework.Input.Mouse.GetState();

			Vector2 mousePosition = currentState.Position.ToVector2();

			foreach(MouseButton button in Enum.GetValues<MouseButton>())
			{
				if (IsButtonPressed(button) && !WasButtonPressed(button))
				{
					EventHandler<MouseEvent> pressed = OnMouseButtonPressed;
					if (pressed != null){
						pressed(this, new MouseEvent(MouseEvent.Type.Pressed, button, mousePosition));
					}
				}
				else if (IsButtonPressed(button) && WasButtonPressed(button))
				{
					EventHandler<MouseEvent> down = OnMouseButtonDown;
					if (down != null)
					{
						down(this, new MouseEvent(MouseEvent.Type.Down, button, mousePosition));
					}
				}
				else if (!IsButtonPressed(button) && WasButtonPressed(button))
				{
					EventHandler<MouseEvent> released = OnMouseButtonReleased;
					if (released != null)
					{
						released(this, new MouseEvent(MouseEvent.Type.Released, button, mousePosition));
					}
				}
			}

		}

		/// <summary>
		/// Checks if the given button is currently pressed.
		/// </summary>
		/// <param name="button"></param>
		/// <returns>bool</returns>
		public bool IsButtonPressed(MouseButton button)
		{
			switch (button)
			{
				case MouseButton.Left:
				{
					return currentState.LeftButton.HasFlag(ButtonState.Pressed);
				}
				case MouseButton.Middle:
				{
					return currentState.MiddleButton.HasFlag(ButtonState.Pressed);
				}
				case MouseButton.Right:
				{
					return currentState.RightButton.HasFlag(ButtonState.Pressed);
				}
				default:
				{
					return false;
				}
			}

		}

		/// <summary>
		/// Checks if the given button was pressed in the previous state.
		/// </summary>
		/// <param name="button"></param>
		/// <returns>bool</returns>
		public bool WasButtonPressed(MouseButton button)
		{
			switch (button)
			{
				case MouseButton.Left:
				{
					return previousState.LeftButton.HasFlag(ButtonState.Pressed);
				}
				case MouseButton.Middle:
				{
					return previousState.MiddleButton.HasFlag(ButtonState.Pressed);
				}
				case MouseButton.Right:
				{
					return previousState.RightButton.HasFlag(ButtonState.Pressed);
				}
				default:
				{
					return false;
				}
			}

		}

		public bool IsMouseInRect(Rectangle rect)
		{
			Point mousePos = currentState.Position;
			rect.X -= (int)(MainGame.ActiveCamera.Position.X);
			rect.Y -= (int)(MainGame.ActiveCamera.Position.Y);
			rect.X = (int)(rect.X * MainGame.ActiveCamera.Zoom);
			rect.Y = (int)(rect.Y * MainGame.ActiveCamera.Zoom);

			rect.Width = (int)(rect.Width * MainGame.ActiveCamera.Zoom);
			rect.Height = (int)(rect.Height * MainGame.ActiveCamera.Zoom);

			Debug.Log(mousePos + " : " + rect);

			if (mousePos.X >= rect.X && mousePos.X <= rect.X + rect.Width && mousePos.Y >= rect.Y && mousePos.Y <= rect.Y + rect.Height) return true;

			return false;
		}
	}
}
