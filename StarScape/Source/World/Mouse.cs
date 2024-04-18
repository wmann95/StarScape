using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace StarScape.Source.World
{

	/// <summary>
	/// This class is a sort of *takeover* of the Microsoft.Xna.Framework.Input.Mouse class, which only has a few useful methods.
	/// </summary>
	public class Mouse
	{

		static MouseState previousState; //the state that was the currentstate prior to GetState() call.
		static MouseState currentState; //the state as of the GetState() call

		public enum MouseButton { Left, Middle, Right }

		/// <summary>
		/// Updates the previousState and currentState
		/// </summary>
		/// <returns></returns>
		public static MouseState GetState()
		{
			previousState = currentState;
			currentState = Microsoft.Xna.Framework.Input.Mouse.GetState();
			return currentState;
		}

		/// <summary>
		/// This is essentially the same as the prebuilt Mouse stuff. Just takes an enum which requests either the left, middle, or right mouse button, and interperets based on that.
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
		public static bool IsButtonPressed(MouseButton button)
		{
			switch (button) //I chose to go with a switch statement instead of an if statement list because, from what I understand, switch statements are usually faster and more efficient.
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
		/// This method checks to see if the requested button is currently pressed and was NOT pressed in the prior state.
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
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
					currStateFlag = currentState.RightButton.HasFlag(ButtonState.Pressed);
					prevStateFlag = previousState.RightButton.HasFlag(ButtonState.Pressed);
					break;
				}
				default:
				{
					Debug.Log(button + " is not a valid mouse button option.");
					return false;
				}
			}

			//bool currStateFlag = currentState.IsKeyDown(key);
			//bool prevStateFlag = previousState.IsKeyDown(key);

			//GetState();
			//Console.WriteLine(currStateFlag + " : " + !prevStateFlag);

			return currStateFlag && !prevStateFlag; //currentState.IsKeyDown(key) && !previousState.IsKeyDown(key);
		}

		/// <summary>
		/// This method checks to see if the requested button is currently not being pressed and the previous state IS being pressed.
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
		public static bool MouseButtonUp(MouseButton button)
		{
			//Flags that are true if the requested button was seen to be pressed in the specific state.
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
					currStateFlag = currentState.RightButton.HasFlag(ButtonState.Pressed);
					prevStateFlag = previousState.RightButton.HasFlag(ButtonState.Pressed);
					break;
				}
				default:
				{
					Debug.Log(button + " is not a valid mouse button option.");
					return false;
				}
			}

			return !currStateFlag && prevStateFlag;
		}

		static bool LeftMouseButtonClickedFlag = false;
		static bool MiddleMouseButtonClickedFlag = false;
		static bool RightMouseButtonClickedFlag = false;

		static bool IsMouseClickFlagged(MouseButton button)
		{
			switch (button) //I chose to go with a switch statement instead of an if statement list because, from what I understand, switch statements are usually faster and more efficient.
			{
				case MouseButton.Left:
				{
					return LeftMouseButtonClickedFlag;
				}
				case MouseButton.Middle:
				{
					return MiddleMouseButtonClickedFlag;
				}
				case MouseButton.Right:
				{
					return RightMouseButtonClickedFlag;
				}
				default:
				{
					return false;
				}
			}
		}

		//how long it should be looking for the mouse up.
		static readonly int MaxClickTime = 200;
		static long LeftClickClock = 0;
		static long MiddleClickClock = 0;
		static long RightClickClock = 0;

		public static bool WasMouseButtonClicked(MouseButton button)
		{
			if (MouseButtonDown(button) && !IsMouseClickFlagged(button))
			{
				switch (button)
				{
					case MouseButton.Left:
					{
						LeftMouseButtonClickedFlag = true;
						LeftClickClock = Time.gameTime;
						break;
					}
					case MouseButton.Middle:
					{
						MiddleMouseButtonClickedFlag = true;
						MiddleClickClock = Time.gameTime;
						break;
					}
					case MouseButton.Right:
					{
						RightMouseButtonClickedFlag = true;
						RightClickClock = Time.gameTime;
						break;
					}
				}
			}

			if (MouseButtonUp(button) && IsMouseClickFlagged(button))
			{
				switch (button)
				{
					case MouseButton.Left:
					{
						LeftMouseButtonClickedFlag = false;

						if (Time.gameTime - LeftClickClock <= MaxClickTime)
						{
							return true;
						}

						break;
					}
					case MouseButton.Middle:
					{
						MiddleMouseButtonClickedFlag = false;

						if (Time.gameTime - MiddleClickClock <= MaxClickTime)
						{
							return true;
						}

						break;
					}
					case MouseButton.Right:
					{
						RightMouseButtonClickedFlag = false;

						if (Time.gameTime - RightClickClock <= MaxClickTime)
						{
							return true;
						}

						break;
					}
				}
			}



			return false;
		}
		
		public static bool IsMouseInRect(Rectangle rect)
		{
			Point mousePos = currentState.Position;
			rect.X -= (int)(World.GameCamera.Position.X);
			rect.Y -= (int)(World.GameCamera.Position.Y);
			rect.X = (int)(rect.X * World.GameCamera.Zoom);
			rect.Y = (int)(rect.Y * World.GameCamera.Zoom);

			rect.Width = (int)(rect.Width * World.GameCamera.Zoom);
			rect.Height = (int)(rect.Height * World.GameCamera.Zoom);

			Debug.Log(mousePos + " : " + rect);

			if (mousePos.X >= rect.X && mousePos.X <= rect.X +  rect.Width && mousePos.Y >= rect.Y && mousePos.Y <= rect.Y + rect.Height) return true;

			return false;
		}

	}
}
