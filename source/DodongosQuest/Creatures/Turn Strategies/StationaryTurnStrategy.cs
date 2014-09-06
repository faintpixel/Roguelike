using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DodongosQuest.Creatures
{
    public class StationaryTurnStrategy : ITurnStrategy
    {
        public void TakeTurn(ICreature creature, World world)
        {
            List<Vector2> surroungingPoints = world.GetSurroundingWorldIndexPositions(creature.WorldIndex);
            if(surroungingPoints.Contains(world.Player.WorldIndex))
            {
                ICreature target = world.Player;
                creature.AttackCreature(ref target);
            }
        }
    }
}
