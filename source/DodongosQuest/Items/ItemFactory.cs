using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DodongosQuest.Items.Use_Strategies;
using DodongosQuest.Items.Get_Strategies;
using DodongosQuest.Items.Give_Strategies;
using DodongosQuest.Creatures;
using DodongosQuest.Creatures.Temporary_Effects;

namespace DodongosQuest.Items
{
    public class ItemFactory
    {
        public static IItem CreateItem(ItemTypes itemType, Vector2 worldIndex, World world)
        {
            if (itemType == ItemTypes.Diamond)
                return CreateDiamond(worldIndex, world);
            else if (itemType == ItemTypes.BatWing)
                return CreateBatWing(worldIndex, world);
            else if (itemType == ItemTypes.PotionOfMinorHealth)
                return CreatePotionOfMinorHealth(worldIndex, world);
            else if (itemType == ItemTypes.PotionOfMinorEnergy)
                return CreatePotionOfMinorEnergy(worldIndex, world);
            else if (itemType == ItemTypes.PotionOfLevitation)
                return CreatePotionOfLevitation(worldIndex, world);
            else if (itemType == ItemTypes.PotionOfBlindness)
                return CreatePotionOfBlindness(worldIndex, world);
            else if (itemType == ItemTypes.CursedDoll)
                return CreateCursedDoll(worldIndex, world);
            else if (itemType == ItemTypes.OldKey)
                return CreateOldKey(worldIndex, world);
            else
                throw new NotImplementedException("Unable to create item.");
        }

        private static IItem CreateOldKey(Vector2 worldIndex, World world)
        {
            return new Item("Old Key", world, worldIndex, new DoNothing(), new StandardGetStrategy(), new StandardGiveStrategy(), ItemCategories.Key, ItemTypes.OldKey);
        }

        private static IItem CreateDiamond(Vector2 worldIndex, World world)
        {
            return new Item("Diamond", world, worldIndex, new DoNothing(), new StandardGetStrategy(), new StandardGiveStrategy(), ItemCategories.Treasure, ItemTypes.Diamond);
        }

        private static IItem CreateBatWing(Vector2 worldIndex, World world)
        {
            return new Item("Bat Wing", world, worldIndex, new DoNothing(), new StandardGetStrategy(), new StandardGiveStrategy(), ItemCategories.Treasure, ItemTypes.BatWing);
        }

        private static IItem CreatePotionOfMinorHealth(Vector2 worldIndex, World world)
        {
            return new Item("Potion of Minor Healing", world, worldIndex, new RestoreFixedAmountOfHealth(10), new StandardGetStrategy(), new StandardGiveStrategy(), ItemCategories.Potion, ItemTypes.PotionOfMinorHealth);
        }

        private static IItem CreatePotionOfMinorEnergy(Vector2 worldIndex, World world)
        {
            return new Item("Potion of Minor Energy", world, worldIndex, new RestoreFixedAmountOfMana(10), new StandardGetStrategy(), new StandardGiveStrategy(), ItemCategories.Potion, ItemTypes.PotionOfMinorEnergy);
        }

        private static IItem CreatePotionOfLevitation(Vector2 worldIndex, World world)
        {
            ITemporaryEffect effect = new MovementTypeTemporaryEffect(MovementType.Flying, 25);
            return new Item("Potion of Levitation", world, worldIndex, new ApplyTemporaryEffect(effect), new StandardGetStrategy(), new StandardGiveStrategy(), ItemCategories.Potion, ItemTypes.PotionOfLevitation);
        }

        private static IItem CreatePotionOfBlindness(Vector2 worldIndex, World world)
        {
            ITemporaryEffect effect = new ReducedVisionTemporaryEffect(100, 25);
            return new Item("Potion of Blindness", world, worldIndex, new ApplyTemporaryEffect(effect), new StandardGetStrategy(), new StandardGiveStrategy(), ItemCategories.Potion, ItemTypes.PotionOfBlindness);
        }

        private static IItem CreateCursedDoll(Vector2 worldIndex, World world)
        {
            ITemporaryEffect effect = new ReducedVisionTemporaryEffect(10, 99999);            
            return new Item("Cursed Doll", world, worldIndex, new DestroyUseStrategy(), new ApplyTemporaryEffectGetStrategy(effect), new RemoveTemporaryEffectGiveStrategy(effect.Id), ItemCategories.Treasure, ItemTypes.CursedDoll);
        }
    }
}
