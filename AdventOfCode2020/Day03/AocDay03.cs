using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day03
{
    class AocDay03
    {
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 3");

            var treePattern = File.ReadAllLines(@"Day03\AocDay03.txt");
            Console.WriteLine($"Result part 1: {CountTrees((3, 1), treePattern)}");
            var part2 = new (int Right, int Down)[]
                {
                    (1,1),
                    (3,1),
                    (5,1),
                    (7,1),
                    (1,2)
                }
                .Select(slope => CountTrees(slope, treePattern))
                .Aggregate((product, treeCount) => product * treeCount);
            Console.WriteLine($"Result part 2: {part2}");
        }

        private static long CountTrees((int Right, int Down) slope, string[] treePattern) =>
            (long)Enumerable.Range(0, treePattern.Length)
                .Where(row => row % slope.Down == 0)
                .Select((row, idx) => treePattern[row][idx * slope.Right % treePattern[0].Length])
                .Count(pos => pos == '#');
    }
}
