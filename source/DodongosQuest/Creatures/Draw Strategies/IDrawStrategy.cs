using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DodongosQuest.Creatures
{
    public interface IDrawStrategy
    {
        void Draw(GameTime gameTime, ICreature creature, World world);
    }
}
