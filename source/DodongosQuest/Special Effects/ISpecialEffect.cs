using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DodongosQuest.Special_Effects
{
    public delegate void SpecialEffectEvent(ISpecialEffect sender);

    public interface ISpecialEffect
    {
        event SpecialEffectEvent Done;
        void Draw(GameTime gameTime);
        void Update(GameTime gameTime);
    }
}
