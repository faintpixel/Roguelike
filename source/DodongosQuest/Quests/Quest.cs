using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Announcements;
using DodongosQuest.Quests.Quest_Completed_Strategies;
using DodongosQuest.Quests.Update_Strategies;

namespace DodongosQuest.Quests
{
    public class Quest : IQuest 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        private World _world;
        private IQuestCompletedStrategy _questCompletedStrategy;
        private IUpdateStrategy _updateStrategy;

        public event QuestEvent DoneWithQuest;

        public Quest(string name, string description, World world, IQuestCompletedStrategy questCompletedStrategy, IUpdateStrategy updateStrategy)
        {
            IsCompleted = false;
            Name = name;
            Description = description;
            _world = world;
            _questCompletedStrategy = questCompletedStrategy;
            _updateStrategy = updateStrategy;
        }

        public void AnnounceQuest()
        {
            Announcer.Instance.Announce("You have received a new quest!", MessageTypes.QuestInformation);
            Announcer.Instance.Announce(Description, MessageTypes.QuestInformation);
        }

        public void Update(GameTime gameTime)
        {
            if(IsCompleted == false)
                _updateStrategy.Update(this, ref _world, gameTime);
        }

        public void QuestCompleted()
        {
            IsCompleted = true;

            _questCompletedStrategy.QuestCompleted(this, _world);

            if (DoneWithQuest != null)
                DoneWithQuest(this);
        }
    }
}
