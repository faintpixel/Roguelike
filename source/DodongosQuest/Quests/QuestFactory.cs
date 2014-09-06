using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Quests.Quest_Completed_Strategies;
using DodongosQuest.Quests.Update_Strategies;
using DodongosQuest.Items;
using DodongosQuest.Creatures;

namespace DodongosQuest.Quests
{
    public class QuestFactory
    {
        public static IQuest MakeQuest(World world, ICreature target)
        {
            IQuestCompletedStrategy questCompletedStrategy = new ItemRewardStrategy(ItemFactory.CreateItem(DodongosQuest.Items.ItemTypes.PotionOfLevitation, new Vector2(0, 0), world));
            IUpdateStrategy updateStrategy = new KillCreatureStrategy(target);
            return new Quest("Kill the man eating plant", "You must seek out and destroy the man eating plant.", world, questCompletedStrategy, updateStrategy);
        }

        public static IQuest MakeItemFindQuest(World world, IItem target)
        {
            IQuestCompletedStrategy questCompletedStrategy = new ItemRewardStrategy(ItemFactory.CreateItem(DodongosQuest.Items.ItemTypes.BatWing, new Vector2(0, 0), world));
            IUpdateStrategy updateStrategy = new FindItemStrategy(target);
            return new Quest("Lost doll", "You must find the cursed doll.", world, questCompletedStrategy, updateStrategy);
        }
    }
}
