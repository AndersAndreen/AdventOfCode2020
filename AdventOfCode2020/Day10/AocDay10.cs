using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day10
{
    public class AocDay10
    {
        // This works for the examples but takes waaaaaay to long for the actual input data
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 10");
            var input = new List<int>() { 0 }.Union(
                File
                    .ReadAllLines(@"Day10\AocDay10.txt")
                    .Where(value => !string.IsNullOrWhiteSpace(value))
                    .Select(int.Parse)
                    .OrderBy(value => value)
            ).ToArray();

            // Question: "What is the number of 1-jolt differences multiplied by the number of 3-jolt differences?"
            var resultStep1 = GetNumberOfJoltDifferences(input);
            Console.WriteLine($"Result part 1: {resultStep1.Item1 * resultStep1.Item2}");

            // Question: "What is the total number of distinct ways you can arrange the adapters to connect the charging outlet to your device?"

            // As the problem states: "there must be more than a trillion valid ways" to arrange the adapters. 
            // But: The limitation of a maximum difference of 3 jolts between adapters has the effect that
            // whereever there is a difference of three jolts between two succeeding adapters there is only one way to go between them.
            // This cuts off the combination tree, and what we end up with is a set of independent combination trees chained together.
            // So, by splitting the list wherever there is a difference of 3 jolts and then parse each combination tree we 
            // end up with a list of choice sums, one for each section. If we then multiply the combination sums with each other
            // We get the total number of combinations possible.
            var resultStep2 =
                DivideInputToGroups(input)
                .Select(group2 => new AdapterCombinationTree(group2[1..], group2[0]).CombinationsCount)
                .Aggregate((x, y) => x * y);
            Console.WriteLine($"Result part 2: {resultStep2}");
        }

        private static List<int[]> DivideInputToGroups(int[] input)
        {
            var inputGroups = new List<int[]>();
            var previous = 0;
            var group = new List<int>();
            foreach (var joltage in input)
            {
                if (joltage - previous == 3)
                {
                    inputGroups.Add(group.ToArray());
                    group.Clear();
                }
                group.Add(joltage);

                previous = joltage;
            }
            inputGroups.Add(group.ToArray());
            return inputGroups;
        }

        public static (long, long) GetNumberOfJoltDifferences(int[] input)
        {
            int diff1Count = 0, diff3Count = 0, previous = 0;
            foreach (var value in input)
            {
                diff1Count += value - previous == 1 ? 1 : 0;
                diff3Count += value - previous == 3 ? 1 : 0;
                previous = value;
            }
            diff3Count += previous == input.Max() ? 1 : 0;
            return (diff1Count, diff3Count);
        }
    }

    public class AdapterCombinationTree
    {
        public long CombinationsCount { get; private set; }
        private readonly Dictionary<int, int[]> _nodes;
        public AdapterCombinationTree(int[] input, int startValue)
        {
            var voltages = new List<int>() { startValue };
            voltages.AddRange(input);
            voltages.Add(input.Any() ? input.Max() + 3 : startValue + 3);
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