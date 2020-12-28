using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day15
{
    class AocDay15
    {
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 15");

            var input = new[] { 6, 13, 1, 15, 2, 0 };
            var resultStep1 = SolvePart1(input);
            Console.WriteLine($"Result part 1: {resultStep1}");

            var resultStep2 = SolvePart2(input);
            Console.WriteLine($"Result part 2: {resultStep2}");
        }

        public static long SolvePart1(int[] input)
        {
            var listBuilder = input.Select((nr, index) => (nr, index)).ToList();
            var stop = 2020;
            for (var i = listBuilder.Count; i < stop; i++)
            {
                var last = listBuilder.Last();
                var xx = listBuilder
                    .Where(nr => nr.nr == last.nr);
                var yy = xx.Count() == 1
                    ? new[] { 0, 0 }
                    : xx.TakeLast(2).Select(nr => nr.index).ToArray();
                listBuilder.Add((yy[1] - yy[0], last.index + 1));
            }
            return listBuilder.Last().nr;
        }

        public static long SolvePart2(int[] input)
        {
            var listBuilder = input
                .Select((nr, index) => (nr, index))
                .ToDictionary(nr => nr.nr, nr => (big: nr.index, small: -1));
            var stop = 30000000;
            var last = listBuilder.Last();
            for (var i = listBuilder.Count; i < stop; i++)
            {
                if (last.Value.small == -1)
                {
                    listBuilder[0] = (big: i, small: listBuilder[0].big);
                    last = new KeyValuePair<int, (int, int)>(0, listBuilder[0]);
                }
                else
                {
                    var toUpdate = last.Value.big - last.Value.small;
                    var keyExists = listBuilder.ContainsKey(toUpdate);
                    listBuilder[toUpdate] = (i, keyExists ? listBuilder[toUpdate].big : -1);
                    last = new KeyValuePair<int, (int, int)>(toUpdate, listBuilder[toUpdate]);
                }
            }
            return last.Key;
        }
    }
}
