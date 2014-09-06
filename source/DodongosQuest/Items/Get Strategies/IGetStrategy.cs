using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DodongosQuest.Items;
using DodongosQuest.Creatures;

namespace DodongosQuest.Items.Get_Strategies
{
    public interface IGetStrategy
    {
        void Get(IItem item, World world, ICreature getter);
    }
}
