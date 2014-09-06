using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Creatures;
using DodongosQuest.Announcements;
using DodongosQuest.Special_Effects;

namespace DodongosQuest.Magic.Cast_Strategies
{
    public class HealSelfStrategy : ICastStrategy 
    {
        private int _minimumHealthToRestore;
        private int _maximumHealthToRestore;

        public HealSelfStrategy(int minimumHealthToRestore, int maximumHealthToRestore)
        {
            _minimumHealthToRestore = minimumHealthToRestore;
            _maximumHealthToRestore = maximumHealthToRestore;
        }

        public bool CastSpell(ICreature caster, Vector2 targetWorldIndex, AreaOfEffect affectedArea, World world)
        {
            int healthRestored = RandomNumberProvider.GetRandomNumber(_minimumHealthToRestore, _maximumHealthToRestore);
            if (caster.Health.Current + healthRestored >= caster.Health.Maximum)
            {
                caster.Health.Current = caster.Health.Maximum;
                Announcer.Instance.Announce(caster.Name + " returned to full health!", MessageTypes.Spell);
            }
            else
            {
                caster.Health.Current += healthRestored;
                Announcer.Instance.Announce(caster.Name + " recovered " + healthRestored + " health.", MessageTypes.Spell);
            }
            world.AddSpecialEffectToWorld(new Heal(caster.WorldIndex, caster.WorldIndex, world));
            return true;
        }
    }
}
