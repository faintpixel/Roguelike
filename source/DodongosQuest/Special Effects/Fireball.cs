using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;
using DodongosQuest.ParticleSystem;

namespace DodongosQuest.Special_Effects
{
    public class Fireball : ISpecialEffect 
    {
        public event SpecialEffectEvent Done;

        private World _world;
        private FireBallParticleSystem _particleSystem;

        public Fireball(Vector2 startWorldIndex, Vector2 endWorldIndex, World world)
        {
            _world = world;
            _particleSystem = new FireBallParticleSystem(GameReference.Game, 1);
            _particleSystem.particleSystemFinished += new DodongosQuest.ParticleSystem.ParticleSystem.ParticleSystemFinishedEventHandler(_particleSystem_particleSystemFinished);
            _particleSystem._world = world;
            Vector2 direction = endWorldIndex - startWorldIndex;
            GameReference.Game.Components.Add(_particleSystem);
            _particleSystem.AddParticles(startWorldIndex, direction);            
        }

        void _particleSystem_particleSystemFinished(object sender, EventArgs e)
        {
            GameReference.Game.Components.Remove((ParticleSystem.ParticleSystem)sender);
            if (Done != null)
                Done(this);           
        }

        public void Draw(GameTime gameTime)
        {
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
