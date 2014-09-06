using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DodongosQuest.Creatures.Remains
{
    public interface IRemains
    {
        void Draw(GameTime gameTime);
        Vector2 WorldIndex { get; set; }
    }
}
