using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RBC
{
    public class Character
    {
        public readonly string Name;
        public readonly string[] Aliases;
        public int Level { get; private set; }
        public double CurrentExp { get; private set; }
        public double LevelUpReq => Level * Level + Level * 10;
        public readonly Type CharType;
        public readonly NP NoblePhantasm;
        public readonly string Hint;
        public Stats CharacterStat;
        public double DMGDealt;
        public double DMGTaken;
        public int Health => CharacterStat.Endurance * 15;
        public int CurrentHealth { get; set; }
        public int AvStatPoints => Level * 8 - (CharacterStat.Strength + CharacterStat.Endurance + CharacterStat.Agility + CharacterStat.Mana + Level * 4) + (int)(CharacterStat.characterStarterStrength + CharacterStat.characterStarterEndurance + CharacterStat.characterStarterAgility + CharacterStat.characterStarterMana);
        public DMGMatter DMGMatter;

        public Character(string name, string[] aliases, string charType, NP noblePhantasm, string hint, int strength, int endurance, int agility, int mana, string dMGMatter)
        {
            Name = name;
            Aliases = aliases;
            Level = 1;
            Type parsedEnum = Enum.Parse<Type>(charType, true);
            CharType = parsedEnum;
            NoblePhantasm = noblePhantasm;
            Hint = hint;
            CharacterStat = new Stats(strength, endurance, agility, mana);
            DMGDealt = 1;
            DMGTaken = 1;
            DMGMatter secondParsedEnum = Enum.Parse<DMGMatter>(dMGMatter, true);
            DMGMatter = secondParsedEnum;
            CurrentHealth = Health;
        }

        public void GainExp(double amount)
        {
            CurrentExp += amount;
            bool levelUp = CurrentExp > LevelUpReq;
            if (levelUp)
            {
                CurrentExp -= LevelUpReq;
                Level++;
                Console.Clear();
                Console.WriteLine($"{Name} leveled up to {Level}.");
                Thread.Sleep(1500);
            }
        }

        public void StrengthRise(int amount)
        {
            CharacterStat.StrengthRise(amount);
        }
        public void EnduranceRise(int amount)
        {
            CharacterStat.EnduranceRise(amount);
        }
        public void AgilityRise(int amount)
        {
            CharacterStat.AgilityRise(amount);
        }
        public void ManaRise(int amount)
        {
            CharacterStat.ManaRise(amount);
        }
        public void MatterMana()
        {
            DMGMatter = DMGMatter.Mana;
        }
    }
}
