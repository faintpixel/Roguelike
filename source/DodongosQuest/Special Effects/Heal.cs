using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DodongosQuest.ParticleSystem;
using Microsoft.Xna.Framework;

namespace DodongosQuest.Special_Effects
{
    public class Heal :ISpecialEffect
    {
        public event SpecialEffectEvent Done;

        private World _world;
        private HealParticleSystem _particleSystem;

        public Heal(Vector2 startWorldIndex, Vector2 endWorldIndex, World world)
        {
            _world = world;
            _particleSystem = new HealParticleSystem(GameReference.Game, 2);
            _particleSystem.particleSystemFinished += new DodongosQuest.ParticleSystem.ParticleSystem.ParticleSystemFinishedEventHandler(_particleSystem_particleSystemFinished);
            _particleSystem._world = world;           
            GameReference.Game.Components.Add(_particleSystem);
            //direction for partcles in heal spell is random (creates illusion of a circle)
            _particleSystem.AddParticles(startWorldIndex, new Vector2(0,0));            
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
