using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DodongosQuest.ParticleSystem
{
    public abstract class ParticleSystem : DrawableGameComponent
    {
        public delegate void ParticleSystemFinishedEventHandler(object sender, EventArgs e);
        public event ParticleSystemFinishedEventHandler particleSystemFinished;

        //Controls the draw order of particles. 
        //Additive blending particles draw on top of particles using alpha blending.
        public const int AlphaBlendDrawOrder = 100;
        public const int AdditiveDrawOrder = 200;
        public World _world;

        //particle texture
        private Texture2D texture;

        //origin when drawing texture (middle of texture)
        private Vector2 origin;
       
        // this number represents the maximum number of effects this particle system
        // will be expected to draw at one time. this is set in the constructor and is
        // used to calculate how many particles we will need.
        private int howManyEffects;

        private Game1 game;

        //reused so no new allocation needed.
        public Particle[] particles;

        //Keeps tract of unused particles.  When a new effect is requested particles are taken from the queue
        //and when finished they are put back in the queue.
        Queue<Particle> freeParticles;

        public bool IsActive
        {
            get
            {
                foreach (Particle p in this.particles)
                {
                    if (p.Active)
                        return true;
                }
                return false;
            }
        }

        public int FreeParticleCount
        {
            get
            {
                return freeParticles.Count<Particle>();
            }
        }

        #region Sub Class Constants
        /// <summary>
        /// minNumParticles and maxNumParticles control the number of particles that are
        /// added when AddParticles is called. The number of particles will be a random
        /// number between minNumParticles and maxNumParticles.
        /// </summary>
        protected int minNumParticles;
        protected int maxNumParticles;

        /// <summary>
        /// this controls the texture that the particle system uses. It will be used as
        /// an argument to ContentManager.Load.
        /// </summary>
        protected string textureFilename;

        /// <summary>
        /// minInitialSpeed and maxInitialSpeed are used to control the initial velocity
        /// of the particles. The particle's initial speed will be a random number 
        /// between these two. The direction is determined by the function 
        /// PickRandomDirection, which can be overriden.
        /// </summary>
        protected float minInitialSpeed;
        protected float maxInitialSpeed;

        /// <summary>
        /// minAcceleration and maxAcceleration are used to control the acceleration of
        /// the particles. The particle's acceleration will be a random number between
        /// these two. By default, the direction of acceleration is the same as the
        /// direction of the initial velocity.
        /// </summary>
        protected float minAcceleration;
        protected float maxAcceleration;

        /// <summary>
        /// minRotationSpeed and maxRotationSpeed control the particles' angular
        /// velocity: the speed at which particles will rotate. Each particle's rotation
        /// speed will be a random number between minRotationSpeed and maxRotationSpeed.
        /// Use smaller numbers to make particle systems look calm and wispy, and large 
        /// numbers for more violent effects.
        /// </summary>
        protected float minRotationSpeed;
        protected float maxRotationSpeed;

        /// <summary>
        /// minLifetime and maxLifetime are used to control the lifetime. Each
        /// particle's lifetime will be a random number between these two. Lifetime
        /// is used to determine how long a particle "lasts." Also, in the base
        /// implementation of Draw, lifetime is also used to calculate alpha and scale
        /// values to avoid particles suddenly "popping" into view
        /// </summary>
        protected float minLifetime;
        protected float maxLifetime;

        /// <summary>
        /// to get some additional variance in the appearance of the particles, we give
        /// them all random scales. the scale is a value between minScale and maxScale,
        /// and is additionally affected by the particle's lifetime to avoid particles
        /// "popping" into view.
        /// </summary>
        protected float minScale;
        protected float maxScale;

        /// <summary>
        /// different effects can use different blend modes. fire and explosions work
        /// well with additive blending, for example.
        /// </summary>
        protected BlendState spriteBlendMode;

        #endregion

        protected ParticleSystem(Game1 game, int howManyEffects)
            : base(game)
        {
            this.game = game;
            this.howManyEffects = howManyEffects;
            this.Initialize();
        }

        public override void Initialize()
        {
            InitializeConstants();

            // calculate the total number of particles we will ever need, using the
            // max number of effects and the max number of particles per effect.         
            particles = new Particle[howManyEffects * maxNumParticles];
            freeParticles = new Queue<Particle>(howManyEffects * maxNumParticles);
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i] = new Particle();
                freeParticles.Enqueue(particles[i]);
            }
            base.Initialize();
        }

        protected abstract void InitializeConstants();

        protected override void LoadContent()
        {
            // make sure sub classes properly set textureFilename.
            if (string.IsNullOrEmpty(textureFilename))
            {
                string message = "textureFilename wasn't set properly";
                throw new InvalidOperationException(message);
            }
            // load the texture....
            texture = game.Content.Load<Texture2D>(textureFilename);

            // ... and calculate the center. this'll be used in the draw call, we
            // always want to rotate and scale around this point.
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;

            base.LoadContent();
        }

        public void AddParticles(Vector2 where,Vector2 direction)
        {
            // the number of particles we want for this effect is a random number
            // somewhere between the two constants specified by the subclasses.
            int numParticles =
                RandomNumberProvider.GetRandomNumber(minNumParticles, maxNumParticles);

            // create that many particles, if you can.
            for (int i = 0; i < numParticles && freeParticles.Count > 0; i++)
            {
                // grab a particle from the freeParticles queue, and Initialize it.
                Particle p = freeParticles.Dequeue();
                InitializeParticle(p, where,direction);
            }
        }

        /// <summary>
        /// Initializes a particle
        /// </summary>
        /// <param name="p"></param>
        /// <param name="where"></param>
        /// <param name="direction">set to null for a random direction for each particle</param>
        protected virtual void InitializeParticle(Particle p, Vector2 where,Vector2 direction)
        {
            if (direction == null)
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

        protected virtual Vector2 PickRandomDirection()
        {
            float angle = RandomNumberProvider.RandomBetween(0, MathHelper.TwoPi);
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        public override void Update(GameTime gameTime)
        {
            // calculate dt, the change in the since the last frame. the particle
            // updates will use this value.
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
      
            foreach (Particle p in particles)
            {

                if (p.Active)
                {                  
                    p.Update(dt);                
                    if (!p.Active)
                    {
                        freeParticles.Enqueue(p);
                    }
                }
            }
            if (!IsActive)
            {
                if (particleSystemFinished != null)
                    particleSystemFinished(this, null);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            xnaHelper.Helpers.GraphicsHelper.spriteBatch.Begin();
            //xnaHelper.Helpers.GraphicsHelper.spriteBatch.Begin(spriteBlendMode);

            foreach (Particle p in particles)
            {               
                if (!p.Active)
                    continue;

                // normalized lifetime is a value from 0 to 1 and represents how far
                // a particle is through its life. 0 means it just started, .5 is half
                // way through, and 1.0 means it's just about to be finished.
                // this value will be used to calculate alpha and scale, to avoid 
                // having particles suddenly appear or disappear.
                float normalizedLifetime = p.TimeSinceStart / p.Lifetime;

                // want particles to fade in and fade out, so calculate alpha
                // to be (normalizedLifetime) * (1-normalizedLifetime). When
                // normalizedLifetime is 0 or 1, alpha is 0. the maximum value is at
                // normalizedLifetime = .5, and is
                // (normalizedLifetime) * (1-normalizedLifetime)
                // (.5)                 * (1-.5)
                // .25
                // maximum alpha to be 1, not .25, scale the 
                // entire equation by 4.
                float alpha = 4 * normalizedLifetime * (1 - normalizedLifetime);
                Color color = new Color(new Vector4(1, 1, 1, alpha));

                // make particles grow as they age. they'll start at 75% of their size,
                // and increase to 100% once they're finished.
                float scale = p.Scale * (.75f + .25f * normalizedLifetime);

                Vector2 worldPosition = _world.ConvertTileIndexToWorldPosition(p.Position.X, p.Position.Y);
                Vector2 tileMiddle = new Vector2(worldPosition.X + World.TILE_SIZE / 2, worldPosition.Y + World.TILE_SIZE / 2);
                Vector2 screenPosition = Camera.GetScreenPosition(tileMiddle);

                xnaHelper.Helpers.GraphicsHelper.spriteBatch.Draw(texture, screenPosition, null, color,
                    p.Rotation, origin, scale, SpriteEffects.None, 0.0f);
            }

            xnaHelper.Helpers.GraphicsHelper.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
