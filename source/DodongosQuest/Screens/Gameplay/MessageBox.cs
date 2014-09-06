using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using xnaHelper.Helpers;
using DodongosQuest.Announcements;

namespace DodongosQuest.Screens.Gameplay
{
    public class MessageBox
    {
        private List<Message> _messages;
        //private SpriteFont _font;
        private List<Vector2> _messageLocations;
        private Texture2D _backgroundImage;
        private Vector2 _position;
        private int _viewingIndex;
        private int _width;
        private int _height;

        public MessageBox(Vector2 position)
        {
            _messages = new List<Message>();
            //_font = ContentHelper.Content.Load<SpriteFont>("MessageBoxFont");
            _backgroundImage = ContentHelper.Content.Load<Texture2D>("MessageBoxBackground");
            _messageLocations = new List<Vector2>();
            _messageLocations.Add(new Vector2(position.X + 10, position.Y + 120));
            _messageLocations.Add(new Vector2(position.X + 10, position.Y + 105));
            _messageLocations.Add(new Vector2(position.X + 10, position.Y + 90));
            _messageLocations.Add(new Vector2(position.X + 10, position.Y + 75));
            _messageLocations.Add(new Vector2(position.X + 10, position.Y + 60));
            _messageLocations.Add(new Vector2(position.X + 10, position.Y + 45));
            _messageLocations.Add(new Vector2(position.X + 10, position.Y + 30));
            _messageLocations.Add(new Vector2(position.X + 10, position.Y + 15));
            _messageLocations.Add(new Vector2(position.X + 10, position.Y + 0));
            _position = position;
            _viewingIndex = 0;
            _height = _backgroundImage.Height;
            _width = _backgroundImage.Width;
        }

        public void AddMessage(Message message)
        {
            _messages.Insert(0, message);
            _viewingIndex = 0;
        }

        public void Draw(GameTime gameTime)
        {
            GraphicsHelper.spriteBatch.Draw(_backgroundImage, _position, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);

            int messageIndex = 0;
            foreach (Vector2 position in _messageLocations)
            {
                if (_messages.Count > messageIndex + _viewingIndex)
                {
                    Message currentMessage = _messages[messageIndex + _viewingIndex];
                    //GraphicsHelper.spriteBatch.DrawString(_font, currentMessage.MessageText, position, GetMessageTypeColor(currentMessage.MessageType));
                }
                messageIndex += 1;
            }            
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Controls.ScrollToOlderMessages))
            {
                _viewingIndex += 1;
                if (_viewingIndex >= _messages.Count)
                    _viewingIndex = _messages.Count - 1;
            }
            else if (state.IsKeyDown(Controls.ScrollToNewerMessages))
            {
                _viewingIndex -= 1;
                if (_viewingIndex < 0)
                    _viewingIndex = 0;
            }
        }

        private Color GetMessageTypeColor(MessageTypes messageType)
        {
            if (messageType == MessageTypes.CreatureAttack)
                return Color.Red;
            else if (messageType == MessageTypes.PlayerAttack)
                return Color.SteelBlue;
            else if (messageType == MessageTypes.CreatureDeath)
                return Color.Gray;
            else if (messageType == MessageTypes.GetItem)
                return Color.Green;
            else if (messageType == MessageTypes.QuestInformation)
                return Color.Gold;
            else
                return Color.White;
        }

        public bool IntersectsWith(Vector2 position)
        {
            if (position.X >= _position.X && position.X <= _position.X + _width)
                if (position.Y >= _position.Y && position.Y <= _position.Y + _height)
                    return true;

            return false;
        }
    }
}
