using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;
using DodongosQuest.Creatures.Player;

namespace DodongosQuest.Screens.Gameplay
{
    public class SideBar
    {
        private Texture2D _backgroundImage;
        private Vector2 _position;
        private PlayerCharacter _player;
        //private SpriteFont _font;
        private Vector2 _playerHitPointsPosition;
        private Vector2 _playerManaPosition;
        private int _width;
        private int _height;

        public SideBar(Vector2 position, ref PlayerCharacter player)
        {
            _position = position;
            _backgroundImage = ContentHelper.Content.Load<Texture2D>("SideBarBackground");
            _player = player;
            //_font = ContentHelper.Content.Load<SpriteFont>("MessageBoxFont");
            _playerHitPointsPosition = new Vector2(position.X + 3, position.Y + 3);
            _playerManaPosition = new Vector2(position.X + 3, position.Y + 23);
            _width = _backgroundImage.Width;
            _height = _backgroundImage.Height;
        }

        public void Draw(GameTime gameTime)
        {
            GraphicsHelper.spriteBatch.Draw(_backgroundImage, _position, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.3f);
            //GraphicsHelper.spriteBatch.DrawString(_font, "HP: " + _player.Health.Current + "/" + _player.Health.Maximum, _playerHitPointsPosition, Color.White);
            //GraphicsHelper.spriteBatch.DrawString(_font, "M: " + _player.Mana.Current + "/" + _player.Mana.Maximum, _playerManaPosition, Color.White);
        }

        public void Update(GameTime gameTime)
        {
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
