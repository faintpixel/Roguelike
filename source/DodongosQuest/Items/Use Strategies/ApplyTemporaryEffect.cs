using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Creatures;
using DodongosQuest.Creatures.Temporary_Effects;
using DodongosQuest.Announcements;


namespace DodongosQuest.Items.Use_Strategies
{
    public class ApplyTemporaryEffect : IUseStrategy 
    {
        private ITemporaryEffect _effect;

        public ApplyTemporaryEffect(ITemporaryEffect effect)
        {
            _effect = effect;
        }

        public void Use(IItem item, World world, ICreature user)
        {
            Announcer.Instance.Announce(user.Name + " feels different. ", MessageTypes.Other);
            Announcer.Instance.Announce(user.Name + " is under the affect of " + _effect.Name + "!", MessageTypes.Other);
            user.AddTemporaryEffect(_effect);
            user.Inventory.Remove(item);
        }
    }
}
