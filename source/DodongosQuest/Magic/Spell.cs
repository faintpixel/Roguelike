using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Creatures;
using DodongosQuest.Announcements;
using DodongosQuest.Magic.Cast_Strategies;

namespace DodongosQuest.Magic
{
    public class Spell : ISpell 
    {
        public int ManaCost { get; set; }
        public string Name { get; set; }
        public AreaOfEffect AffectedArea { get; set; }
        public bool TargetCanMove { get; set; }
        public bool AllowRotation { get; set; }

        private ICastStrategy _castStrategy;
        private World _world;

        public Spell(string name, int manaCost, AreaOfEffect affectedArea, bool targetCanMove, World world, ICastStrategy castStrategy, bool allowRotation)
        {
            Name = name;
            ManaCost = manaCost;
            _world = world;
            _castStrategy = castStrategy;
            TargetCanMove = targetCanMove;
            AffectedArea = affectedArea;
            AllowRotation = allowRotation;
        }

        public bool CastSpell(ICreature caster, Vector2 targetWorldIndex)
        {
            if (caster.Mana.Current - ManaCost < 0)
            {
                Announcer.Instance.Announce("Not enough mana to cast " + Name + ".", MessageTypes.Spell);
                return false;
            }
            else
            {
                Announcer.Instance.Announce(caster.Name + " casts " + Name + ".", MessageTypes.Spell);
                caster.Mana.Current -= ManaCost;
                return _castStrategy.CastSpell(caster, targetWorldIndex, AffectedArea, _world);
            }
        }

        public void Rotate()
        {
            if (AllowRotation)
            {
                Dictionary<Vector2, double> newBaseIndices = new Dictionary<Vector2, double>();
                foreach (KeyValuePair<Vector2, double> index in AffectedArea.affectedBaseIndices)
                    newBaseIndices.Add(new Vector2(index.Key.Y, index.Key.X), index.Value);

                AffectedArea.affectedBaseIndices = newBaseIndices;
            }
        }
    }
}
