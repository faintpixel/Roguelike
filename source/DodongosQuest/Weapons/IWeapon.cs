using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DodongosQuest.Creatures;

namespace DodongosQuest.Weapons
{
    public interface IWeapon
    {
        string Name { get; set; }
        int Range { get; set; }
        void Attack(ICreature attacker, ICreature target);
    }
}
