using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Creatures;
using DodongosQuest.Announcements;

namespace DodongosQuest.Quests.Update_Strategies
{
    public class KillCreatureStrategy : IUpdateStrategy 
    {
        private bool _targetKilled;
        private bool _targetSpotted;
        private ICreature _target;

        public KillCreatureStrategy(ICreature target)
        {
            _targetKilled = false;
            _targetSpotted = false;
            _target = target;
            target.Death += new CreatureEvent(target_Death);
        }

        void target_Death(ICreature sender)
        {
            _targetKilled = true;
        }

        public void Update(IQuest quest, ref World world, GameTime gameTime)
        {
            if (_targetKilled)
            {
                Announcer.Instance.Announce("You destroy the plant and complete your quest!", MessageTypes.QuestInformation);
                quest.QuestCompleted();                
            }
            else if (_targetSpotted == false)
            {
                if (world.CanPlayerSeeWorldIndex(_target.WorldIndex))
                {
                    Announcer.Instance.Announce("The great beast lurks before you. Your moment of victory is at hand!", MessageTypes.QuestInformation);
                    _targetSpotted = true;
                }
            }
        }
    }
}
