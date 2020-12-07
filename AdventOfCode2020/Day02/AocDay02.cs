using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day02
{
    public class AocDay02
    {
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 2");
            // How many passwords are valid according to their policies?
            List<PasswordRow> list = DataLoader.BuildList();
            var numValidPasswords1 = list.Count(row => row.IsValid1);
            Console.WriteLine($"Result part 1: {numValidPasswords1}");

            var numValidPasswords2 = list.Count(row => row.IsValid2);
            Console.WriteLine($"Result part 2: {numValidPasswords2}");
        }
    }

    public class PasswordRow
    {
        public int Min { get; }
        public int Max { get; }
        public char Letter { get; }
        public string Password { get; }
        public bool IsValid1
        {
            get
            {
                var charCount = Password.Count(character => character == Letter);
                return charCount >= Min && charCount <= Max;
            }
        }

        public bool IsValid2 => Password[Min - 1] == Letter ^ Password[Max - 1] == Letter;

        public PasswordRow(int min, int max, char letter, string password)
        {
            Min = min;
            Max = max;
            Letter = letter;
            Password = password;
        }
    }

}
