using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day17
{
    class AocDay17
    {
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 17");
            var input = File
                .ReadAllLines(@"Day17\AocDay17.txt")
                .Reverse()
                .SelectMany((row, y) => row
                    .Select((cellState, x) => (cellState, x, y))
                    .Where(cell => cell.cellState == '#'))
                .ToList();

            var input1 = input
                .Select(cell => (cell.x, cell.y, z: 0))
                .ToHashSet();
            var result1 = BootProcess1(input1).Count();
            Console.WriteLine($"Result part 1: {result1}"); // 298

            var input2 = input
                .Select(cell => (cell.x, cell.y, z: 0, z2: 0))
                .ToHashSet();
            var result2 = BootProcess2(input2).Count();
            Console.WriteLine($"Result part 2: {result2}"); // 1792
        }

        private static ICollection<(int x, int y, int z)> BootProcess1(ICollection<(int, int, int)> activeCells)
        {
            var actives = activeCells;
            for (var i = 0; i < 6; i++)
            {
                var survivalNrs = new[] { 2, 3 };
                IEnumerable<(int x, int y, int z)> MakeNewborns((int, int, int) active)
                    => AllNeighbors1(active).Except(actives).Where(neighbor
                        => AllNeighbors1(neighbor).Intersect(actives).Count() == 3);

                var newborns = actives.SelectMany(MakeNewborns);
                var survivors = actives.Where(active =>
                    survivalNrs.Contains(AllNeighbors1(active).Intersect(actives).Count()));
                actives = newborns.Union(survivors).ToHashSet();
            }
            return actives;
        }

        private static (int x, int y, int z)[] AllNeighbors1((int x, int y, int z) center)
            => new[] { center.x - 1, center.x, center.x + 1 }
                .SelectMany(x => new[] { center.y - 1, center.y, center.y + 1 }
                    .SelectMany(y => new[] { center.z - 1, center.z, center.z + 1 }
                        .Select(z => (x, y, z)))).Except(new[] { center }).ToArray();


        private static ICollection<(int x, int y, int z, int z2)> BootProcess2(ICollection<(int, int, int, int)> activeCells)
        {
            var actives = activeCells;
            for (var i = 0; i < 6; i++)
            {
                var survivalNrs = new[] { 2, 3 };
                IEnumerable<(int x, int y, int z, int z2)> MakeNewborns2((int, int, int, int) active)
                    => AllNeighbors2(active).Except(actives).Where(neighbor
                        => AllNeighbors2(neighbor).Intersect(actives).Count() == 3);

                var newborns = actives.SelectMany(MakeNewborns2);
                var survivors = actives.Where(active =>
                    survivalNrs.Contains(AllNeighbors2(active).Intersect(actives).Count()));
                actives = newborns.Union(survivors).ToHashSet();
            }
            return actives;
        }

        private static (int x, int y, int z, int z2)[] AllNeighbors2((int x, int y, int z, int z2) center)
            => new[] { center.x - 1, center.x, center.x + 1 }
                .SelectMany(x => new[] { center.y - 1, center.y, center.y + 1 }
                    .SelectMany(y => new[] { center.z - 1, center.z, center.z + 1 }
                        .SelectMany(z => new[] { center.z2 - 1, center.z2, center.z2 + 1 }
                            .Select(z2 => (x, y, z, z2))))).Except(new[] { center }).ToArray();

    }
}
