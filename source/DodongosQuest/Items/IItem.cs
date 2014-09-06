using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Creatures;

namespace DodongosQuest.Items
{
    public delegate void ItemEvent(IItem sender, ICreature creature);

    public interface IItem
    {
        string Name { get; set; }
        Vector2 WorldIndex { get; set; }
        ItemCategories ItemCategory { get; }
        ItemTypes ItemType { get; }
        event ItemEvent GotItem;
        event ItemEvent GiveItem;
        event ItemEvent UseItem;
        event ItemEvent DisposedItem;
        void Get(ICreature getter);
        void Give(ICreature giver);
        void Use(ICreature user);
    }
}
