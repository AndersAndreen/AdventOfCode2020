using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day07
{
    public class Bag
    {
        public string Color { get; private set; } = "";
        private ICollection<(int, string)> _bagReferences = new List<(int, string)>();

        private Bag() { }

        public static Bag Map(string input)
        {
            var parse = input
                .Replace("bags", "")
                .Replace("bag", "")
                .Trim('.')
                .Split("contain", StringSplitOptions.RemoveEmptyEntries)
                .Select(bag => bag.Trim())
                .ToList();
            return new Bag
            {
                Color = parse[0].Trim(),
                _bagReferences = parse[1]
                    .Split(",")
                    .Select(bag => bag.Trim())
                    .Select(bag => Regex.Split(bag, @"(?<=\d+)"))
                    .Where(bag => bag.Length > 1)
                    .Select(bag => (int.TryParse(bag[0], out var result) ? result : 0, bag[1].Trim()))
                    .ToList()
            };
        }

        public IEnumerable<Bag> Match(ICollection<Bag> allBags, string search)
        {
            if (!_bagReferences.Any())
            {
                return new List<Bag>();
            }
            if (_bagReferences.Select(bagReference => bagReference.Item2).Contains(search))
            {
                return new List<Bag>() { allBags.Single(bag => bag.Color == Color) };
            }
            return _bagReferences.SelectMany(bagReference => allBags
                .Single(bag => bag.Color == bagReference.Item2)
                .Match(allBags, search)).Any()
                ? new List<Bag>() { allBags.Single(bag => bag.Color == Color) }
                : new List<Bag>();
        }

        public int CountTotalBags(ICollection<Bag> allBags)
        {
            if (!_bagReferences.Any()) { return 1; }

            return _bagReferences
                .Select(bagReference =>
                    bagReference.Item1 * allBags
                        .Single(bag => bag.Color == bagReference.Item2)
                        .CountTotalBags(allBags)).Sum()+1;
        }
    }
}