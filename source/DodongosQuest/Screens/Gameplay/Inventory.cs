using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DodongosQuest.Items;
using xnaHelper.Helpers;

namespace DodongosQuest.Screens.Gameplay
{
    public class Inventory  
    {
        private Vector2 _position;
        private List<IItem> _playerInventory;
        private SpriteFont _font;
        private Vector2[] _inventoryPositions;
        private int _width;
        private int _height;

        public Inventory(Vector2 position, List<IItem> playerInventory)
        {
            _position = position;
            _playerInventory = playerInventory;
            //_font = ContentHelper.Content.Load<SpriteFont>("SmallFont");
            _font = ContentHelper.Content.Load<SpriteFont>("arial");
            _inventoryPositions = new Vector2[10];

            _width = 300;
            _height = 0;
            // calc the font height
            Vector2 dim = _font.MeasureString("Fark");

            for (int i = 0; i < 10; i++)
            {
                _inventoryPositions[i] = new Vector2(0, (i * dim.Y));
                if (_height < _inventoryPositions[i].Y)
                {
                    _height = (int)_inventoryPositions[i].Y;
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < 10; i++)
            {
                if (_playerInventory.Count > i)
                {
                    string message = "[" + i + "] " + _playerInventory[i].Name;
                    GraphicsHelper.spriteBatch.DrawString(_font, message, new Vector2(_position.X + _inventoryPositions[i].X, _position.Y + _inventoryPositions[i].Y), Color.White);
                }
            }
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
