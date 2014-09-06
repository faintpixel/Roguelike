using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xnaHelper.Helpers;
using DodongosQuest.Creatures.AttackStrategies;
using DodongosQuest.Creatures.Remains;

namespace DodongosQuest.Creatures
{
    public class CreatureFactory
    {
        public static ICreature CreateCreature(Creatures creatureType, Vector2 worldIndex, World world)
        {
            if (creatureType == Creatures.GiantBat)
                return CreateGiantBat(worldIndex, world);
            else if (creatureType == Creatures.SkeletonWarrior)
                return CreateSkeletonWarrior(worldIndex, world);
            else if (creatureType == Creatures.Octopus)
                return CreateOctopus(worldIndex, world);
            else if (creatureType == Creatures.Golem)
                return CreateGolem(worldIndex, world);
            else if (creatureType == Creatures.ManEatingPlant)
                return CreateManEatingPlant(worldIndex, world);
            else if (creatureType == Creatures.WalkingDragon)
                return CreateWalkingDragon(worldIndex, world);
            else if (creatureType == Creatures.FireSpirit)
                return CreateFireSpirit(worldIndex, world);
            else
            {
                throw new NotImplementedException("Unknown creature type.");
            }
        }

        private static Creature CreateFireSpirit(Vector2 worldIndex, World world)
        {
            // should be immune to fire damage and randomly shoot fireballs
            return new Creature("Fire Spirit",
                worldIndex,
                MovementType.Flying,
                new Stat(10),
                new Stat(50),
                new WanderingSpellCasterTurnStrategy(1, world),
                new WalkingCreatureDrawStrategy(ContentHelper.Content.Load<Texture2D>(@"Creatures\FireSprite")),
                new StandardDeathStrategy(),
                new StandardAttackStrategy(),
                world,
                RemainsFactory.CreateBlood(worldIndex, world));
        }

        private static Creature CreateGiantBat(Vector2 worldIndex, World world)
        {
            return new Creature("Giant Bat",
                worldIndex,
                MovementType.Flying,
                new Stat(5),
                new Stat(5),
                new RandomTurnStrategy(1),
                new WalkingCreatureDrawStrategy(ContentHelper.Content.Load<Texture2D>("GiantBat")),
                new StandardDeathStrategy(),
                new StandardAttackStrategy(),
                world,
                RemainsFactory.CreateBatWing(worldIndex, world));
        }

        private static Creature CreateWalkingDragon(Vector2 worldIndex, World world)
        {
            return new Creature("Dragon",
                worldIndex,
                MovementType.Walking,
                new Stat(50),
                new Stat(200),
                new RandomTurnStrategy(3),
                new WalkingCreatureDrawStrategy(ContentHelper.Content.Load<Texture2D>(@"Creatures\Dragon")),
                new StandardDeathStrategy(),
                new ProjectileAttackStrategy(),
                world,
                null);
        }

        private static Creature CreateSkeletonWarrior(Vector2 worldIndex, World world)
        {
            return new Creature("Skeleton Warrior",
                worldIndex,
                MovementType.Walking,
                new Stat(10),
                new Stat(5),
                new RandomTurnStrategy(1),
                new WalkingCreatureDrawStrategy(ContentHelper.Content.Load<Texture2D>("SkeletonWarrior")),
                new StandardDeathStrategy(),
                new StandardAttackStrategy(),
                world,
                RemainsFactory.CreateBones(worldIndex, world));
        }

        private static Creature CreateOctopus(Vector2 worldIndex, World world)
        {
            return new Creature("Octopus",
                worldIndex,
                MovementType.Swimming,
                new Stat(6),
                new Stat(3),
                new RandomTurnStrategy(1),
                new WalkingCreatureDrawStrategy(ContentHelper.Content.Load<Texture2D>(@"Creatures\Octopus")),
                new StandardDeathStrategy(),
                new StandardAttackStrategy(),
                world,
                null);
        }

        private static Creature CreateGolem(Vector2 worldIndex, World world)
        {
            return new Creature("Golem",
                worldIndex, 
                MovementType.Walking, 
                new Stat(30), 
                new Stat(50),
                new RandomTurnStrategy(3),
                new WalkingCreatureDrawStrategy(ContentHelper.Content.Load<Texture2D>(@"creatures\golem")), 
                new StandardDeathStrategy(),
                new StandardAttackStrategy(), 
                world,
                RemainsFactory.CreateDust(worldIndex, world));
        }

        private static Creature CreateManEatingPlant(Vector2 worldIndex, World world)
        {
            return new Creature("Man Eating Plant",
                worldIndex,
                MovementType.Walking,
                new Stat(40),
                new Stat(0),
                new StationaryTurnStrategy(),
                new WalkingCreatureDrawStrategy(ContentHelper.Content.Load<Texture2D>(@"creatures\manEatingPlant")),
                new StandardDeathStrategy(),
                new StandardAttackStrategy(),
                world,
                RemainsFactory.CreateGreenGoo(worldIndex, world));
        }
    }
}
