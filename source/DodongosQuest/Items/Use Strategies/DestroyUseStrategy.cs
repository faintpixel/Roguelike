using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Creatures;
using DodongosQuest.Announcements;

namespace DodongosQuest.Items.Use_Strategies
{
    public class DestroyUseStrategy : IUseStrategy 
    {
        public void Use(IItem item, World world, ICreature user)
        {
            Announcer.Instance.Announce(user.Name + " destroys " + item.Name + "!", MessageTypes.Other);
            item.Give(user);
        }
    }
}
