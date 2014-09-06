using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;

namespace DodongosQuest.Creatures.Remains
{
    public class Remains : IRemains 
    {
        private World _world;
        public Vector2 WorldIndex { get; set; }
        private Texture2D _image;

        public Remains(Vector2 worldIndex, World world, Texture2D image)
        {
            _world = world;
            WorldIndex = worldIndex;
            _image = image;
        }

        public void Draw(GameTime gameTime)
        {
            if (_world.PlayerCanSeeWorldIndex(WorldIndex))
            {
                Vector2 worldPosition = _world.ConvertTileIndexToWorldPosition(WorldIndex.X, WorldIndex.Y);
                Vector2 screenPosition = Camera.GetScreenPosition(worldPosition);

                GraphicsHelper.spriteBatch.Draw(_image, screenPosition, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.7f);
            }
        }
    }
}
