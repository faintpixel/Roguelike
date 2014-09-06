using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DodongosQuest.ParticleSystem
{
    public class Particle
    {       
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Acceleration;
        public Vector2 InitialVelocity;
        public Vector2 InitialPosition;

        private float lifetime;
        public float Lifetime
        {
            get { return lifetime; }
            set { lifetime = value; }
        }
      
        private float timeSinceStart;
        public float TimeSinceStart
        {
            get { return timeSinceStart; }
            set { timeSinceStart = value; }
        }
       
        private float scale;
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
       
        //in radians
        private float rotation;
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
      
        private float rotationSpeed;
        public float RotationSpeed
        {
            get { return rotationSpeed; }
            set { rotationSpeed = value; }
        }
        
        public bool Active
        {
            get { return TimeSinceStart < Lifetime; }
        }

       
        public void Initialize(Vector2 position, Vector2 velocity, Vector2 acceleration,
            float lifetime, float scale, float rotationSpeed)
        {
            this.InitialPosition = position;
            this.Position = position;
            this.Velocity = velocity;
            this.InitialVelocity = velocity;
            this.Acceleration = acceleration;
            this.Lifetime = lifetime;
            this.Scale = scale;
            this.RotationSpeed = rotationSpeed;
          
            this.TimeSinceStart = 0.0f;
            
            this.Rotation = RandomNumberProvider.RandomBetween(0, MathHelper.TwoPi);
        }
       
        public void Update(float dt)
        {
            Velocity += Acceleration * dt;
            Position += Velocity * dt;

            Rotation += RotationSpeed * dt;

            TimeSinceStart += dt;
        }
    }
}
