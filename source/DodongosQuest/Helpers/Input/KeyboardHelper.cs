using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace xnaHelper.Input
{
    public class KeyboardHelper
    {
        private KeyboardState _previousState = Keyboard.GetState();
        private List<Keys> _keysOfInterest;

        public delegate void KeyboardEvent(KeyboardHelper sender, Keys key);
        public event KeyboardEvent KeyPressed;
        public event KeyboardEvent KeyReleased;

        public KeyboardHelper(List<Keys> keysOfInterest)
        {
            _keysOfInterest = keysOfInterest;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentState = Keyboard.GetState();

            foreach (Keys key in _keysOfInterest)
            {
                if (_previousState.IsKeyDown(key) != currentState.IsKeyDown(key))
                {
                    if(currentState.IsKeyDown(key))
                    {
                        if (this.KeyPressed != null)
                            KeyPressed(this, key);
                    }
                    else if(_previousState.IsKeyDown(key))
                    {
                        if (this.KeyReleased != null)
                            KeyReleased(this, key);
                    }
                }
            }

            _previousState = currentState;
        }
    }
}
