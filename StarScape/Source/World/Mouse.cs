using System;
using Microsoft.Xna.Framework.Input;

namespace StarScape.Source.World
{
	public class Mouse
	{

		static MouseState previousState;
		static MouseState currentState;

		public enum MouseButton { Left, Middle, Right }

		public static MouseState GetState()
		{
			previousState = currentState;
			currentState = Microsoft.Xna.Framework.Input.Mouse.GetState();
			return currentState;
		}

		public static bool IsButtonPressed(MouseButton button)
		{
			GetState();
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

		public static bool MouseButtonDown(MouseButton button)
		{
			bool currStateFlag;
			bool prevStateFlag;

			switch (button)
			{
				case MouseButton.Left:
				{
					currStateFlag = currentState.LeftButton.HasFlag(ButtonState.Pressed);
					prevStateFlag = previousState.LeftButton.HasFlag(ButtonState.Pressed);
					break;
				}
				case MouseButton.Middle:
				{
					currStateFlag = currentState.MiddleButton.HasFlag(ButtonState.Pressed);
					prevStateFlag = previousState.MiddleButton.HasFlag(ButtonState.Pressed);
					break;
				}
				case MouseButton.Right:
				{
					currStateFlag = currentState.MiddleButton.HasFlag(ButtonState.Pressed);
					prevStateFlag = previousState.MiddleButton.HasFlag(ButtonState.Pressed);
					break;
				}
				default:
				{
					Debug.WriteLine(button + " is not a valid mouse button option.");
					return false;
				}
			}

			//bool currStateFlag = currentState.IsKeyDown(key);
			//bool prevStateFlag = previousState.IsKeyDown(key);

			//GetState();
			//Console.WriteLine(currStateFlag + " : " + !prevStateFlag);

			return currStateFlag && !prevStateFlag; //currentState.IsKeyDown(key) && !previousState.IsKeyDown(key);
		}

		public static bool MouseButtonUp(MouseButton button)
		{
			bool currStateFlag;
			bool prevStateFlag;

			switch (button)
			{
				case MouseButton.Left:
				{
					currStateFlag = currentState.LeftButton.HasFlag(ButtonState.Pressed);
					prevStateFlag = previousState.LeftButton.HasFlag(ButtonState.Pressed);
					break;
				}
				case MouseButton.Middle:
				{
					currStateFlag = currentState.MiddleButton.HasFlag(ButtonState.Pressed);
					prevStateFlag = previousState.MiddleButton.HasFlag(ButtonState.Pressed);
					break;
				}
				case MouseButton.Right:
				{
					currStateFlag = currentState.MiddleButton.HasFlag(ButtonState.Pressed);
					prevStateFlag = previousState.MiddleButton.HasFlag(ButtonState.Pressed);
					break;
				}
				default:
				{
					Debug.WriteLine(button + " is not a valid mouse button option.");
					return false;
				}
			}

			//bool currStateFlag = currentState.IsKeyDown(key);
			//bool prevStateFlag = previousState.IsKeyDown(key);

			//GetState();
			//Console.WriteLine(currStateFlag + " : " + !prevStateFlag);

			return !currStateFlag && prevStateFlag; //currentState.IsKeyDown(key) && !previousState.IsKeyDown(key);
		}

	}
}
