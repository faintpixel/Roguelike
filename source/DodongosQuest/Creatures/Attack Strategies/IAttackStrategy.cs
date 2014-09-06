using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DodongosQuest.Creatures.AttackStrategies
{

    public interface IAttackStrategy
    {
        void AttackCreature(ICreature attacker, ref ICreature creature);
    }
}
