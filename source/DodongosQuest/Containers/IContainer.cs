using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Items;

namespace DodongosQuest.Containers
{
    public interface IContainer
    {
        void Draw(GameTime gameTime);
        void Update(GameTime gameTime);
        List<IItem> Contents { get; set; }
        Vector2 WorldIndex { get; set; }
        bool IsLocked { get; set; }
    }
}
