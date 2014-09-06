using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace DodongosQuest.Creatures
{
    public delegate void TemporaryEffectEvent(ITemporaryEffect sender);

    public interface ITemporaryEffect
    {
        event TemporaryEffectEvent Completed;
        string Name { get; set; }
        Guid Id { get; set; }
        int TurnsRemaining { get; set; }
        void ApplyEffect(ICreature creature, World world);
        void UpdateEffect(ICreature creature, World world);
        ITemporaryEffect Clone();
    }
}
