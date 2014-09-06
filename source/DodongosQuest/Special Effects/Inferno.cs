using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;

namespace DodongosQuest.Special_Effects
{
    public class Inferno : ISpecialEffect 
    {
        public event SpecialEffectEvent Done;
        private Texture2D _magicCircleImage;
        private World _world;
        private Vector2 _targetIndex;
        private TimeSpan _firstUpdate;
        private TimeSpan _lifespan;
        private Dictionary<Vector2, double> affectedWorldIndices;
        private ParticleSystem.TileFireParticleSystem tileFire;
        public Inferno(Vector2 targetIndex, World world, AreaOfEffect affectedArea)
        {
            _magicCircleImage = ContentHelper.Content.Load<Texture2D>(@"Magic\MagicCircle3x3");
            _firstUpdate = new TimeSpan();
            _lifespan = new TimeSpan(0, 0, 3);
            _world = world;
            _targetIndex = targetIndex;

            affectedWorldIndices = affectedArea.GetAffectedWorldIndices(targetIndex);

            tileFire = new DodongosQuest.ParticleSystem.TileFireParticleSystem(GameReference.Game, 5);
            GameReference.Game.Components.Add(tileFire);
            tileFire._world = world;
            tileFire.particleSystemFinished += new DodongosQuest.ParticleSystem.ParticleSystem.ParticleSystemFinishedEventHandler(tileFire_particleSystemFinished);
            foreach (KeyValuePair<Vector2, double> affectedWorldIndex in affectedWorldIndices)
            {
                tileFire.AddParticles(affectedWorldIndex.Key, new Vector2(0,0));
            }
        }

        void tileFire_particleSystemFinished(object sender, EventArgs e)
        {
            GameReference.Game.Components.Remove((ParticleSystem.ParticleSystem)sender);
            if (Done != null)
                Done(this);
        }

        public void Draw(GameTime gameTime)
        {
            Vector2 worldPosition = _world.ConvertTileIndexToWorldPosition(_targetIndex.X - 1, _targetIndex.Y - 1);
            Vector2 screenPosition = Camera.GetScreenPosition(worldPosition);
            GraphicsHelper.spriteBatch.Draw(_magicCircleImage, screenPosition, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.3f);
        }

        public void Update(GameTime gameTime)
        {    
            if (_firstUpdate == new TimeSpan())
                _firstUpdate = gameTime.TotalGameTime;
            else if (gameTime.TotalGameTime - _firstUpdate >= _lifespan)
            {
                if (Done != null)
                    Done(this);
            }
        }
    }
}
