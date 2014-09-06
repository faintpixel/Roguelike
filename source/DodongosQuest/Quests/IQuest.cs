using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DodongosQuest.Quests
{
    public delegate void QuestEvent(IQuest sender);

    public interface IQuest
    {
        event QuestEvent DoneWithQuest;
        string Name { get; }
        void AnnounceQuest();
        void Update(GameTime gameTime);
        void QuestCompleted();
    }
}
