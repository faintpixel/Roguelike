using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Creatures;

namespace DodongosQuest.Magic.Cast_Strategies
{
    public interface ICastStrategy
    {
        bool CastSpell(ICreature caster, Vector2 targetWorldIndex, AreaOfEffect affectedArea, World world);
    }
}
