using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Creatures;
using DodongosQuest.Creatures.Temporary_Effects;
using DodongosQuest.Announcements;

namespace DodongosQuest.Items.Get_Strategies
{
    public class ApplyTemporaryEffectGetStrategy : IGetStrategy 
    {
        private ITemporaryEffect _effect;
        private StandardGetStrategy _standardGet;

        public ApplyTemporaryEffectGetStrategy(ITemporaryEffect effect)
        {
            _effect = effect;
            _standardGet = new StandardGetStrategy();
        }

        public void Get(IItem item, World world, ICreature getter)
        {
            Announcer.Instance.Announce(getter.Name + " feels different. ", MessageTypes.Other);
            Announcer.Instance.Announce(getter.Name + " is under the affect of " + _effect.Name + "!", MessageTypes.Other);
            getter.AddTemporaryEffect(_effect);

            _standardGet.Get(item, world, getter);
        }
    }
}
