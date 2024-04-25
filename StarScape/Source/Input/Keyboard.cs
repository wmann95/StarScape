using System;
using Microsoft.Xna.Framework.Input;

namespace StarScape.Source.Input
{
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

        public static bool IsKeyPressed(Keys key)
        {
            GetState();
            return currentState.IsKeyDown(key);
        }

        public static bool WasKeyTyped(Keys key)
        {
            //GetState();
            bool currStateFlag = currentState.IsKeyDown(key);
            bool prevStateFlag = previousState.IsKeyDown(key);

            return currStateFlag && !prevStateFlag;
        }

    }
}