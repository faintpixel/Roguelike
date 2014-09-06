using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Creatures;
using DodongosQuest.Items.Use_Strategies;
using DodongosQuest.Items.Get_Strategies;
using DodongosQuest.Items.Give_Strategies;

namespace DodongosQuest.Items
{
    public class Item : IItem 
    {
        public string Name { get; set; }
        public Vector2 WorldIndex { get; set; }
        public ItemCategories ItemCategory { get; set; }
        public ItemTypes ItemType { get; set; }
        private World _world;
        private IUseStrategy _useStrategy;
        private IGetStrategy _getStrategy;
        private IGiveStrategy _giveStrategy;

        public event ItemEvent GotItem;
        public event ItemEvent GiveItem;
        public event ItemEvent UseItem;
        public event ItemEvent DisposedItem;

        public Item(string name, World world, Vector2 worldIndex, IUseStrategy useStrategy, IGetStrategy getStrategy, IGiveStrategy giveStrategy, ItemCategories itemCategory, ItemTypes itemType)
        {
            Name = name;
            _world = world;
            WorldIndex = worldIndex;
            _useStrategy = useStrategy;
            _getStrategy = getStrategy;
            _giveStrategy = giveStrategy;
            ItemCategory = itemCategory;
            ItemType = itemType;
        }

        public void Get(ICreature getter)
        {
            _getStrategy.Get(this, _world, getter);

            if (GotItem != null)
                GotItem(this, getter);
        }

        public void Give(ICreature giver)
        {
            _giveStrategy.Give(this, _world, giver);

            if (GiveItem != null)
                GiveItem(this, giver);
        }

        public void Use(ICreature user)
        {
            _useStrategy.Use(this, _world, user);
            
            if (UseItem != null)
                UseItem(this, user);
        }
    }
}
