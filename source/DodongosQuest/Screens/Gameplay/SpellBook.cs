using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DodongosQuest.Items;
using xnaHelper.Helpers;
using DodongosQuest.Magic;

namespace DodongosQuest.Screens.Gameplay
{
    public class SpellBook
    {
        private Vector2 _position;
        private List<ISpell> _playerSpells;
        private SpriteFont _font;
        private Vector2[] _spellPositions;
        private List<string> _keys;
        private int _width;
        private int _height;

        public SpellBook(Vector2 position, List<ISpell> playerSpells)
        {
            _position = position;
            _playerSpells = playerSpells;
            //_font = ContentHelper.Content.Load<SpriteFont>("SmallFont");
            _font = ContentHelper.Content.Load<SpriteFont>("arial");
            _spellPositions = new Vector2[10];
            _keys = new List<string>();
            _keys.Add("Q");
            _keys.Add("W");
            _keys.Add("E");
            _keys.Add("R");
            _keys.Add("T");
            _keys.Add("Y");

            _width = 300;
            _height = 0;
            // calc the font height
            Vector2 dim = _font.MeasureString("Fark");
            for (int i = 0; i < 10; i++)
            {
                _spellPositions[i] = new Vector2(0, (i * dim.Y) + 25);
                if (_height < _spellPositions[i].Y)
                {
                    _height = (int)_spellPositions[i].Y;
                }
            }

        }

        public string GetSpellAt(Vector2 pos)
        {
            // BROKEN
            Vector2 dim = _font.MeasureString("Fark");
            for (int i = 0; i < _playerSpells.Count; i++)
            {
                if (pos.Y >= _spellPositions[i].Y && pos.Y <= _spellPositions[i].Y + dim.Y);
                {
                    return _keys[i];
                }
            }
            return null;
        }

        public void Draw(GameTime gameTime)
        {

            for (int i = 0; i < 10; i++)
            {
                if (_playerSpells.Count > i)
                {
                    string message = "[" + _keys[i] + "] " + _playerSpells[i].Name;
                    GraphicsHelper.spriteBatch.DrawString(_font, message, new Vector2(_position.X + _spellPositions[i].X, _position.Y + _spellPositions[i].Y), Color.White);
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
