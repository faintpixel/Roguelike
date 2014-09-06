using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DodongosQuest.Creatures.AttackStrategies;
using Microsoft.Xna.Framework;
using xnaHelper.Helpers;
using Microsoft.Xna.Framework.Graphics;
using DodongosQuest.ParticleSystem;

namespace DodongosQuest.Creatures
{
    public class Fire
    {
        public delegate void FlamingAttackEvent(Fire sender);

        private World _world;
        private Texture2D _image;
        public IDrawStrategy DrawStrategy { get; set; }
        public IAttackStrategy AttackStrategy { get; set; }
        public Vector2 WorldIndex { get; set; }       
        //private FireBallParticleSystem fireBallParticles;       

        public event FlamingAttackEvent Flame;

        public Fire(Vector2 worldIndex, World world)
        {
            _world = world;
            WorldIndex = worldIndex;
            _image = ContentHelper.Content.Load<Texture2D>(@"Fire");
            //fireBallParticles = new FireBallParticleSystem(GameReference.Game, 1);
            //fireBallParticles._world = world;
            //GameReference.Game.Components.Add(fireBallParticles);
            //fireBallParticles.AddParticles(WorldIndex, new Vector2(0, -0.5f));
        }

        public void Draw(GameTime gameTime)
        {
            //handled by particle system   
        }

        public void Update(GameTime gameTime)
        {
            //if (fireBallParticles.IsActive)
            //{
                if (Flame != null)
                    Flame(this);
            ////}
        }
    }
}
