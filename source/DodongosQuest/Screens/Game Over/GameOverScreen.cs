using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;

namespace DodongosQuest.Screens.Game_Over
{
    public class GameOverScreen : IScreen 
    {
        private Texture2D _backgroundImage;

        public GameOverScreen()
        {
            _backgroundImage = ContentHelper.Content.Load<Texture2D>(@"GameOverBackground");
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GraphicsHelper.spriteBatch.Draw(_backgroundImage, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.5f);
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }
    }
}
