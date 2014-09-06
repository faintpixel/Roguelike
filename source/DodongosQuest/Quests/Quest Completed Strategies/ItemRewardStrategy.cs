using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DodongosQuest.Items;
using DodongosQuest.Announcements;

namespace DodongosQuest.Quests.Quest_Completed_Strategies
{
    public class ItemRewardStrategy : IQuestCompletedStrategy 
    {
        private IItem _itemToReward;

        public ItemRewardStrategy(IItem itemToReward)
        {
            _itemToReward = itemToReward;
        }

        public void QuestCompleted(IQuest quest, World world)
        {
            Announcer.Instance.Announce("You receive an item for your hard work.", MessageTypes.QuestInformation);
            _itemToReward.Get(world.Player);
        }
    }
}
