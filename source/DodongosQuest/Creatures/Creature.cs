using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Items;
using DodongosQuest.Creatures.AttackStrategies;
using DodongosQuest.Weapons;
using DodongosQuest.Creatures.Remains;
using DodongosQuest.Magic;

namespace DodongosQuest.Creatures
{
    public class Creature : ICreature 
    {
        public MovementType MovementType { get; set; }
        public Vector2 WorldIndex { get; set; }
        public string Name { get; set; }
        public Stat Health { get; set; }
        public Stat Mana { get; set; }
        public ITurnStrategy TurnStrategy { get; set; }
        public IDrawStrategy DrawStrategy { get; set; }
        public IDeathStrategy DeathStrategy { get; set; }
        public IAttackStrategy AttackStrategy { get; set; }
        public List<IItem> Inventory { get; set; }
        public List<ISpell> Spells { get; set; }
        public List<ITemporaryEffect> TemporaryEffects { get; set; }
        public IWeapon Weapon { get; set; }
        public Stat ViewDistance { get; set; }
        private World _world;
        public IRemains Remains { get; set; }

        public event CreatureEvent Death;
        public event CreatureFlameEvent FlamingAttack;
      

        public Creature(string name, Vector2 worldIndex, MovementType movementType, Stat hitPoints, Stat mana, ITurnStrategy turnStrategy, IDrawStrategy drawStrategy, IDeathStrategy deathStrategy, IAttackStrategy attackStrategy, World world, IRemains remains)
        {
            MovementType = movementType;
            WorldIndex = worldIndex;
            Name = name;
            Health = hitPoints;
            Mana = mana;
            TurnStrategy = turnStrategy;
            DrawStrategy = drawStrategy;
            DeathStrategy = deathStrategy;
            AttackStrategy = attackStrategy;
            _world = world;
            Inventory = new List<IItem>();
            Spells = new List<ISpell>();
            TemporaryEffects = new List<ITemporaryEffect>();
            ViewDistance = new Stat(15);
            Remains = remains;
        }        

        public void Draw(GameTime gameTime)
        {
            DrawStrategy.Draw(gameTime, this, _world);
        }

        public void Update(GameTime gameTime)
        {
            if (Health.Current <= 0)
                DeathStrategy.Die(this);
        }

        public void TakeTurn()
        {
            TurnStrategy.TakeTurn(this, _world);
        }

        public void AttackCreature(ref ICreature creature)
        {
            if (FlamingAttack != null)
            {
                FlamingAttack(this);
            }
            AttackStrategy.AttackCreature(this, ref creature);
        }

        public void Die()
        {
            if (Death != null)
                Death(this);
        }

        public bool IsPlayer()
        {
            return false;
        }

        public void AddTemporaryEffect(ITemporaryEffect effect)
        {
            TemporaryEffects.Add(effect);
            effect.Completed += new TemporaryEffectEvent(TemporaryEffectCompleted);
        }

        void TemporaryEffectCompleted(ITemporaryEffect sender)
        {
            TemporaryEffects.Remove(sender);
        }

        public IItem GetItemFromInventory(Items.ItemTypes itemType)
        {
            foreach (IItem item in Inventory)
            {
                if (item.ItemType == itemType)
                {
                    return item;
                    break;
                }
            }

            return null;
        }

    }
}
