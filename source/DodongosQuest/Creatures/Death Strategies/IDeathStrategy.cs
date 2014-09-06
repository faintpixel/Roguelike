using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DodongosQuest.Creatures
{
    public interface IDeathStrategy
    {
        void Die(Creature creature);
    }
}
