using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace KLib
{
    public static class Input
    {
        static KeyboardState keyState;
        public static KeyboardState KeyState
        {
            get { return Input.keyState; }
        }
        static KeyboardState prevKeyState;
        public static KeyboardState PrevKeyState
        {
            get { return Input.prevKeyState; }
        }

        static MouseState mouseState;
        public static MouseState MouseState
        {
            get { return Input.mouseState; }
        }
        static MouseState prevMouseState;
        public static MouseState PrevMouseState
        {
            get { return Input.prevMouseState; }
        }

        private static int mouseX;
        public static int MouseX
        {
            get { return Input.mouseX; }
        }

        private static int mouseY;
        public static int MouseY
        {
            get { return Input.mouseY; }
        }

        static int scroll;
        public static int Scroll
        {
            get { return Input.scroll; }
        }
        static int prevScroll;
        public static int PrevScroll
        {
            get { return Input.prevScroll; }
        }

        public static bool IsKeyDown(Keys key)
        {
            return keyState.IsKeyDown(key);
        }

        public static bool IsKeyUp(Keys key)
        {
            return keyState.IsKeyUp(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key);
        }

        public static bool LeftMouseClicked()
        {
            return mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released;
        }

        public static bool RightMouseClicked()
        {
            return mouseState.RightButton == ButtonState.Pressed && prevMouseState.RightButton == ButtonState.Released;
        }

        public static bool MiddleMouseClicked()
        {
            return mouseState.MiddleButton == ButtonState.Pressed && prevMouseState.MiddleButton == ButtonState.Released;
        }

        public static bool ScrollWheelUp()
        {
            return scroll > prevScroll;
        }

        public static bool ScrollWheelDown()
        {
            return scroll < prevScroll;
        }

        public static void PreUpdate(GameTime dt)
        {
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            scroll = mouseState.ScrollWheelValue;

            mouseX = mouseState.X;
            mouseY = mouseState.Y;
        }

        public static void PostUpdate(GameTime dt)
        {
            prevKeyState = keyState;
            prevMouseState = mouseState;
            prevScroll = prevMouseState.ScrollWheelValue;
        }
    }
}
