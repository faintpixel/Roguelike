using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Announcements;

namespace DodongosQuest.Creatures
{
    public class MovementTypeTemporaryEffect : ITemporaryEffect 
    {
        public string Name { get; set; }
        public int TurnsRemaining { get; set; }
        public bool EffectComplete { get; set; }
        public Guid Id { get; set; }
        private MovementType _creaturesOriginalMovementType;
        private MovementType _movementType;

        public event TemporaryEffectEvent Completed;

        public MovementTypeTemporaryEffect(MovementType movementType, int duration)
        {
            TurnsRemaining = duration;            
            _movementType = movementType;
            EffectComplete = false;
            Name = movementType.ToString();
            Id = Guid.NewGuid();
        }

        public void ApplyEffect(ICreature creature, World world)
        {
            _creaturesOriginalMovementType = creature.MovementType;
            creature.MovementType = _movementType;
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
                    if (TurnsRemaining == 5)
                        Announcer.Instance.Announce(creature.Name + " feels the potion wearing off.", MessageTypes.Other);
                    else if (TurnsRemaining == 10)
                        Announcer.Instance.Announce(creature.Name + " feels his movement returning to normal.", MessageTypes.Other);
                    TurnsRemaining -= 1;
                }
            }
        }

        public void EffectCompleted(ICreature creature, World world)
        {
            creature.MovementType = _creaturesOriginalMovementType;
            Announcer.Instance.Announce(creature.Name + "'s movement returned to normal.", MessageTypes.Other);
            if (Completed != null)
                Completed(this);
        }

        public ITemporaryEffect Clone()
        {
            return (ITemporaryEffect)this.MemberwiseClone();
        }
    }
}
