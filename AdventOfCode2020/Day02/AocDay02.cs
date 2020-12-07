using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC_2020_02
{
    class AocDay02
    {
        public static void Run()
        {
            // How many passwords are valid according to their policies?
            List<PasswordRow> list = DataLoader.BuildList();
            var numValidPasswords1 = list.Count(row => row.IsValid1);
            Console.WriteLine($"Part 1 number of valid passwords: {numValidPasswords1}");

            var numValidPasswords2 = list.Count(row => row.IsValid2);
            Console.WriteLine($"Part 2 number of valid passwords: {numValidPasswords2}");
        }
    }

    class PasswordRow
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
