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

            // calc the font height
            Vector2 dim = _font.MeasureString("Fark");
            for (int i = 0; i < 10; i++)
                _spellPositions[i] = new Vector2(0, (i * dim.Y) + 25);
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
    }
}
