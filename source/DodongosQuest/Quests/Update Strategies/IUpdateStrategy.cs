using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DodongosQuest.Quests.Update_Strategies
{
    public interface IUpdateStrategy
    {
        void Update(IQuest quest, ref World world, GameTime gameTime);
    }
}
