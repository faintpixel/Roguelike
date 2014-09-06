using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Items;
using DodongosQuest.Weapons;
using DodongosQuest.Creatures.Remains;
using DodongosQuest.Magic;

namespace DodongosQuest.Creatures
{
    public delegate void CreatureEvent(ICreature sender);
    public delegate void CreatureFlameEvent(ICreature sender);


    public interface ICreature
    {        
        event CreatureEvent Death;
        event CreatureFlameEvent FlamingAttack;
        MovementType MovementType { get; set; }
        Vector2 WorldIndex { get; set; }
        string Name { get; }
        Stat Health { get; set; }
        Stat Mana { get; set; }
        IWeapon Weapon { get; set; }
        void Draw(GameTime gameTime);
        void Update(GameTime gameTime);
        void TakeTurn();
        void AttackCreature(ref ICreature creature);
        bool IsPlayer();
        List<IItem> Inventory { get; set; }
        List<ISpell> Spells { get; set; }
        List<ITemporaryEffect> TemporaryEffects { get; set; }
        void AddTemporaryEffect(ITemporaryEffect effect);
        Stat ViewDistance { get; set; }
        IRemains Remains { get; set; }
        IItem GetItemFromInventory(Items.ItemTypes itemType);
    }
}
