using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Items;
using DodongosQuest.Creatures;
using DodongosQuest.Announcements;
using DodongosQuest.Special_Effects;

namespace DodongosQuest.Weapons.Attack_Strategies
{
    public class ProjectileAttackStrategy : IAttackStrategy
    {
        private int _minimumDamage;
        private int _maximumDamage;
        private int _chanceOfHit;

        public ProjectileAttackStrategy(int minimumDamage, int maximumDamage, int chanceOfHit)
        {
            _minimumDamage = minimumDamage;
            _maximumDamage = maximumDamage;
            _chanceOfHit = chanceOfHit;
        }

        public void Attack(IWeapon weapon, ICreature attacker, ICreature target, World world)
        {
            MessageTypes messageTypeToUse;
            bool attackHits = RandomNumberProvider.CheckIfChanceOccurs(_chanceOfHit);
            int amountOfDamage = RandomNumberProvider.GetRandomNumber(_minimumDamage, _maximumDamage);

            if (attacker.IsPlayer())
                messageTypeToUse = MessageTypes.PlayerAttack;
            else
                messageTypeToUse = MessageTypes.CreatureAttack;

            if (attackHits)
            {
                target.Health.Current -= amountOfDamage;
                Announcer.Instance.Announce(attacker.Name + " hits " + target.Name + " for " + amountOfDamage + " damage!", MessageTypes.CreatureAttack);
            }
            else
            {
                Announcer.Instance.Announce(attacker.Name + " misses " + target.Name + "!", MessageTypes.CreatureAttack);
            }

            world.AddSpecialEffectToWorld(new Fireball(attacker.WorldIndex, target.WorldIndex, world));
        }
    }
}
