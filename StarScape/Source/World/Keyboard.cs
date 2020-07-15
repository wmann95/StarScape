using System;
using Microsoft.Xna.Framework.Input;

namespace StarScape.Source.World
{
	/// <summary>
	/// This class is a sort of *takeover* of the Microsoft.Xna.Framework.Input.Keyboard class, which only has a few useful methods.
	/// </summary>
	public class Keyboard
	{

		static KeyboardState previousState;
		static KeyboardState currentState;

		public static KeyboardState GetState()
		{
			previousState = currentState;
			currentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
			return currentState;
		}

		/// <summary>
		/// Essentially the default IsKeyDown() method.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool IsKeyPressed(Keys key)
		{
			GetState();
			return currentState.IsKeyDown(key);
		}

		/// <summary>
		/// This method checks to see if the key was down in the previous state and is currently not down
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool WasKeyTyped(Keys key)
		{
			bool currStateFlag = currentState.IsKeyDown(key);
			bool prevStateFlag = previousState.IsKeyDown(key);

			GetState();
			//Console.WriteLine(currStateFlag + " : " + !prevStateFlag);

			return currStateFlag && !prevStateFlag;
		}

	}
}