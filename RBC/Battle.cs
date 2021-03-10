using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RBC
{
    public class Battle
    {
        private readonly Player Player;
        private readonly double WinExp;
        private readonly double LoseExp;
        
        private int SelfNPPoint;        
        public readonly string SelfName;
        public readonly string[] SelfAliases;
        public readonly Type SelfType;
        public bool SelfRevealed;
        public readonly string SelfHint;
        public readonly NP SelfNP;
        public double SelfDMGDealt;
        public double SelfDMGTaken;
        public int SelfStrength;
        public int SelfEndurance;
        public int SelfAgility;
        public int SelfMana;
        public int SelfMaxHealth =>  SelfEndurance * 12;
        public int SelfCurrentHealth;
        public DMGMatter SelfDMGMatter;
        public double SelfCriticalChance => 5 + 0.1 * ((SelfAgility - EnemyAgility) / 5);
        
        private int? EnemyNPPoint;
        public readonly string EnemyName;
        public readonly string[] EnemyAliases;
        public readonly Type EnemyType;
        public bool EnemyRevealed;
        public readonly string EnemyHint;
        public readonly NP EnemyNP;
        public double EnemyDMGDealt;
        public double EnemyDMGTaken;
        public int EnemyStrength;
        public int EnemyEndurance;
        public int EnemyAgility;
        public int EnemyMana;
        public int EnemyMaxHealth => EnemyEndurance * 12;
        public int EnemyCurrentHealth;
        public DMGMatter EnemyDMGMatter;
        public double EnemyCriticalChance => 5 + 0.1 * ((EnemyAgility - SelfAgility) / 5);

        public Battle(Player player, int enemyLevel, bool isServant = false)
        {
            Player = player;
            WinExp = enemyLevel * 25;
            LoseExp = enemyLevel * 5;

            SelfName = player.UsingChar.Name;
            SelfAliases = player.UsingChar.Aliases;
            SelfType = player.UsingChar.CharType;
            SelfHint = player.UsingChar.Hint;
            SelfNP = player.UsingChar.NoblePhantasm;
            SelfNPPoint = 0;
            SelfDMGDealt = 1;
            SelfDMGTaken = 1;
            SelfStrength = player.PlayerStat.Strength + player.UsingChar.CharacterStat.Strength;
            SelfEndurance = player.PlayerStat.Endurance + player.UsingChar.CharacterStat.Endurance;
            SelfAgility = player.PlayerStat.Agility + player.UsingChar.CharacterStat.Agility;
            SelfMana = player.PlayerStat.Mana + player.UsingChar.CharacterStat.Mana;
            SelfCurrentHealth = SelfMaxHealth - (player.UsingChar.Health - player.UsingChar.CurrentHealth);

            if (isServant)
            {
                Character enemy = CharList.Randomise();
                EnemyName = enemy.Name;
                EnemyAliases = enemy.Aliases;
                EnemyType = enemy.CharType;
                EnemyHint = enemy.Hint;
                EnemyNP = enemy.NoblePhantasm;
                EnemyNPPoint = 0;
                EnemyDMGDealt = 1;
                EnemyDMGTaken = 1;
                EnemyStrength = enemy.CharacterStat.Strength + enemyLevel * 2;
                EnemyEndurance = enemy.CharacterStat.Endurance + enemyLevel * 2;
                EnemyAgility = enemy.CharacterStat.Agility + enemyLevel * 2;
                EnemyMana = enemy.CharacterStat.Mana + enemyLevel * 2;
                EnemyCurrentHealth = EnemyMaxHealth;
            }
        }

        public void Start()
        {
            while (SelfCurrentHealth > 0 && EnemyCurrentHealth > 0)
            {
                bool hintTaken = false;
                bool playerStarts = SelfAgility > EnemyAgility;
                if (playerStarts)
                {
                    bool choosingMove = true;
                    while (choosingMove)
                    {

                        Thread.Sleep(3500);
                        Console.Clear();
                        if (hintTaken && !EnemyRevealed)
                        {
                            Console.WriteLine(EnemyHint + "\n");
                        }
                        Console.WriteLine($"Player - {SelfCurrentHealth}/{SelfMaxHealth}   ||   Enemy - {EnemyCurrentHealth}/{EnemyMaxHealth}");
                        Console.WriteLine("Choose number:\n\n1. Attack\n\n2. Noble Phantasm\n\n3. hint (wont take away turn)\n\n4. Guess name\n\n");
                        string choice = Console.ReadLine();
                        if (choice == "1")
                        {
                            Attack();
                            choosingMove = false;
                        }
                        else if (choice == "2")
                        {
                            if (SelfNPPoint == 6)
                            {
                                SelfNP.SelfAwaken(this);
                                choosingMove = false;
                                SelfNPPoint = 0;
                            }
                            else
                            {
                                Console.WriteLine($"Servant have not enough NP point to awaken noble phantasm (current -> {SelfNPPoint} || needed -> 6)");
                            }
                        }
                        else if (choice == "3")
                        {
                            hintTaken = true;
                        }
                        else if (choice == "4")
                        {
                            if (!EnemyRevealed)
                            {
                                Console.Clear();
                                Console.Write("Whats enemy name? ->   "); string answer = Console.ReadLine().Trim().ToLower();
                                if (NameCheck(answer))
                                {
                                    Console.WriteLine($"Correct, enemy name is {answer}, they have been weakened");
                                    EnemyStrength = (int)Math.Round(EnemyStrength * 0.5);
                                    EnemyMana = (int)Math.Round(EnemyMana * 0.6);
                                    EnemyRevealed = true;
                                    choosingMove = false;
                                }
                                else
                                {
                                    Console.WriteLine($"{answer} is not right name.");
                                    choosingMove = false;
                                }
                            }
                            else
                            {
                                Console.WriteLine("You already know enemy name.");
                            }
                        }
                    }
                    if (EnemyCurrentHealth > 0)
                    {
                        Thread.Sleep(3500);
                        Console.Clear();
                        if (EnemyNPPoint == 6)
                        {
                            EnemyNP.EnemyAwaken(this);
                            EnemyNPPoint = 0;
                        }
                        else
                        {
                            GetAttacked();
                        }
                    }

                }
                else
                {
                    Thread.Sleep(3500);
                    Console.Clear();
                    if (EnemyNPPoint == 6)
                    {
                        EnemyNP.EnemyAwaken(this);
                        EnemyNPPoint = 0;
                    }
                    else
                    {
                        GetAttacked();
                    }
                    bool choosingMove = true;
                    if (SelfCurrentHealth > 0)
                    {
                        while (choosingMove)
                        {
                            Thread.Sleep(3500);
                            Console.Clear();
                            if (hintTaken && !EnemyRevealed)
                            {
                                Console.WriteLine(EnemyHint + "\n");
                            }
                            Console.WriteLine($"Player - {SelfCurrentHealth}/{SelfMaxHealth}   ||   Enemy - {EnemyCurrentHealth}/{EnemyMaxHealth}");
                            Console.WriteLine("Choose number:\n\n1. Attack\n\n2. Noble Phantasm\n\n3. hint (wont take away turn)\n\n4. Guess name\n\n");
                            string choice = Console.ReadLine();
                            if (choice == "1")
                            {
                                Attack();
                                choosingMove = false;
                            }
                            else if (choice == "2")
                            {
                                if (SelfNPPoint == 6)
                                {
                                    SelfNP.SelfAwaken(this);
                                    choosingMove = false;
                                    SelfNPPoint = 0;
                                }
                                else
                                {
                                    Console.WriteLine($"Servant have not enough NP point to awaken noble phantasm (current -> {SelfNPPoint} || needed -> 6)");
                                }
                            }
                            else if (choice == "3")
                            {
                                hintTaken = true;
                            }
                            else if (choice == "4")
                            {
                                if (!EnemyRevealed)
                                {
                                    Console.Clear();
                                    Console.Write("Whats enemy name? ->   "); string answer = Console.ReadLine().Trim().ToLower();
                                    if (NameCheck(answer))
                                    {
                                        Console.WriteLine($"Correct, enemy name is {answer}, they have been weakened");
                                        EnemyStrength = (int)Math.Round(EnemyStrength * 0.5);
                                        EnemyMana = (int)Math.Round(EnemyMana * 0.6);
                                        EnemyRevealed = true;
                                        choosingMove = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{answer} is not right name.");
                                        choosingMove = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("You already know enemy name.");
                                }
                            }
                            else { Console.WriteLine("Invalid choice."); }
                        }
                    }


                }
                if (SelfNPPoint != 6)
                {
                    SelfNPPoint++;
                }
                EnemyNPPoint++;
            }
            if (SelfCurrentHealth <= 0)
            {
                Console.WriteLine($"You have lost, gaining {LoseExp} exp for master as reward");
                Player.RemoveUsingChar();
                Player.GetExp(LoseExp);
            }
            else
            {
                Console.WriteLine($"You have won, gaining {WinExp} exp for master and servant as reward");
                Player.UsingChar.GainExp(WinExp);
                Player.GetExp(WinExp);
            }

        }

        private void Attack()
        {
            Random rnd = new Random();
            bool isCritical = rnd.Next(1, 1001) < SelfCriticalChance * 10;
            int dmgDealt = 0;
            if (isCritical)
            {
                if (SelfDMGMatter == DMGMatter.Strength)
                {
                    dmgDealt = (int)Math.Round(SelfStrength / EnemyDMGTaken * SelfDMGDealt * 1.5);
                    EnemyCurrentHealth -= dmgDealt;
                }
                else
                {
                    dmgDealt = (int)Math.Round(SelfMana / EnemyDMGTaken * SelfDMGDealt * 1.5);
                    EnemyCurrentHealth -= dmgDealt;
                }
            }
            else
            {
                if (SelfDMGMatter == DMGMatter.Strength)
                {
                    dmgDealt = (int)Math.Round(SelfStrength / EnemyDMGTaken * SelfDMGDealt);
                    EnemyCurrentHealth -= dmgDealt;
                }
                else
                {
                    dmgDealt = (int)Math.Round(SelfMana / EnemyDMGTaken * SelfDMGDealt);
                    EnemyCurrentHealth -= dmgDealt;
                }

            }
            if (SelfRevealed)
            {
                if (EnemyRevealed)
                {
                    Console.WriteLine($"{SelfName} dealt {dmgDealt} damage to enemy {EnemyName}");
                }
                else
                {
                    Console.WriteLine($"{SelfName} dealt {dmgDealt} damage to enemy {EnemyType}");
                }
            }
            else
            {
                if (EnemyRevealed)
                {
                    Console.WriteLine($"{SelfType} dealt {dmgDealt} damage to enemy {EnemyName}");
                }
                else
                {
                    Console.WriteLine($"{SelfType} dealt {dmgDealt} damage to enemy {EnemyType}");
                }
            }
        }
        private void GetAttacked()
        {
            Random rnd = new Random();
            bool isCritical = rnd.Next(1, 1001) < EnemyCriticalChance * 10;
            int dmgDealt = 0;
            if (isCritical)
            {
                if (EnemyDMGMatter == DMGMatter.Strength)
                {
                    dmgDealt = (int)Math.Round(EnemyStrength / SelfDMGTaken * EnemyDMGDealt * 1.5);
                    SelfCurrentHealth -= dmgDealt;
                }
                else
                {
                    dmgDealt = (int)Math.Round(EnemyMana / SelfDMGTaken * EnemyDMGDealt * 1.5);
                    SelfCurrentHealth -= dmgDealt;
                }
            }
            else
            {
                if (EnemyDMGMatter == DMGMatter.Strength)
                {
                    dmgDealt = (int)Math.Round(EnemyStrength / SelfDMGTaken * EnemyDMGDealt);
                    SelfCurrentHealth -= dmgDealt;
                }
                else
                {
                    dmgDealt = (int)Math.Round(EnemyMana / SelfDMGTaken * EnemyDMGDealt);
                    SelfCurrentHealth -= dmgDealt;
                }
            }
            if (EnemyRevealed)
            {
                if (SelfRevealed)
                {
                    Console.WriteLine($"Enemy {EnemyName} dealt {dmgDealt} damage to {SelfName}");
                }
                else
                {
                    Console.WriteLine($"Enemy {EnemyName} dealt {dmgDealt} damage to {SelfType}");
                }
            }
            else
            {
                if (EnemyRevealed)
                {
                    Console.WriteLine($"Enemy {EnemyType} dealt {dmgDealt} damage to {SelfName}");
                }
                else
                {
                    Console.WriteLine($"Enemy {EnemyType} dealt {dmgDealt} damage to {SelfType}");
                }
            }
        }
        private bool NameCheck(string target)
        {
            foreach (string alias in EnemyAliases)
            {
                if (alias == target)
                {
                    return true;
                }
            }
            return false;  
        }
    }
}
