using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Announcements;

namespace DodongosQuest.Creatures.Temporary_Effects
{
    public class ReducedVisionTemporaryEffect : ITemporaryEffect 
    {
        public event TemporaryEffectEvent Completed;

        public string Name { get; set; }
        public int TurnsRemaining { get; set; }
        public bool EffectComplete { get; set; }
        public Guid Id { get; set; }

        private int _amountToReduceVisionBy;

        public ReducedVisionTemporaryEffect(int amountToReduceVisionBy, int duration)
        {
            TurnsRemaining = duration;
            Name = "Reduced Vision";
            _amountToReduceVisionBy = amountToReduceVisionBy;
            Id = Guid.NewGuid();
        }

        public void ApplyEffect(ICreature creature, World world)
        {
            creature.ViewDistance.Current -= _amountToReduceVisionBy;
            if (creature.IsPlayer() == true)
                world.DiscoverTerrainAroundPlayer();
        }

        public void UpdateEffect(ICreature creature, World world)
        {
            if (EffectComplete == false)
            {
                if (TurnsRemaining <= 0)
                {
                    EffectCompleted(creature, world);
                    EffectComplete = true;
                }
                else
                {
                    TurnsRemaining -= 1;
                }
            }
        }

        public void EffectCompleted(ICreature creature, World world)
        {
            if (creature.ViewDistance.Current + _amountToReduceVisionBy >= creature.ViewDistance.Maximum)
            {
                creature.ViewDistance.Current = creature.ViewDistance.Maximum;
                Announcer.Instance.Announce(creature.Name + "'s vision returned to normal!", MessageTypes.Other);
            }
            else
            {
                creature.ViewDistance.Current += _amountToReduceVisionBy;
                Announcer.Instance.Announce(creature.Name + "'s vision improved.", MessageTypes.Other);
            }

            world.DiscoverTerrainAroundPlayer();

            if (Completed != null)
                Completed(this);
        }

        public ITemporaryEffect Clone()
        {
            return (ITemporaryEffect)this.MemberwiseClone();
        }
    }
}
