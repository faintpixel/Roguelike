using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;

namespace DodongosQuest
{
    public class AreaOfEffect
    {
        public Dictionary<Vector2, double> affectedBaseIndices;
        private Texture2D _affectedTileImage;
        private World _world;
        public Direction DirectionFacing;

        public AreaOfEffect(World world)
        {
            affectedBaseIndices = new Dictionary<Vector2, double>();
            _affectedTileImage = ContentHelper.Content.Load<Texture2D>("SelectedTile");
            _world = world;
            DirectionFacing = Direction.West;
        }

        private Vector2 ConvertBaseIndexToWorldIndex(Vector2 baseIndex, Vector2 targetIndex)
        {
            float baseXTouse = 0;
            float baseYToUse = 0;

            if (DirectionFacing == Direction.West)
            {
                baseXTouse = baseIndex.X;
                baseYToUse = baseIndex.Y;
            }
            else if (DirectionFacing == Direction.East)
            {
                baseXTouse = baseIndex.X * -1;
                baseYToUse = baseIndex.Y * -1;
            }
            else if (DirectionFacing == Direction.North)
            {
                baseXTouse = baseIndex.Y * -1;
                baseYToUse = baseIndex.X;
            }
            else if (DirectionFacing == Direction.South)
            {
                baseXTouse = baseIndex.Y;
                baseYToUse = baseIndex.X * -1;
            }
                
            return new Vector2(targetIndex.X + baseXTouse, targetIndex.Y + baseYToUse);  
        }

        public Dictionary<Vector2, double> GetAffectedWorldIndices(Vector2 targetIndex)
        {
            Dictionary<Vector2, double> affectedWorldIndices = new Dictionary<Vector2, double>();

            foreach (KeyValuePair<Vector2, double> x in affectedBaseIndices)
            {
                Vector2 worldIndex = ConvertBaseIndexToWorldIndex(x.Key, targetIndex);
                affectedWorldIndices.Add(worldIndex, x.Value);
            }

            return affectedWorldIndices;
        }

        public void Draw(GameTime gameTime, Vector2 targetIndex)
        {
            Dictionary<Vector2, double> affectedWorldIndices = GetAffectedWorldIndices(targetIndex);

            foreach (KeyValuePair<Vector2, double> x in affectedWorldIndices)
            {
                Vector2 worldPosition = _world.ConvertTileIndexToWorldPosition(x.Key.X, x.Key.Y);
                Vector2 screenPosition = Camera.GetScreenPosition(worldPosition);
                Color color = new Color(new Vector4(1, 1, 1, (float)x.Value));
                GraphicsHelper.spriteBatch.Draw(_affectedTileImage, screenPosition, null, color, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.6f);    
            }            
        }
    }
}
