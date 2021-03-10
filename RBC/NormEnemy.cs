using System;
using System.Collections.Generic;

namespace RBC
{
    public class NormEnemy
    {
        public readonly string Name;
        public readonly List<string> Aliases;
        public int Level { get; private set; }
        public readonly string Hint;
        public double DMGDealt;
        public double DMGTaken;
        public int Strength { get; private set; }
        public int Endurance { get; private set; }
        public int Agility { get; private set; }
        public int Health => Endurance * 10 + Strength * 5;
        public int CurrentHealth { get; set; }

        public NormEnemy(string name, int level, List<string> aliases, string charType, string hint, int strength, int endurance, int agility)
        {
            Name = name;
            Aliases = aliases;
            Level = level;
            Type parsedEnum = Enum.Parse<Type>(charType, true);
            Hint = hint;
            Strength = strength;
            Endurance = endurance;
            Agility = agility;
            DMGDealt = 1;
            DMGTaken = 1;
        }
    }
}
