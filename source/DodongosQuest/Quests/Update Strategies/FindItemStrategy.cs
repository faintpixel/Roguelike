using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Items;
using DodongosQuest.Announcements;
using DodongosQuest.Creatures;

namespace DodongosQuest.Quests.Update_Strategies
{
    public class FindItemStrategy : IUpdateStrategy 
    {
        private bool _foundItem;

        public FindItemStrategy(IItem target)
        {
            _foundItem = false;
            target.GotItem += new ItemEvent(GotTargetItem);
        }

        void GotTargetItem(IItem sender, ICreature creature)
        {
            _foundItem = true;
        }

        public void Update(IQuest quest, ref World world, GameTime gameTime)
        {
            if (_foundItem)
            {
                Announcer.Instance.Announce("You find the cursed doll and complete your quest!", MessageTypes.QuestInformation);
                quest.QuestCompleted();
            }
        }
    }
}
