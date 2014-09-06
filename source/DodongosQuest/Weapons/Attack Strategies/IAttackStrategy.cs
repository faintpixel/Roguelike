using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DodongosQuest.Creatures;

namespace DodongosQuest.Weapons.Attack_Strategies
{
    public interface IAttackStrategy
    {
        void Attack(IWeapon weapon, ICreature attacker, ICreature target, World world);
    }
}
