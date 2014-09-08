using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DodongosQuest.Creatures;
using Microsoft.Xna.Framework;

namespace DodongosQuest.Magic
{
    public interface ISpell
    {
        int ManaCost { get; }
        string Name { get; }
        AreaOfEffect AffectedArea { get; }
        bool TargetCanMove { get; }
        bool AllowRotation { get; }
        bool CastSpell(ICreature caster, Vector2 targetWorldIndex);
        void Rotate();
    }
}
