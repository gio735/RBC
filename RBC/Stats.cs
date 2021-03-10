using System;
using System.Collections.Generic;
using System.Text;

namespace RBC
{
    public class Stats
    {
        public readonly int? characterStarterStrength;
        public readonly int? characterStarterEndurance;
        public readonly int? characterStarterAgility;
        public readonly int? characterStarterMana;
        public int Strength { get; private set; }
        public int Endurance { get; private set; }
        public int Agility { get; private set; }
        public int Mana { get; private set; }

        public Stats(int? starterStrengrth = null, int? starterEndurance = null, int? starterAgility = null, int? starterMana = null)
        {
            characterStarterStrength = starterStrengrth;
            characterStarterEndurance = starterEndurance;
            characterStarterAgility = starterAgility;
            characterStarterMana = starterMana;
            if (starterStrengrth == null)
            {
                Strength = 1;
                Endurance = 1;
                Agility = 1;
                Mana = 1;
            }
            else
            {
                Strength = (int)starterStrengrth;
                Endurance = (int)starterEndurance;
                Agility = (int)starterAgility;
                Mana = (int)starterMana;
            }
            
        }

        public void StatUp(Stat statType, int amount, bool isMaster)
        {
            if (statType == Stat.Str) statType = Stat.Strength;
            else if (statType == Stat.End) statType = Stat.Endurance;
            else if (statType == Stat.Agi) statType = Stat.Agility;
            if (statType == Stat.Strength)
            {
                Strength += amount;
            }
            else if (statType == Stat.Endurance)
            {
                Endurance += amount;
            }
            else if (statType == Stat.Agility)
            {
                Agility += amount;
            }
            else
            {
                Mana += amount;
            }
            Message(statType, amount, isMaster);
            
        }
        private void Message(Stat statName, int amount, bool isMaster)
        {
            if (isMaster)
            {
                Console.WriteLine($"Successfully added {amount} point(s) to master's {statName} stat.");
            }
            else
            {
                Console.WriteLine($"Successfully added {amount} point(s) to servant's {statName} stat.");
            }
            
        }
        public void LvlUp()
        {
            Strength++;
            Endurance++;
            Agility++;
            Mana++;
        }
        public void StrengthRise(int amount)
        {
            Strength += amount;
        }
        public void EnduranceRise(int amount)
        {
            Endurance += amount;
        }
        public void AgilityRise(int amount)
        {
            Agility += amount;
        }
        public void ManaRise(int amount)
        {
            Mana += amount;
        }
    }
}
