using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DodongosQuest.Announcements;
using DodongosQuest.ParticleSystem;
using Microsoft.Xna.Framework;
using xnaHelper.Helpers;
using Microsoft.Xna.Framework.Graphics;

namespace DodongosQuest.Creatures.AttackStrategies
{
    class ProjectileAttackStrategy : IAttackStrategy 
    {
        public void AttackCreature(ICreature attacker, ref ICreature creature)
        {
            AttackWithFire(attacker, ref creature);
        }

        private void AttackWithFire(ICreature attacker, ref ICreature creature)
        {            
            int damage = RandomNumberProvider.GetRandomNumber(4, 10);
            Announcer.Instance.Announce(attacker.Name + " burned " + creature.Name + " with fire for " + damage + "damage", MessageTypes.CreatureAttack);
            creature.Health.Current -= damage;


        }

    }
}
