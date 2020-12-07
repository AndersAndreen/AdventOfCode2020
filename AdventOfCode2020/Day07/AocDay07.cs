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
            var bagRules = File
                .ReadAllText(@"Day07\AocDay07.txt")
                .Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(Bag.Map)
                .ToList();
            var searchTerm = "shiny gold";

            // https://adventofcode.com/2020/day/7#part1
            //1: How many bag colors can eventually contain at least one shiny gold bag? 
            var resultStep1 = bagRules
                .SelectMany(rule=>rule.Match(bagRules, searchTerm))
                .Select(bag=>bag.Color)
                .ToList();
            Console.WriteLine($"Result part 1: {resultStep1.Count()}");

            //https://adventofcode.com/2020/day/7#part2
            //2: How many individual bags are required inside your single shiny gold bag?
            var resultStep2 = bagRules.Single(bagRule=>bagRule.Color == searchTerm).CountTotalBags(bagRules);
            Console.WriteLine($"Result part 2: {resultStep2-1}"); // -1 för att den yttersta bagen inte ska räknas
        }
    }
}
