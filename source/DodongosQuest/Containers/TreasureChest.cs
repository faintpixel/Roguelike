using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;
using DodongosQuest.Items;

namespace DodongosQuest.Containers
{
    public class TreasureChest : IContainer
    {
        public List<IItem> Contents { get; set; }
        private World _world;
        public Vector2 WorldIndex { get; set; }
        public bool IsLocked { get; set; }
        private Texture2D _emptyImage;
        private Texture2D _fullImage;

        public TreasureChest(List<IItem> contents, bool isLocked, Vector2 worldIndex, World world)
        {
            Contents = contents;
            _world = world;
            WorldIndex = worldIndex;
            _emptyImage = ContentHelper.Content.Load<Texture2D>("OpenChest");
            _fullImage = ContentHelper.Content.Load<Texture2D>("ClosedChest");
            IsLocked = isLocked;
        }

        public void Draw(GameTime gameTime)
        {
            if (_world.PlayerHasExploredWorldIndex(WorldIndex))
            {
                Vector2 worldPosition = _world.ConvertTileIndexToWorldPosition(WorldIndex.X, WorldIndex.Y);
                Vector2 screenPosition = Camera.GetScreenPosition(worldPosition);
                Texture2D currentImage;
                if (IsEmpty())
                    currentImage = _emptyImage;
                else
                    currentImage = _fullImage;

                GraphicsHelper.spriteBatch.Draw(currentImage, screenPosition, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.7f);
            }
        }

        public void Update(GameTime gameTime)
        {
        }

        public bool IsEmpty()
        {
            if (Contents.Count > 0)
                return false;
            else
                return true;
        }
    }
}
