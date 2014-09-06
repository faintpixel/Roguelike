using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DodongosQuest.Creatures;
using DodongosQuest.Items;

namespace DodongosQuest.Items.Use_Strategies
{
    public interface IUseStrategy
    {
        void Use(IItem item, World world, ICreature user);
    }
}
