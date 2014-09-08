using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Creatures;
using DodongosQuest.Magic.Cast_Strategies;

namespace DodongosQuest.Magic
{
    public class SpellFactory
    {
        public static ISpell CreateHealSelfSpell(World world)
        {
            AreaOfEffect areaOfEffect = new AreaOfEffect(world);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(0, 0), 1);
            return new Spell("Heal Self", 2, areaOfEffect, false, world, new HealSelfStrategy(5, 20), false);
        }

        public static ISpell CreateTeleportSpell(World world)
        {
            AreaOfEffect areaOfEffect = new AreaOfEffect(world);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(0, 0), 1);
            return new Spell("Teleport", 2, areaOfEffect, true, world, new TeleportStrategy(), false);
        }

        public static ISpell CreateFireboltSpell(World world)
        {
            AreaOfEffect areaOfEffect = new AreaOfEffect(world);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(0, 0), 1);
            return new Spell("Firebolt", 5, areaOfEffect, true, world, new FireballStrategy(2, 8), false);
        }

        public static ISpell CreateFireballSpell(World world)
        {
            AreaOfEffect areaOfEffect = new AreaOfEffect(world);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(0, 0), 1);
            return new Spell("Fireball", 5, areaOfEffect, true, world, new FireballStrategy(10, 25), false);
        }

        public static ISpell CreateFlameSpell(World world)
        {
            AreaOfEffect areaOfEffect = new AreaOfEffect(world);
            //areaOfEffect.affectedBaseIndices.Add(new Vector2(-1, 0), 0.75);
            //areaOfEffect.affectedBaseIndices.Add(new Vector2(-2, 0), 0.50);
            //areaOfEffect.affectedBaseIndices.Add(new Vector2(-3, 0), 0.25);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(-1, 0), 1);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(-2, 0), 0.75);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(-3, 0), 0.5);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(-3, -1), 0.25);
            return new Spell("Flame", 5, areaOfEffect, false, world, new FireballStrategy(10, 25), false);
        }

        public static ISpell CreateInfernoSpell(World world)
        {
            AreaOfEffect areaOfEffect = new AreaOfEffect(world);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(0, 0), 1);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(-1, -1), 1);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(-1, 1), 1);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(1, -1), 1);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(1, 1), 1);
            return new Spell("Inferno", 5, areaOfEffect, true, world, new InfernoStrategy(10, 25), false);
        }

        public static ISpell CreateHasteSpell(World world)
        {
            // this should make the target creature move faster

            return null;
        }

        public static ISpell CreateFirewallSpell(World world)
        {
            // fire should burn anything on it right away, and any creatures that walk into should be burnt as they enter.

            AreaOfEffect areaOfEffect = new AreaOfEffect(world);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(-1, 0), 1);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(-2, 0), 1);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(0, 0), 1);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(1, 0), 1);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(2, 0), 1);
            areaOfEffect.affectedBaseIndices.Add(new Vector2(3, 0), 1);
 
            return new Spell("Firewall", 5, areaOfEffect, true, world, new FireballStrategy(10, 25), true);
        }

        public static ISpell CreateBarrierSpell(World world)
        {
            // this should make terrain not allow creatures/player to pass through it.

            return null;
        }
    }
}
