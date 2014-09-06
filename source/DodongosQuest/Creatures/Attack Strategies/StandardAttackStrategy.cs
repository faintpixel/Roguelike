using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DodongosQuest.Announcements;

namespace DodongosQuest.Creatures.AttackStrategies
{
    public class StandardAttackStrategy : IAttackStrategy 
    {
        public void AttackCreature(ICreature attacker, ref ICreature creature)
        {
            int damage = RandomNumberProvider.GetRandomNumber(1, 2);
            Announcer.Instance.Announce(attacker.Name + " inflicted " + damage + " damage to " + creature.Name, MessageTypes.CreatureAttack);
            creature.Health.Current -= damage;
        }

     
    }
}
