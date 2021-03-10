using System;
using System.Collections.Generic;
using System.Text;

namespace RBC
{
    public class NP
    {
        public readonly string Name;
        private int DMGAmount;
        private double DMGDealtMult;
        private double DMGTakenMult;
        private double StrengthAwakened;
        private double EnduranceAwakened;
        private double AgilityAwakened;
        private double ManaAwakened;
        private double Heal;
        private DMGMatter DMGMatterSwitch;
        //public Effect GoodEffect;
        //public Effect BadEffect;
        public NP(string name, int dMGAmount, double dMGDealtMult, double dMGTakenMult, double strengthAwakened, double enduranceAwakened, double agilityAwakened, double manaAwakened, double heal, string newMatter)
        {
            Name = name;
            DMGAmount = dMGAmount;
            DMGDealtMult = dMGDealtMult;
            DMGTakenMult = dMGTakenMult;
            StrengthAwakened = strengthAwakened;
            EnduranceAwakened = enduranceAwakened;
            AgilityAwakened = agilityAwakened;
            ManaAwakened = manaAwakened;
            Heal = heal;
            DMGMatter ParsedEnum = Enum.Parse<DMGMatter>(newMatter, true);
            DMGMatterSwitch = ParsedEnum;
        }
        public void SelfAwaken(Battle battle)
        {
            if (battle.SelfRevealed) 
            {
                Console.WriteLine($"{battle.SelfName} have awakened their noble phantasm {Name}");
            }
            else
            {
                Console.WriteLine($"{battle.SelfType} have awakened their noble phantasm {Name}");
            }
            
            battle.EnemyCurrentHealth -= DMGAmount * battle.SelfMana;
            battle.SelfDMGDealt += DMGDealtMult;
            battle.EnemyDMGTaken += DMGTakenMult;
            battle.SelfStrength += (int)Math.Round(StrengthAwakened * battle.SelfMana);
            battle.SelfEndurance += (int)Math.Round(EnduranceAwakened * battle.SelfMana);
            battle.SelfCurrentHealth += (int)Math.Round(EnduranceAwakened * battle.SelfMana) * 10;
            battle.SelfAgility += (int)Math.Round(AgilityAwakened * battle.SelfMana);
            battle.SelfMana += (int)Math.Round(ManaAwakened * battle.SelfMana);
            battle.SelfCurrentHealth += (int)(Heal * battle.SelfMana);
            if (battle.SelfCurrentHealth > battle.SelfMaxHealth)
            {
                battle.SelfCurrentHealth = battle.SelfMaxHealth;
            }
            battle.SelfDMGMatter = DMGMatterSwitch;
        }
        public void EnemyAwaken(Battle battle)
        {
            if (battle.EnemyRevealed)
            {
                Console.WriteLine($"{battle.EnemyName} have awakened their noble phantasm {Name}");
            }
            else
            {
                Console.WriteLine($"{battle.EnemyType} have awakened their noble phantasm {Name}");
            }
            battle.SelfCurrentHealth -= DMGAmount * battle.EnemyMana;
            battle.EnemyDMGDealt += DMGDealtMult;
            battle.SelfDMGTaken += DMGTakenMult;
            battle.EnemyStrength += (int)Math.Round(StrengthAwakened * battle.EnemyMana);
            battle.EnemyEndurance += (int)Math.Round(EnduranceAwakened * battle.EnemyMana);
            battle.EnemyCurrentHealth += (int)Math.Round(EnduranceAwakened * battle.EnemyMana) * 10;
            battle.EnemyAgility += (int)Math.Round(AgilityAwakened * battle.EnemyMana);
            battle.EnemyMana += (int)Math.Round(ManaAwakened * battle.EnemyMana);
            battle.EnemyCurrentHealth += (int)(Heal * battle.EnemyMana);
            if (battle.EnemyCurrentHealth > battle.EnemyMaxHealth)
            {
                battle.EnemyCurrentHealth = battle.EnemyMaxHealth;
            }
            battle.EnemyDMGMatter = DMGMatterSwitch;
        }
    }
}
