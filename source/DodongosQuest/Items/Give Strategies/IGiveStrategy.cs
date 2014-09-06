using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DodongosQuest.Creatures;
using DodongosQuest.Items;

namespace DodongosQuest.Items.Give_Strategies
{
    public interface IGiveStrategy
    {
        void Give(IItem item, World world, ICreature giver);
    }
}
