using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DodongosQuest.Items;
using DodongosQuest.Creatures;
using DodongosQuest.Items.Get_Strategies;
using DodongosQuest.Items.Give_Strategies;
using DodongosQuest.Items.Use_Strategies;
using DodongosQuest.Weapons.Attack_Strategies;

namespace DodongosQuest.Weapons
{
    public class Weapon : IItem, IWeapon 
    {
        public string Name { get; set; }
        public Vector2 WorldIndex { get; set; }
        public ItemCategories ItemCategory { get; set; }
        public ItemTypes ItemType { get; set; }
        private IUseStrategy _useStrategy;
        private IGetStrategy _getStrategy;
        private IGiveStrategy _giveStrategy;
        private List<IAttackStrategy> _attacks;
        public int Range { get; set; }
        private World _world;

        public event ItemEvent GotItem;
        public event ItemEvent GiveItem;
        public event ItemEvent UseItem;
        public event ItemEvent DisposedItem;

        public Weapon(string name, Vector2 worldIndex, World world, IUseStrategy useStrategy, IGetStrategy getStrategy, IGiveStrategy giveStrategy, List<IAttackStrategy> attacks, int range)
        {
            Name = name;
            WorldIndex = worldIndex;
            _useStrategy = useStrategy;
            _getStrategy = getStrategy;
            _giveStrategy = giveStrategy;
            _attacks = attacks;
            _world = world;
            ItemCategory = ItemCategories.Weapon;
            ItemType = ItemTypes.Diamond;
            Range = range;
        }

        public void Attack(ICreature attacker, ICreature target)
        {
            if (_world.GetStraightLineDistance(attacker.WorldIndex, target.WorldIndex) <= Range)
            {
                foreach (IAttackStrategy attack in _attacks)
                    attack.Attack(this, attacker, target, _world);
            }
            else
                Announcements.Announcer.Instance.Announce("Target out of " + attacker.Name + "'s range.", DodongosQuest.Announcements.MessageTypes.Other);
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
