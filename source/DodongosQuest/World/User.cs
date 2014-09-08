using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DodongosQuest
{
    public class User
    {
        public delegate void UserInput(User sender, List<Keys> userInput);
        public event UserInput UserInputReceived;

        private KeyboardState _previousKeyboardState = new KeyboardState();
        private List<Keys> _keysOfInterest;
        private TimeSpan _keyRepeatTime;
        private Dictionary<Keys, TimeSpan> _heldKeys;

        public User()
        {
            _keysOfInterest = new List<Keys>();
            _keysOfInterest.Add(Controls.Wait);
            _keysOfInterest.Add(Controls.MoveLeft);
            _keysOfInterest.Add(Controls.MoveRight);
            _keysOfInterest.Add(Controls.MoveUp);
            _keysOfInterest.Add(Controls.MoveDown);
            _keysOfInterest.Add(Controls.CloseDoor);
            _keysOfInterest.Add(Controls.CenterViewOnPlayer);
            _keysOfInterest.Add(Controls.UseHealthPotion);
            _keysOfInterest.Add(Controls.TEST_DoSomething);
            _keysOfInterest.Add(Controls.UseInventoryItem0);
            _keysOfInterest.Add(Controls.UseInventoryItem1);
            _keysOfInterest.Add(Controls.UseInventoryItem2);
            _keysOfInterest.Add(Controls.UseInventoryItem3);
            _keysOfInterest.Add(Controls.UseInventoryItem4);
            _keysOfInterest.Add(Controls.UseInventoryItem5);
            _keysOfInterest.Add(Controls.UseInventoryItem6);
            _keysOfInterest.Add(Controls.UseInventoryItem7);
            _keysOfInterest.Add(Controls.UseInventoryItem8);
            _keysOfInterest.Add(Controls.UseInventoryItem9);
            _keysOfInterest.Add(Controls.UseSpell0);
            _keysOfInterest.Add(Controls.UseSpell1);
            _keysOfInterest.Add(Controls.UseSpell2);
            _keysOfInterest.Add(Controls.UseSpell3);
            _keysOfInterest.Add(Controls.UseSpell4);
            _keysOfInterest.Add(Controls.UseSpell5);
            _keysOfInterest.Add(Controls.Fire);
            _keysOfInterest.Add(Controls.CastSpellOrAttackTarget);
            _keysOfInterest.Add(Controls.SelectNextTarget);
            _keysOfInterest.Add(Controls.SelectPreviousTarget);
            _keysOfInterest.Add(Controls.RotateSpell);

            _keyRepeatTime = new TimeSpan(0, 0, 0, 0, 175);
            _heldKeys = new Dictionary<Keys, TimeSpan>();
        }

        public void Update(GameTime gameTime)
        {
            List<Keys> userInput = GetUserInput(gameTime);
            if (userInput.Count != 0)
                if (UserInputReceived != null)
                    UserInputReceived(this, userInput);
        }

        private List<Keys> GetUserInput(GameTime gameTime)
        {
            List<Keys> unhandledReleasedKeys = new List<Keys>();       
            KeyboardState currentKeyboardState = Keyboard.GetState();

            foreach (Keys key in _keysOfInterest)
            {
                if (currentKeyboardState.IsKeyUp(key) && _heldKeys.ContainsKey(key))
                    _heldKeys.Remove(key);
                if (currentKeyboardState.IsKeyDown(key))
                {
                    if (_heldKeys.ContainsKey(key) == false)
                    {
                        unhandledReleasedKeys.Add(key);
                        _heldKeys.Add(key, gameTime.TotalGameTime);
                    }
                    else
                    {
                        TimeSpan timeOfLastTrigger = _heldKeys[key];
                        if (timeOfLastTrigger + _keyRepeatTime <= gameTime.TotalGameTime)
                        {
                            unhandledReleasedKeys.Add(key);
                            _heldKeys[key] = gameTime.TotalGameTime;
                        }
                    }
                }
            }

            _previousKeyboardState = currentKeyboardState;

            return unhandledReleasedKeys;
        }
    }
}
