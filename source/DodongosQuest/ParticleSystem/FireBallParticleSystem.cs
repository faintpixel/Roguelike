using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DodongosQuest.ParticleSystem
{
    public class FireBallParticleSystem : ParticleSystem
    {
        public delegate void FireBallFinishedEventHandler(object sender,EventArgs e);
        public event FireBallFinishedEventHandler fireBallFinished;

        public FireBallParticleSystem(Game1 game, int howManyEffects)
            : base(game, howManyEffects)
        {
        }

        /// <summary>
        /// Set up the constants that will give this particle system its behavior and
        /// properties.
        /// </summary>
        protected override void InitializeConstants()
        {
            textureFilename = "explosion";

            // high initial speed with lots of variance.  make the values closer
            // together to have more consistently circular explosions.
            minInitialSpeed = 5.0f;
            maxInitialSpeed = 5.0f;

            // doesn't matter what these values are set to, acceleration is tweaked in
            // the override of InitializeParticle.
            minAcceleration = 0.0f;
            maxAcceleration = 0.0f;

            // explosions should be relatively short lived
            minLifetime = 0.2f;
            maxLifetime = 0.2f;

            minScale = .1f;
            maxScale = .1f;

            minNumParticles = 15;
            maxNumParticles = 15;

            minRotationSpeed = -MathHelper.PiOver4;
            maxRotationSpeed = MathHelper.PiOver4;

            // additive blending is very good at creating fiery effects.
            spriteBlendMode = BlendState.Additive;

            DrawOrder = AdditiveDrawOrder;
        }

        protected override void InitializeParticle(Particle p, Vector2 where,Vector2 direction)
        {
            base.InitializeParticle(p, where, direction);
            p.Acceleration = new Vector2(0, 0);           
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!this.IsActive)
            {
                if (fireBallFinished != null)
                    fireBallFinished(this, null);
            }
        }
    }
}
