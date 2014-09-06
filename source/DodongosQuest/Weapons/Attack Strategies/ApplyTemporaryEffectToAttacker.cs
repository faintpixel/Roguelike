using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Weapons;
using DodongosQuest.Creatures;
using DodongosQuest.Creatures.Temporary_Effects;


namespace DodongosQuest.Weapons.Attack_Strategies
{
    public class ApplyTemporaryEffectToAttacker : IAttackStrategy
    {
        private ITemporaryEffect _effect;

        public ApplyTemporaryEffectToAttacker(ITemporaryEffect effect)
        {
            _effect = effect;
        }

        public void Attack(IWeapon weapon, ICreature attacker, ICreature target, World world)
        {
            ITemporaryEffect e = _effect.Clone();
            e.Id = Guid.NewGuid();
            attacker.AddTemporaryEffect(e);
        }
    }
}
