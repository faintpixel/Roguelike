using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DodongosQuest.Creatures;
using xnaHelper.Helpers;

namespace DodongosQuest.Terrain
{
    public delegate void TerrainEvent(Terrain sender, ICreature creature);

    public class Terrain : ITerrain 
    {
        public string Name { get; set; }
        public bool ObstructsLineOfSight { get; set; }
        public bool IsVisibleToPlayer { get; set; }
        public bool IsExploredByPlayer { get; set; }
        public Vector2 WorldIndex { get; set; }

        private List<MovementType> _allowedMovementTypes;
        private Texture2D _tileImage;
        private Texture2D _unexploredTileImage;
        private Texture2D _fogOfWarImage;
        private Texture2D _miniMapImage;
        private Vector2 _worldPosition;
        private Color _miniMapColor;
        

        public Terrain(string name, Texture2D tileImage, bool obstructsLineOfSight, List<MovementType> allowedMovementTypes, Color miniMapColor, Vector2 worldIndex, World world)
        {
            Name = name;
            _unexploredTileImage = ContentHelper.Content.Load<Texture2D>("Nothing");
            _fogOfWarImage = ContentHelper.Content.Load<Texture2D>("FogOfWar");
            _miniMapImage = ContentHelper.Content.Load<Texture2D>(@"Terrain\MiniMapTile");
            _tileImage = tileImage;
            _miniMapColor = miniMapColor;
            ObstructsLineOfSight = obstructsLineOfSight;
            _allowedMovementTypes = allowedMovementTypes;
            WorldIndex = worldIndex;
            _worldPosition = world.ConvertTileIndexToWorldPosition(worldIndex.X, worldIndex.Y);
        }

        public bool CanCreatureEnter(ICreature creature)
        {
            if (_allowedMovementTypes.Contains(creature.MovementType))
                return true;
            else
                return false;
        }

        public void AffectCreature(ICreature creature)
        {
            if (CanCreatureEnter(creature) == false)
                creature.Health.Current -= 2;
        }

        public void Draw(GameTime gameTime)
        {
            Vector2 screenPosition = Camera.GetScreenPosition(_worldPosition);
            if (IsExploredByPlayer)
            {
                GraphicsHelper.spriteBatch.Draw(_tileImage, screenPosition, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.9f);
                if (IsVisibleToPlayer == false)
                    GraphicsHelper.spriteBatch.Draw(_fogOfWarImage, screenPosition, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.8f);
            }
            else
                GraphicsHelper.spriteBatch.Draw(_unexploredTileImage, screenPosition, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.9f);
        }

        public void DrawMiniMap(GameTime gameTime, Vector2 position)
        {
            if(IsExploredByPlayer)
                GraphicsHelper.spriteBatch.Draw(_miniMapImage, position, null, _miniMapColor, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
