using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Creatures;
using DodongosQuest.Items;
using DodongosQuest.Announcements;

namespace DodongosQuest.Items.Get_Strategies
{
    public class StandardGetStrategy : IGetStrategy 
    {
        public void Get(IItem item, World world, ICreature getter)
        {            
            getter.Inventory.Add(item);
            Announcer.Instance.Announce(getter.Name + " got " + item.Name + ".", MessageTypes.GetItem);
        }
    }
}
