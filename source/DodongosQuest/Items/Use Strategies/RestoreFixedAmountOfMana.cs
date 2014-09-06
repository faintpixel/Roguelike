using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Creatures;
using DodongosQuest.Items;
using DodongosQuest.Announcements;

namespace DodongosQuest.Items.Use_Strategies
{
    public class RestoreFixedAmountOfMana : IUseStrategy
    {
        private int _manaToRecover;

        public RestoreFixedAmountOfMana(int amountOfManaToRecover)
        {
            _manaToRecover = amountOfManaToRecover;
        }

        public void Use(IItem item, World world, ICreature user)
        {
            if (user.Mana.Current + _manaToRecover >= user.Mana.Maximum)
            {
                user.Mana.Current = user.Mana.Maximum;
                Announcer.Instance.Announce(user.Name + " regained all mana!", MessageTypes.Other);
            }
            else
            {
                user.Mana.Current += _manaToRecover;
                Announcer.Instance.Announce(user.Name + " recovered " + _manaToRecover + " mana.", MessageTypes.Other);
            }

            user.Inventory.Remove(item);
        }
    }
}
