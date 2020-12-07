using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day04
{
    public static class AocDay04
    {
        public static string[] EyeColors = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
        public static string[] RequiredKeys = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 4");
            var puzzleInput = File.ReadAllText(@"Day04\AocDay04.txt")
                .Replace("\r", "")
                .Split("\n\n")
                .Select(passport => Regex.Split(passport, "[\n ]", RegexOptions.None)
                    .Select(field => field.Split(':'))
                    .Where(field => field.Length == 2)
                    .Select(field => new KeyValuePair<string, string>(field[0], field[1]))
                    .ToDictionary(x => x.Key, x => x.Value)
                    )
                .ToList();
            var validPassportsStep1 = puzzleInput
                .Where(passport => RequiredKeys
                    .All(requiredKey => passport
                        .Any(field => field.Key.Contains(requiredKey))))
                .ToList();
            Console.WriteLine($"Result part 1: {validPassportsStep1.Count()}");

            var validPassportsStep2 = validPassportsStep1
                .Where(passport =>
                    ValidateBirthYear(passport["byr"])
                    && ValidateIssueYear(passport["iyr"])
                    && ValidateExpirationYear(passport["eyr"])
                    && ValidateHeight(passport["hgt"])
                    && ValidateHairColor(passport["hcl"])
                    && ValidateEyeColor(passport["ecl"])
                    && ValidatePassportId(passport["pid"])
                    )
                .ToList();
            Console.WriteLine($"Result part 2: {validPassportsStep2.Count()}");
        }

        public static bool ValidateBirthYear(string value) => IsWithinRange(int.Parse(value), 1920, 2002);
        public static bool ValidateIssueYear(string value) => IsWithinRange(int.Parse(value), 2010, 2020);
        public static bool ValidateExpirationYear(string value) => IsWithinRange(int.Parse(value), 2020, 2030);
        public static bool ValidateHeight(string value) =>
            Regex.IsMatch(value, @"^\d+(cm|in)$", RegexOptions.IgnoreCase)
            && (value[^2..] == "cm"
                ? IsWithinRange(int.Parse(value[..^2]), 150, 193)
                : IsWithinRange(int.Parse(value[..^2]), 59, 76));
        public static bool ValidateHairColor(string value) => Regex.IsMatch(value, @"^#[1234567890abcdef]{6}$", RegexOptions.IgnoreCase);
        public static bool ValidateEyeColor(string value) => EyeColors.Contains(value);
        public static bool ValidatePassportId(string value) => Regex.IsMatch(value, @"^\d{9}$");
        public static bool IsWithinRange(int value, int min, int max) => value >= min && value <= max;
    }
}
