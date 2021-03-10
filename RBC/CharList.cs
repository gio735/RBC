using System;
using System.Collections.Generic;
using System.Text;

namespace RBC
{
    public static class CharList
    {
        public static List<Character> CharacterList { get; private set; }

        static CharList()
        {
            CharacterList = new List<Character>();
        }

        public static void Append(Character character)
        {
            CharacterList.Add(character);
        }

        public static void List()
        {
            int counter = 1;
            foreach (Character character in CharacterList)
            {
                Console.WriteLine($"{counter}) {character.Name}.\n");
                counter++;
            }
        }

        public static Character Randomise()
        {
            int amount = CharacterList.Count;
            Random rnd = new Random();
            int index = rnd.Next(0, amount);
            return CharacterList[index];
        }
    }
}
