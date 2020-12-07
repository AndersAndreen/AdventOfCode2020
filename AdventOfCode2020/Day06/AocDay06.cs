using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day06
{
    public static class AocDay06
    {
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 6");
            var answers = File.ReadAllText(@"Day06\AocDay06.txt")
                .Replace("\r", "")
                .Split("\n\n")
                .Select(group => group.Split("\n", StringSplitOptions.RemoveEmptyEntries)
                    .Select(person => person.ToLower()
                        .ToList()))
                .ToList();

            var resultStep1 = answers
                    .Select(group => group
                        .SelectMany(person => person))
                    .Select(allGroupAnswers => allGroupAnswers.ToHashSet())
                .Sum(groupAnswers => groupAnswers.Count());
            Console.WriteLine($"Result part 1: {resultStep1}");

            var resultStep2V1 = answers
                .Select(group => GetCommonAnswersForGroup2(group.ToList()).Count());
            Console.WriteLine($"Result part 2 (V1): {resultStep2V1.Sum()}");

            var resultStep2V2 = answers
                .Select(group => GetCommonAnswersForGroup2(group.ToList()).Count());
            Console.WriteLine($"Result part 2 (V2): {resultStep2V2.Sum()}");
        }

        private static IEnumerable<char> GetCommonAnswersForGroup1(ICollection<IEnumerable<char>> group)
        {
            return group
                .SelectMany((personAnswers, ix1) => personAnswers
                    .Where(answer => group
                        .All(memberAnswers => memberAnswers.Contains(answer))))
                .ToHashSet();
        }

        private static IEnumerable<char> GetCommonAnswersForGroup2(IReadOnlyCollection<IEnumerable<char>> group)
        {
            var x = group
                .SelectMany(personAnswers => personAnswers.ToHashSet())
                .GroupBy(answer => answer)
                .Select(answer => (answer.Key, Count: answer.Count()))
                .Where(answer => answer.Count == group.Count())
                .Select(answer => answer.Key);
            ;
            return x;
        }
    }
}
