using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day03
{
    class AocDay03V2
    {
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 3");

            var treePattern = File.ReadAllLines(@"Day03\AocDay03.txt");
            Console.WriteLine($"Result part 1: {CountTreesV2((3, 1), treePattern)}");
            var part2 = new (int Right, int Down)[]
                {
                    (1,1),
                    (3,1),
                    (5,1),
                    (7,1),
                    (1,2)
                }
                .Select(slope => CountTreesV2(slope, treePattern))
                .Aggregate((product, treeCount) => product * treeCount);
            Console.WriteLine($"Result part 2: {part2}");
        }

        private static long CountTreesV2((int Right, int Down) slope, string[] treePattern) =>
            (long)treePattern.Select((row, rowNr) => (row, rowNr)) // indexera varje kartrad
                .Where((row, rowNr) => rowNr % slope.Down == 0) // ta bort kartrader som ska hoppas över
                .Select((row, idx) => row.row[idx * slope.Right % treePattern[0].Length]) // välj kartposition
                .Count(pos => pos == '#'); // Ta bara med de positioner som har ett träd
    }
}
