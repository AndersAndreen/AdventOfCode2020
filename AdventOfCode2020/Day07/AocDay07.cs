using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day07
{
    public static class AocDay07
    {
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 7");
            // How many bag colors can eventually contain at least one shiny gold bag? 
            var bagRules = File
                .ReadAllText(@"Day07\AocDay07.txt")
                .Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(BagRule.Map)
                .ToList();
            var search = "shiny gold";
            var x = bagRules.Where(rule =>
                rule.Content.Any(item => item.Item2==search));
            var resultStep1 = bagRules.Count();
            Console.WriteLine($"Result part 1: {resultStep1}");

            var resultStep2 = 2;
            Console.WriteLine($"Result part 2: {resultStep2}");
        }
    }
}
