using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DodongosQuest.Screens
{
    public interface IScreen
    {
        void Draw(GameTime gameTime);
        void Update(GameTime gameTime);
    }
}
