using System;
using System.Collections.Generic;
using System.Threading;

namespace RBC
{
    public class Player
    {
        public string Name { get; set; }
        public int Level { get; private set; }
        public double CurrentExp { get; private set; }
        public double LevelUpReq => Level * Level + Level * 10;
        public int Gold { get; private set; }
        private List<Character> _chars;
        public int ServantsOwned => _chars.Count;
        public int MaxServant => 1 + Level / 5;
        public Character UsingChar => _chars[0];
        public Stats PlayerStat;
        public int AvStatPoints => Level * 8 - (PlayerStat.Strength + PlayerStat.Endurance + PlayerStat.Agility + PlayerStat.Mana + Level * 4);

        public Player(string name)
        {
            PlayerStat = new Stats();
            Name = name;
            Level = 1;
            _chars = new List<Character>();
            new Stats();
        }

        public void GetExp(double amount)
        {
            CurrentExp += amount;
            bool levelUp = CurrentExp > LevelUpReq;
            if (levelUp)
            {
                CurrentExp -= LevelUpReq;
                Level++;
                PlayerStat.LvlUp();
                Console.Clear();
                Console.WriteLine($"You have leveled up to {Level}.");
                Thread.Sleep(1500);
                
            }
        }

        public void Summon()
        {
            bool capableToSummon = MaxServant > _chars.Count;
            if (capableToSummon)
            {
                Character summoned = CharList.Randomise();
                _chars.Add(summoned);
                Console.WriteLine($"Successfully summoned {summoned.CharType} - {summoned.Name} on slot {_chars.IndexOf(summoned) + 1}.");
            }
            else
            {
                Console.WriteLine("Summon failed. (not enough slots)");
            }
        }

        public void Heal()
        {
            UsingChar.CurrentHealth = UsingChar.Health;
            Console.WriteLine($"{UsingChar.Name} is fully healed.");
        }

        public void Servants()
        {
            ServantList();

            Console.WriteLine("Click anything to return.");
            Console.ReadKey();
        }

        public void SelectServant()
        {
            bool takingNum = true;
            int index = 0;
            while (takingNum)
            ServantList();
            Console.WriteLine("Whats index of servant you wish to select?\n\n");
            bool parsed = int.TryParse(Console.ReadLine().Trim(), out index);
            if (parsed)
            {
                Console.WriteLine($"Are you sure {index} is index of servant you want to select? (y to select)\n");
                string answer = Console.ReadLine().Trim().ToLower();
                if (answer == "y")
                {
                    Select(index);
                }
            }
        }
        private void ServantList()
        {
            int i = 1;
            Console.Clear();
            foreach (Character character in _chars)
            {
                Console.WriteLine($"{i}. {character.Name} level {character.Level}\n\n");
                i++;
            }
        }
        private void Select(int index)
        {
            if (index < _chars.Count && index > 1)
            {
                Character currentFirst = UsingChar;
                _chars[0] = _chars[index - 1];
                _chars[index - 1] = currentFirst;
            }
            else
            {
                Console.WriteLine("Invalid index");
            }
        }

        public void MasterAdd(string stat, int amount)
        {
            bool enoughPTS = AvStatPoints >= amount;
            if (enoughPTS)
            {
                bool parsed = Enum.TryParse<Stat>(stat, true, out Stat parsedEnum);
                if (parsed)
                {
                    PlayerStat.StatUp(parsedEnum, amount, true);
                }
                else
                {
                    Console.WriteLine("Invalid argument.\nstat must be: strength/str, endurance/end, agility/agi or mana");
                }
            }
            else
            {
                Console.WriteLine($"you have {AvStatPoints} available stat point only.");
            }
        }

        public void ServantAdd(string stat, int amount)  
        {
            bool enoughPTS = UsingChar.AvStatPoints >= amount;
            if (enoughPTS)
            {
                bool parsed = Enum.TryParse<Stat>(stat, true, out Stat parsedEnum);
                if (parsed)
                {
                    UsingChar.CharacterStat.StatUp(parsedEnum, amount, false);
                }
                else
                {
                    Console.WriteLine("Invalid argument.\nstat must be: strength/str, endurance/end, agility/agi or mana");
                }
            }
            else
            {
                Console.WriteLine($"you have {UsingChar.AvStatPoints} available stat point only.");
            }
        }

        public void RemoveUsingChar()
        {
            _chars.Remove(UsingChar);
        }
        
        public void Profile()
        {
            Console.Clear();
            Console.WriteLine($"Name: {Name}.\n\nLevel: {Level}.\n\nExp: {CurrentExp}/{LevelUpReq}\n\nStrength: {PlayerStat.Strength}.\n\nEndurance: {PlayerStat.Endurance}.\n\nAgility: {PlayerStat.Agility}.\n\nMana: {PlayerStat.Mana}.\n\nStat points: {AvStatPoints}.\n\nServant capacity: {MaxServant}\n\nServant owned: {_chars.Count}\n\nClick anything yo leave.");
            Console.ReadKey();
        }

        public void ServantInfo()
        {
            Console.Clear();
            Console.WriteLine($"Name: {UsingChar.Name}\n\nAliases:");
            foreach(string alias in UsingChar.Aliases)
            {
                Console.WriteLine(alias + ".");
            }
            Console.WriteLine($"\n\nLevel: {UsingChar.Level}.\n\nExp: {UsingChar.CurrentExp}/{UsingChar.LevelUpReq}\n\nStats:\n\nStrength - {UsingChar.CharacterStat.Strength}\nEndurance - {UsingChar.CharacterStat.Endurance}\nAgility - {UsingChar.CharacterStat.Agility}\nMana - {UsingChar.CharacterStat.Mana}\n\nNoble phantasm: {UsingChar.NoblePhantasm.Name}\n\nClick anything to return");
            Console.ReadKey();
        }
    }
}