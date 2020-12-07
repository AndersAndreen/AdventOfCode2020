using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day07
{
    public class BagRule
    {
        public string Color { get; set; } = "";
        public ICollection<(int, string)> Content { get; set; } = new List<(int, string)>();

        public static BagRule Map(string input)
        {
            var parse = input
                .Replace("bags", "")
                .Replace("bag", "")
                .Trim('.')
                .Split("contain", StringSplitOptions.RemoveEmptyEntries)
                .Select(bag => bag.Trim())
                .ToList();
            return new BagRule
            {
                Color = parse[0].Trim(),
                Content = parse[1]
                    .Split(",")
                    .Select(bag => bag.Trim())
                    .Select(bag => Regex.Split(bag, @"(?<=\d+)"))
                    .Where(bag => bag.Length > 1)
                    .Select(bag => (int.TryParse(bag[0], out var result) ? result : 0, bag[1].Trim()))
                    .ToList()
            };
        }

        public BagRule Find(IEnumerable<string> items, string search)
        {
            if (Content.Any(bag => bag.Item2 == search))
            {

            }

        }


    }
}