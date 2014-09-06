using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DodongosQuest.Items;
using DodongosQuest.Creatures;
using DodongosQuest.Announcements;

namespace DodongosQuest.Items.Use_Strategies
{
    public class DoNothing : IUseStrategy 
    {
        public void Use(IItem item, World world, ICreature user)
        {
            Announcer.Instance.Announce(user.Name + " admires " + item.Name + ".", MessageTypes.Other);
        }
    }
}
