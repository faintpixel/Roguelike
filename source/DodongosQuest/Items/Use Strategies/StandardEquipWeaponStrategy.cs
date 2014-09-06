using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Creatures;
using DodongosQuest.Announcements;
using DodongosQuest.Weapons;

namespace DodongosQuest.Items.Use_Strategies
{
    public class StandardEquipWeaponStrategy : IUseStrategy 
    {
        public void Use(IItem item, World world, ICreature user)
        {
            if (item.ItemCategory == ItemCategories.Weapon)
            {
                user.Weapon = (IWeapon)item;
                Announcer.Instance.Announce(user.Name + " is now using " + item.Name + ".", MessageTypes.GetItem);
            }
        }
    }
}
