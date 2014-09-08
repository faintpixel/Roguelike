using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;

namespace DodongosQuest.Achievements
{
    public class AchievementNotifier
    {
        private Texture2D _backgroundImage;
        private Texture2D _achievementImage;
        private Vector2 _position;
        private Vector2 _imagePosition;
        private int _millisecondsToShowFor;
        private bool _showing;
        private TimeSpan _timeOfFirstUpdate;
        private string _name;
        private string _description;
        private SpriteFont _headerFont;
        private SpriteFont _nameFont;
        private SpriteFont _descriptionFont;
        private Vector2 _headerPosition;
        private Vector2 _namePosition;
        private Vector2 _descriptionPosition;

        public delegate void AchievementNotifierEvent(AchievementNotifier sender);
        public event AchievementNotifierEvent DoneDisplaying;

        public AchievementNotifier(Vector2 position, int millisecondsToShowFor, Texture2D achievementImage, string name, string description)
        {
            _position = position;
            _millisecondsToShowFor = millisecondsToShowFor;
            _backgroundImage = ContentHelper.Content.Load<Texture2D>(@"Achievements\AchievementMessageBackground");
            _showing = true;
            _achievementImage = achievementImage;
            _name = name;
            _description = description;
            _headerPosition = new Vector2(position.X + 56, position.Y + 3);
            _imagePosition = new Vector2(position.X + 6, position.Y + 6);
            _namePosition = new Vector2(position.X + 56, position.Y + 18);
            _descriptionPosition = new Vector2(position.X + 56, position.Y + 36);
            _nameFont = ContentHelper.Content.Load<SpriteFont>("arial");
            _descriptionFont = ContentHelper.Content.Load<SpriteFont>("arial");
            _headerFont = ContentHelper.Content.Load<SpriteFont>("arial");
        }

        public void Draw(GameTime gameTime)
        {
            if (_showing)
            {
                GraphicsHelper.spriteBatch.Draw(_backgroundImage, _position, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);
                GraphicsHelper.spriteBatch.Draw(_achievementImage, _imagePosition, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.0f);
                GraphicsHelper.spriteBatch.DrawString(_headerFont, "Achievement Unlocked!", _headerPosition, Color.DarkGreen);
                GraphicsHelper.spriteBatch.DrawString(_nameFont, _name, _namePosition, Color.White);
                GraphicsHelper.spriteBatch.DrawString(_descriptionFont, _description, _descriptionPosition, Color.Silver);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (_showing)
            {
                if(_timeOfFirstUpdate == null || _timeOfFirstUpdate == new TimeSpan())
                    _timeOfFirstUpdate = gameTime.TotalGameTime;
                if (gameTime.TotalGameTime - _timeOfFirstUpdate >= new TimeSpan(0, 0, 0, 0, _millisecondsToShowFor))
                {
                    if (DoneDisplaying != null)
                        DoneDisplaying(this);
                    _showing = false;
                }
            }
        }
    }
}
