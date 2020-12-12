using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day10
{
    public class AocDay10_V3
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
            var tree = new AdapterCombinationTreeV3(input);

            //What is the number of 1-jolt differences multiplied by the number of 3-jolt differences?
            var resultStep1 = GetNumberOfJoltDifferences(input);
            Console.WriteLine($"Result part 1: 1:{resultStep1.Item1} 2:{resultStep1.Item2}");

            // What is the total number of distinct ways you can arrange the adapters to connect the charging outlet to your device?
            var resultStep2 = tree.TotalCombinations;
            Console.WriteLine($"Result part 2: {resultStep2}");
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

    public class AdapterCombinationTreeV3
    {
        public long TotalCombinations => _partialTrees.Aggregate((x, y) => x * y);
        private long _combinationsCount;
        private readonly Dictionary<int, int[]> _nodes;
        public int StepStop { get; private set; } = 0;
        private List<long> _partialTrees = new List<long>();
        private bool _break;
        private int _max;
        public AdapterCombinationTreeV3(int[] input)
        {
            var voltages = new List<int>() { 13 };
            voltages.AddRange(input);
            voltages.Add(input.Max() + 3);
            _max = voltages.Max();
            _nodes = new Dictionary<int, int[]>();
            foreach (var (value, index) in voltages.Select((v, ix) => (v, ix)))
            {
                var branches = voltages.Skip(index + 1).Take(3).Where(v => v - value <= 3).ToArray();
                _nodes.Add(value, branches);
            }

            CountCombinationLeafs(0);


            do
            {
                CountCombinationLeafs(StepStop);
                _partialTrees.Add(_combinationsCount);
                _combinationsCount = 0;
            } while (StepStop < _max);
        }

        private void CountCombinationLeafs(int value)
        {
            if (_nodes[value].Length == 1 && _nodes[value][0] - value == 3)
            {
                if (_break)
                {
                    _combinationsCount++;
                    StepStop = _nodes[value][0];
                    _break = false;
                    return;
                }
                else
                {
                    _break = true;

                }
            }
            foreach (var item in _nodes[value])
            {
                CountCombinationLeafs(item);
            }
        }


    }
}