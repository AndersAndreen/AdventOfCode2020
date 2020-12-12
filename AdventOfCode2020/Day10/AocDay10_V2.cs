using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day10
{
    public class AocDay10_V2
    {
        // This works for the examples but takes waaaaaay to long for the actual input data
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 10");
            var input = File
                .ReadAllLines(@"Day10\AocDay10.txt")
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .Select(int.Parse)
                .OrderBy(value => value)
                .ToArray();
            var tree = new AdapterCombinationTreeV2(input);

            //What is the number of 1-jolt differences multiplied by the number of 3-jolt differences?
            var resultStep1 = GetNumberOfJoltDifferences(input);
            Console.WriteLine($"Result part 1: 1:{resultStep1.Item1} 2:{resultStep1.Item2}");

            // What is the total number of distinct ways you can arrange the adapters to connect the charging outlet to your device?
            var resultStep2 = tree.CombinationsCount;
            Console.WriteLine($"Result part 2: {resultStep2}");
        }

        public static (long,long) GetNumberOfJoltDifferences(int[] input)
        {
            int diff1Count = 0, diff3Count = 0, previous = 0;
            foreach (var value in input)
            {
                diff1Count += value - previous == 1 ? 1 : 0;
                diff3Count += value - previous == 3 ? 1 : 0;
                previous = value;
            }
            diff3Count += previous == input.Max() ? 1 : 0;
            return (diff1Count,diff3Count);
        }
    }

    public class AdapterCombinationTreeV2
    {
        public long CombinationsCount { get; private set; }
        private readonly Dictionary<int, int[]> _nodes;

        public AdapterCombinationTreeV2(int[] input)
        {
            var startValue = 0;
            var voltages = new List<int>() { startValue };
            voltages.AddRange(input);
            voltages.Add(input.Max() + 3);
            _nodes = new Dictionary<int, int[]>();
            foreach (var (value, index) in voltages.Select((v, ix) => (v, ix)))
            {
                var branches = voltages.Skip(index + 1).Take(3).Where(v => v - value <= 3).ToArray();
                _nodes.Add(value, branches);
            }
            CountCombinationLeafs(startValue);
        }

        private void CountCombinationLeafs(int value)
        {
            if (_nodes[value].Length == 0)
            {
                CombinationsCount++;
                return;
            }
            foreach (var item in _nodes[value])
            {
                CountCombinationLeafs(item);
            }
        }
    }
}