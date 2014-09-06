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
    public class FireballStrategy : ICastStrategy
    {
        private int _minimumDamage;
        private int _maximumDamage;
        private Dictionary<Vector2, double> affectedWorldIndicies = new Dictionary<Vector2,double>();
        private int damage = 0;

        public FireballStrategy(int minimumDamage, int maximumDamage)
        {
            _minimumDamage = minimumDamage;
            _maximumDamage = maximumDamage;           
        }

        public bool CastSpell(ICreature caster, Vector2 targetWorldIndex, AreaOfEffect affectedArea, World world)
        {
            affectedWorldIndicies = affectedArea.GetAffectedWorldIndices(targetWorldIndex);
            damage = RandomNumberProvider.GetRandomNumber(_minimumDamage, _maximumDamage);

            foreach (KeyValuePair<Vector2, double> affectedWorldIndex in affectedWorldIndicies)
            {
                ICreature target = world.GetCreatureAtIndex(affectedWorldIndex.Key);
                if (target != null)
                {
                    int actualDamage = (int)(damage * affectedWorldIndex.Value);
                    Announcer.Instance.Announce(target.Name + " was hit by a fireball for " + actualDamage + " damage!", MessageTypes.Spell);
                    target.Health.Current -= actualDamage;                    
                }
                world.AddSpecialEffectToWorld(new Fireball(caster.WorldIndex, affectedWorldIndex.Key, world));
            }
     
            return true;
        }
    }
}
