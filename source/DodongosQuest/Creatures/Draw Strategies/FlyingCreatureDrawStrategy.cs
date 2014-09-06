using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;

namespace DodongosQuest.Creatures
{
    public class FlyingCreatureDrawStrategy : IDrawStrategy 
    {
        private Texture2D _image;

        public FlyingCreatureDrawStrategy(Texture2D image)
        {
            _image = image;
        }

        public void Draw(GameTime gameTime, ICreature creature, World world)
        {
            if (world.PlayerCanSeeWorldIndex(creature.WorldIndex))
            {
                Vector2 worldPosition = world.ConvertTileIndexToWorldPosition(creature.WorldIndex.X, creature.WorldIndex.Y);
                Vector2 screenPosition = Camera.GetScreenPosition(worldPosition);

                GraphicsHelper.spriteBatch.Draw(_image, screenPosition, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.3f);
            }
        }
    }
}
