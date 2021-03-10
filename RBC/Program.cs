using System;
using System.Collections.Generic;
using System.Threading;

namespace RBC
{
    class Program
    {
        static void Main(string[] args)
        {
            Create();
            Player user = new Player("");
            bool choosingName = true;
            bool playing = true;
            Battle battle;
            while (choosingName)
            {
                Console.Write("Write your username ->  ");
                string possiblyName = Console.ReadLine();
                Console.WriteLine($"\nAre you sure you want \"{possiblyName}\" as your username? (write \"yes\" or \'y\' to accept)");
                string answer = Console.ReadLine().Trim().ToLower();
                if (answer == "yes" || answer == "y")
                {
                    Console.Clear();
                    user = new Player(possiblyName);
                    Console.WriteLine($"Your name from now is {possiblyName}!");
                    choosingName = false;
                }
                else
                {
                    Console.Clear();
                }
            }
            while (playing)
            {
                Thread.Sleep(3000);
                Console.Clear();
                Console.WriteLine($"1. Summon Servant\n\n2. Heal\n\n3. Battle\n\n4. Add stat\n\n5. Profile\n\n6. Select new servant\n\n7. Servant list \n\n8. Quit\n\n");
                string choice = Console.ReadLine().Trim();
                if (choice == "1")
                {
                    user.Summon();
                }
                else if(choice == "2")
                {
                    user.Heal();
                }
                else if (choice == "3")
                {
                    if (user.UsingChar == null)
                    {
                        Console.WriteLine("You have not servant to fight.");
                    }
                    else
                    {
                        bool takingLevel = true;
                        while (takingLevel)
                        {
                            Console.Clear();
                            Console.WriteLine("What level enemy should be?");
                            bool wasParsed = int.TryParse(Console.ReadLine(), out int result);
                            if (wasParsed && result > 0)
                            {
                                battle = new Battle(user, result, true);
                                battle.Start();
                                takingLevel = false;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for enemy level.");
                                Thread.Sleep(1000);
                            }
                        }
                        
                    }
                }
                else if (choice == "4")
                {
                    Console.Clear();
                    Console.WriteLine("1. Add master stat\n\n2. Add servant stat");
                    string decision = Console.ReadLine();
                    if (decision == "1")
                    {
                        StatAddInteract(user, true);
                    }
                    else if (decision == "2")
                    {
                        StatAddInteract(user, false);
                    }

                }
                else if (choice == "5")
                {
                    user.Profile();
                }
                else if (choice == "6")
                {
                    if (user.ServantsOwned > 1)
                    {
                        user.SelectServant();
                    }
                    else
                    {
                        Console.WriteLine($"You have {user.ServantsOwned} servant owned only. (selecting different servant is impossible)");
                    }   
                }
                else if (choice == "7")
                {
                    if (user.ServantsOwned > 0)
                    {
                        user.Servants();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("You dont own any servant");
                    }
                }
                else if (choice == "8")
                {
                    if (user.ServantsOwned > 0)
                    {
                        user.ServantInfo();
                    }
                    else
                    {
                        Console.WriteLine("You dont own any servant");
                    }
                }
                else if(choice == "9")
                {
                    playing = false;
                }
                else
                {
                    Console.WriteLine("Invalid input, try again.");
                }
            }
        }

        private static void Create()
        {
            NP enumaElish = new NP("Enuma Elish", 5, 0.1, 0, 0, 0, 0, 0, 0, "strength");
            string[] gilgameshAliases = { "gilgamesh", "humanity's oldest king", "king of heroes of decadence", "king of heroes", "linchpin of heaven", "king of uruk", "king of babylonia", "the golden king", "gil", "goldie", "wedge of heaven" };
            string gilgameshHint = "He ruled the Sumerian city-state of Uruk, the capital city of ancient Mesopotamia in the B.C. era. He was an ultimate, transcendent being so divine as to be two-thirds god and one-third human, and no others in the world could match him.";
            Character gilgamesh = new Character("Gilgamesh", gilgameshAliases, "archer", enumaElish, gilgameshHint, 40, 30, 30, 50, "strength");
            CharList.Append(gilgamesh);

            NP vasaviShakti = new NP("Vasavi Shakti", 9, 0, 0.4, 0, 0, 0, 0, 0, "strength");
            string[] karnaAliases = { "karna", "lancer of red", "son of the sun god", "hero of charity", "launcher" };
            string karnaHint = "The invulnerable hero of the Indian epic Mahabharata, as a hero on the vanquished side. The central conflict of The Mahabharata is the war over influence between the Pandava royal family and Kaurava royal family. Lancer became famous as the rival of Arjuna, the great hero of Hindu mythology.";
            Character karna = new Character("Karna", karnaAliases, "lancer", vasaviShakti, karnaHint, 40, 30, 50, 50, "strength");
            CharList.Append(karna);

            NP blueEyesWhiteDragon = new NP("Blue-Eyes White Dragon", 0, 0, 0, 0, 1.5, 0, 0.5, 0.3, "mana");
            string[] kaibaAliases = {"seto kaiba", "kaiba", "kaiba seto", "kaiba - boy" };
            string kaibanHint = "An intellectually gifted and innovative computer programmer, engineer, and inventor during his youth, he invented virtual software for playing video games as a young child prodigy.";
            Character setoKaiba = new Character("Seto Kaiba", kaibaAliases, "caster", blueEyesWhiteDragon, kaibanHint, 15, 20, 25, 40, "strength");
            CharList.Append(setoKaiba);
        }
        
        private static void StatAddInteract(Player player, bool isMaster)
        {
            bool choosingStatType = true;
            bool choosingAmount = true;
            string result = "";
            int amount = 0;
            while (choosingStatType)
            {
                Console.Clear();
                Console.WriteLine("Which stat would u like to rise? (stats: strength/str, endurance/end, agility/agi and mana)");
                result = Console.ReadLine().Trim().ToLower();
                Console.Clear();
                Console.WriteLine($"Are you sure u want to rise \"{result}\" stat? (y to accept. make sure you write correct stat)");
                string answer = Console.ReadLine().Trim().ToLower();
                if (answer == "y")
                {
                    choosingStatType = false;
                }
            }
            while (choosingAmount)
            {
                Console.Clear();
                Console.WriteLine("How much point would u like to add?\n\n");
                bool parsed = int.TryParse(Console.ReadLine().Trim(), out amount);
                if (parsed)
                {
                    choosingAmount = false;
                }
                else
                {
                    Console.WriteLine("Invalid amount, try again.");
                    Thread.Sleep(2000);
                }
            }
            if (isMaster)
            {
                player.MasterAdd(result, amount);
            }
            else
            {
                player.ServantAdd(result, amount);
            }
            
        }
    }
}
