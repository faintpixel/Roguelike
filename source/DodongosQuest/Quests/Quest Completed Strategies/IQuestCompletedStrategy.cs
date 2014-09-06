using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DodongosQuest.Quests.Quest_Completed_Strategies
{
    public interface IQuestCompletedStrategy
    {
        void QuestCompleted(IQuest quest, World world);
    }
}
