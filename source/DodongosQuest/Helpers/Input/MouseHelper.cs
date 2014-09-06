using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace xnaHelper.Input
{
    public enum MouseButtons
    {
        Left = 0,
        Right = 1,
        Middle = 2
    }

    public class MouseHelper
    {
        public delegate void MouseMovementEvent(MouseHelper sender, MouseState state);
        public delegate void MouseButtonEventEvent(MouseHelper sender, MouseButtons button, MouseState state);

        public event MouseMovementEvent MouseMoved;
        public event MouseButtonEventEvent MouseButtonPressed;
        public event MouseButtonEventEvent MouseButtonReleased;

        private int previousXPosition = 0;
        private int previousYPosition = 0;

        public ButtonState previousLeftButtonState = ButtonState.Released;
        public ButtonState previousRightButtonState = ButtonState.Released;

        public MouseHelper()
        {
        }

        public void Update()
        {
            MouseState state = Mouse.GetState();
            if (state.X != previousXPosition || state.Y != previousYPosition)
                if (MouseMoved != null)
                    MouseMoved(this, state);

            if (state.LeftButton != previousLeftButtonState)
            {
                if (state.LeftButton == ButtonState.Pressed)
                {
                    if (MouseButtonPressed != null)
                        MouseButtonPressed(this, MouseButtons.Left, state);
                }
                else if (state.LeftButton == ButtonState.Released)
                    if (MouseButtonReleased != null)
                        MouseButtonReleased(this, MouseButtons.Left, state);
                
            }
            if (state.RightButton != previousRightButtonState)
            {
                if (state.RightButton == ButtonState.Pressed)
                {
                    if (MouseButtonPressed != null)
                        MouseButtonPressed(this, MouseButtons.Right, state);
                }
                else if (state.RightButton == ButtonState.Released)
                    if (MouseButtonReleased != null)
                        MouseButtonReleased(this, MouseButtons.Right, state);
            }

            previousXPosition = state.X;
            previousYPosition = state.Y;
            previousLeftButtonState = state.LeftButton;
            previousRightButtonState = state.RightButton;
        }
    }
}
