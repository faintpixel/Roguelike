using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Announcements;
using DodongosQuest.Creatures.Temporary_Effects;
using DodongosQuest.Creatures;

namespace DodongosQuest.Items.Give_Strategies
{
    public class RemoveTemporaryEffectGiveStrategy : IGiveStrategy 
    {
        private IGiveStrategy _standardGive;
        private Guid _id;

        public RemoveTemporaryEffectGiveStrategy(Guid effectId)
        {
            _id = effectId;
            _standardGive = new StandardGiveStrategy();
        }

        public void Give(IItem item, World world, ICreature giver)
        {
            for (int i = giver.TemporaryEffects.Count - 1; i >= 0; i--)
                if (giver.TemporaryEffects[i].Id == _id)
                {
                    giver.TemporaryEffects[i].TurnsRemaining = 0;
                    break;
                }
            _standardGive.Give(item, world, giver);
        }
    }
}
