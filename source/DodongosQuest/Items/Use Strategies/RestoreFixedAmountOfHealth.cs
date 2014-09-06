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
    public class RestoreFixedAmountOfHealth : IUseStrategy 
    {
        private int _healthToRecover;

        public RestoreFixedAmountOfHealth(int amountOfHealthToRecover)
        {
            _healthToRecover = amountOfHealthToRecover;
        }

        public void Use(IItem item, World world, ICreature user)
        {
            if (user.Health.Current + _healthToRecover >= user.Health.Maximum)
            {
                user.Health.Current = user.Health.Maximum;
                Announcer.Instance.Announce(user.Name + " returned to full health!", MessageTypes.Other);
            }
            else
            {
                user.Health.Current += _healthToRecover;
                Announcer.Instance.Announce(user.Name + " recovered " + _healthToRecover + " health.", MessageTypes.Other);
            }

            user.Inventory.Remove(item);
        }


    }
}
