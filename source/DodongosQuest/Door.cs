using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;

namespace DodongosQuest
{
    public class Door
    {
        public Vector2 WorldIndex { get; set; }
        private Texture2D _openImage;
        private Texture2D _closedImage;
        public bool IsOpen;
        World _world;

        public Door(Vector2 worldIndex, bool isOpen, World world)
        {
            WorldIndex = worldIndex;
            _openImage = ContentHelper.Content.Load<Texture2D>("OpenDoor");
            _closedImage = ContentHelper.Content.Load<Texture2D>("ClosedDoor");
            IsOpen = isOpen;
            _world = world;
        }

        public void Draw(GameTime gameTime)
        {
            if (_world.PlayerHasExploredWorldIndex(WorldIndex))
            {
                Vector2 worldPosition = _world.ConvertTileIndexToWorldPosition(WorldIndex.X, WorldIndex.Y);
                Vector2 screenPosition = Camera.GetScreenPosition(worldPosition);

                if (IsOpen)
                    GraphicsHelper.spriteBatch.Draw(_openImage, screenPosition, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.7f);
                else
                    GraphicsHelper.spriteBatch.Draw(_closedImage, screenPosition, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.7f);
            }
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Open()
        {
            IsOpen = true;
        }

        public void Close()
        {
            IsOpen = false;
        }
    }
}
