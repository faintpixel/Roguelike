using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;
using DodongosQuest.Creatures;
using DodongosQuest.Weapons;
using DodongosQuest.Announcements;
using DodongosQuest.Items;
using DodongosQuest.Creatures.Remains;
using DodongosQuest.Magic;
using DodongosQuest.Magic.Cast_Strategies;

namespace DodongosQuest.Creatures.Player
{
    public class PlayerCharacter : ICreature 
    {
        private Texture2D _tileImage;
        public Vector2 WorldIndex { get; set; }
        private World _world;
        public MovementType MovementType { get; set; }
        public IWeapon Weapon { get; set; }
        public Stat Health { get; set; }
        public Stat Mana { get; set; }
        public string Name { get; set; }
        public event CreatureEvent Death;
        public event CreatureFlameEvent FlamingAttack;
        public List<IItem> Inventory { get; set; }
        public List<ISpell> Spells { get; set; }
        public List<ITemporaryEffect> TemporaryEffects { get; set; }
        public Stat ViewDistance { get; set; }
        public IRemains Remains { get; set; }

        public PlayerCharacter(Vector2 worldIndex, World world)
        {
            _tileImage = ContentHelper.Content.Load<Texture2D>("HumanWarrior");
            WorldIndex = worldIndex;
            _world = world;
            Weapon = WeaponFactory.CreateSword(new Vector2(0, 0), world);
            Name = "Player";
            Health = new Stat(25);
            Mana = new Stat(20);
            MovementType = MovementType.Walking;
            Inventory = new List<IItem>();
            Spells = new List<ISpell>();
            TemporaryEffects = new List<ITemporaryEffect>();
            ViewDistance = new Stat(20);
            Inventory.Add((IItem)Weapon);
            Remains = RemainsFactory.CreateBones(new Vector2(0, 0), world);

            Spells.Add(SpellFactory.CreateHealSelfSpell(world));
            Spells.Add(SpellFactory.CreateFireballSpell(world));
            Spells.Add(SpellFactory.CreateInfernoSpell(world));
            Spells.Add(SpellFactory.CreateFlameSpell(world));
            Spells.Add(SpellFactory.CreateFirewallSpell(world));
            Spells.Add(SpellFactory.CreateTeleportSpell(world));
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
            Vector2 worldPosition = _world.ConvertTileIndexToWorldPosition(WorldIndex.X, WorldIndex.Y);
            Vector2 screenPosition = Camera.GetScreenPosition(worldPosition);
            GraphicsHelper.spriteBatch.Draw(_tileImage, screenPosition, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.3f);
        }

        public void AttackCreature(ref ICreature creature)
        {
            Weapon.Attack(this, creature);
        }

        public void TakeTurn()
        {
            for (int i = TemporaryEffects.Count - 1; i >= 0; i--)
                if (TemporaryEffects.Count > i)
                    TemporaryEffects[i].UpdateEffect(this, _world);
        }

        public bool IsPlayer()
        {
            return true;
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

        public void AddTemporaryEffect(ITemporaryEffect effect)
        {
            effect.ApplyEffect(this, _world);
            TemporaryEffects.Add(effect);
            effect.Completed += new TemporaryEffectEvent(TemporaryEffectCompleted);
        }

        void TemporaryEffectCompleted(ITemporaryEffect sender)
        {
            TemporaryEffects.Remove(sender);
        }
    }
}
