using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DodongosQuest.Announcements;

namespace DodongosQuest.Creatures
{
    public class StandardDeathStrategy : IDeathStrategy 
    {
        public void Die(Creature creature)
        {
            Announcer.Instance.Announce(creature.Name + " has died.", MessageTypes.CreatureDeath);
            creature.Die();
        }
    }
}
