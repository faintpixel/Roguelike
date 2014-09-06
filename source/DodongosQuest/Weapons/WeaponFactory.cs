using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Items;
using DodongosQuest.Items.Get_Strategies;
using DodongosQuest.Items.Give_Strategies;
using DodongosQuest.Items.Use_Strategies;
using DodongosQuest.Weapons.Attack_Strategies;
using DodongosQuest.Creatures.Temporary_Effects;

namespace DodongosQuest.Weapons
{
    public class WeaponFactory
    {
        public static IWeapon CreateSword(Vector2 worldIndex, World world)
        {
            IUseStrategy useStrategy = new StandardEquipWeaponStrategy();
            IGetStrategy getStrategy = new StandardGetStrategy();
            IGiveStrategy giveStrategy = new StandardGiveStrategy();
            List<IAttackStrategy> attacks = new List<IAttackStrategy>();
            attacks.Add(new PhysicalAttack(1, 6, 90));

            return new Weapon("Sword",
                worldIndex,
                world,
                useStrategy,
                getStrategy,
                giveStrategy,
                attacks,
                1);
        }

        public static IWeapon CreateGiantAxe(Vector2 worldIndex, World world)
        {
            IUseStrategy useStrategy = new StandardEquipWeaponStrategy();
            IGetStrategy getStrategy = new StandardGetStrategy();
            IGiveStrategy giveStrategy = new StandardGiveStrategy();
            List<IAttackStrategy> attacks = new List<IAttackStrategy>();
            attacks.Add(new PhysicalAttack(1, 6, 90));
            attacks.Add(new ApplyTemporaryEffectToAttacker(new Creatures.Temporary_Effects.ReducedVisionTemporaryEffect(18, 1)));

            return new Weapon("Giant Axe Of Blinding Rage",
                worldIndex,
                world,
                useStrategy,
                getStrategy,
                giveStrategy,
                attacks,
                1);
        }

        public static IWeapon CreateBow(Vector2 worldIndex, World world)
        {
            IUseStrategy useStrategy = new StandardEquipWeaponStrategy();
            IGetStrategy getStrategy = new StandardGetStrategy();
            IGiveStrategy giveStrategy = new StandardGiveStrategy();
            List<IAttackStrategy> attacks = new List<IAttackStrategy>();
            attacks.Add(new ProjectileAttackStrategy(1, 3, 75));

            return new Weapon("Fire Bow",
                worldIndex,
                world,
                useStrategy,
                getStrategy,
                giveStrategy,
                attacks,
                10);
        }
    }
}
