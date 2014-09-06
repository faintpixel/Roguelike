using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DodongosQuest.ParticleSystem
{
    public class HealParticleSystem :ParticleSystem
    {
        public HealParticleSystem(Game1 game, int howManyEffects)
            : base(game, howManyEffects)
        {
        }

        /// <summary>
        /// Set up the constants that will give this particle system its behavior and
        /// properties.
        /// </summary>
        protected override void InitializeConstants()
        {
            textureFilename = "particle";

            // high initial speed with lots of variance.  make the values closer
            // together to have more consistently circular explosions.
            minInitialSpeed = 2.0f;
            maxInitialSpeed = 2.0f;

            // doesn't matter what these values are set to, acceleration is tweaked in
            // the override of InitializeParticle.
            minAcceleration = 0.5f;
            maxAcceleration = 0.5f;

            // explosions should be relatively short lived
            minLifetime = 1.1f;
            maxLifetime = 1.1f;

            minScale = 0.5f;
            maxScale = 0.5f;

            minNumParticles = 200;
            maxNumParticles = 200;

            minRotationSpeed = -MathHelper.PiOver4;
            maxRotationSpeed = MathHelper.PiOver4;

            // additive blending is very good at creating fiery effects.
            spriteBlendMode = BlendState.Additive;

            DrawOrder = AdditiveDrawOrder;
        }

        protected override void InitializeParticle(Particle p, Vector2 where,Vector2 direction)
        {            
            direction = PickRandomDirection();

            // pick some random values for our particle
            float velocity =
                RandomNumberProvider.RandomBetween(minInitialSpeed, maxInitialSpeed);
            float acceleration =
                RandomNumberProvider.RandomBetween(minAcceleration, maxAcceleration);
            float lifetime =
                RandomNumberProvider.RandomBetween(minLifetime, maxLifetime);
            float scale =
                RandomNumberProvider.RandomBetween(minScale, maxScale);
            float rotationSpeed =
                RandomNumberProvider.RandomBetween(minRotationSpeed, maxRotationSpeed);

            // then initialize it with those random values. initialize will save those,
            // and make sure it is marked as active.
            p.Initialize(
                where, velocity * direction, acceleration * direction,
                lifetime, scale, rotationSpeed);                
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Particle p in this.particles)
            {
                if (p.InitialVelocity.X > 0)
                    p.Velocity.X = p.Velocity.X - 0.05f;
                if (p.InitialVelocity.X < 0)
                    p.Velocity.X = p.Velocity.X + 0.05f;
                if (p.InitialVelocity.Y > 0)
                    p.Velocity.Y = p.Velocity.Y - 0.05f;
                if (p.InitialVelocity.Y < 0)
                    p.Velocity.Y = p.Velocity.Y + 0.05f;
            }

            base.Update(gameTime);
        }
    }
}
