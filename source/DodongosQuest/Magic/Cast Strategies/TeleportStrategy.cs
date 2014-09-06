using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Creatures;
using DodongosQuest.Announcements;
using DodongosQuest.Terrain;
using DodongosQuest.Containers;


namespace DodongosQuest.Magic.Cast_Strategies
{
    public class TeleportStrategy : ICastStrategy
    {
        public bool CastSpell(ICreature caster, Vector2 targetWorldIndex, AreaOfEffect affectedArea, World world)
        {
            ITerrain targetTile = world.Tiles[(int)targetWorldIndex.X, (int)targetWorldIndex.Y];

            if (targetTile.IsExploredByPlayer && targetTile.CanCreatureEnter(caster) && world.IsWorldIndexFreeOfObstacles(targetWorldIndex))
            {
                caster.WorldIndex = targetWorldIndex;
                world.CenterCameraOnPlayer();
                world.DiscoverTerrainAroundPlayer();
            }
            else
            {
                Announcer.Instance.Announce(caster.Name + " tried to teleport, but could not.", MessageTypes.SpellFailure);
            }

            return true;
        }
    }
}
