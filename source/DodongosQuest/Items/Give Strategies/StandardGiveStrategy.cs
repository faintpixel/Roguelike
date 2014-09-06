using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Creatures;
using DodongosQuest.Items;
using DodongosQuest.Announcements;

namespace DodongosQuest.Items.Give_Strategies
{
    public class StandardGiveStrategy : IGiveStrategy 
    {
        public void Give(IItem item, World world, ICreature giver)
        {
            giver.Inventory.Remove(item);
        }
    }
}
