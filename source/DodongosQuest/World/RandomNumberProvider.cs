using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DodongosQuest
{
    public class RandomNumberProvider
    {
        private static Random _randomProvider = new Random();

        public static int GetRandomNumber(int minimumValue, int maximumValue)
        {
            return _randomProvider.Next(minimumValue, maximumValue + 1);
        }

        public static bool CheckIfChanceOccurs(int percentChance)
        {
            int outcome = GetRandomNumber(0, 100);
            if (outcome <= percentChance)
                return true;
            else
                return false;
        }

        public static float RandomBetween(float min, float max)
        {
            return min + (float)_randomProvider.NextDouble() * (max - min);
        }
    }
}
