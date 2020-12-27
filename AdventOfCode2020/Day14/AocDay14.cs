using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day14
{
    class AocDay14
    {
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 14");

            var input = File
                .ReadAllLines(@"Day14\AocDay14.txt")
                .ToArray();
            var resultStep1 = new CodeInitializer().SolvePart1(input);
            Console.WriteLine($"Result part 1: {resultStep1}");

            var resultStep2 = new CodeInitializer().SolvePart2(input);
            Console.WriteLine($"\nResult part 2: {resultStep2}");
        }
    }

    public class CodeInitializer
    {
        private readonly Dictionary<string, long> _memory = new Dictionary<string, long> { };
        private string _mask = "";

        public long SolvePart1(string[] input)
        {
            var commands = new Dictionary<string, Action<string>>
            {
                {"mask", param => _mask = param[3..]},
                {
                    "mem[", param =>
                    {
                        var orValue = Convert.ToInt64(_mask.Replace("X", "0"), 2);
                        var andValue = Convert.ToInt64(_mask.Replace("X", "1"), 2);
                        var x = param.Split("] = ");
                        _memory[x[0]] = int.Parse(x[1]) & andValue | orValue;
                    }
                },
            };
            return RunInitializer(input, commands);
        }

        private long RunInitializer(string[] input, Dictionary<string, Action<string>> commands)
        {
            foreach (var row in input)
            {
                var cmd = row.Substring(0, 4);
                var prm = row[4..];
                commands[cmd](prm);
            }
            return _memory.Sum(kv => kv.Value);
        }

        public long SolvePart2(string[] input)
        {
            var commands = new Dictionary<string, Action<string>>
            {
                {"mask", param => _mask = param[3..]},
                {
                    "mem[", param =>
                    {
                        var x = param.Split("] = ");
                        var combinations = GetCombinations(ApplyMask(_mask, x[0])).ToHashSet();
                        foreach (var address in combinations)
                        {
                            _memory[address] = int.Parse(x[1]);
                        }
                    }
                },
            };
            return RunInitializer(input, commands);
        }

        private string[] GetCombinations(string prm)
        {
            if (prm.Count(p => p == 'X') == 1)
            {
                return new[] { prm.Replace('X', '0'), prm.Replace('X', '1') };
            };
            var firstX = prm.IndexOf('X');
            var combinations = new[]
            {
                GetCombinations(prm.Remove(firstX, 1).Insert(firstX, "0")),
                GetCombinations( prm.Remove(firstX, 1).Insert(firstX, "1"))
            }.SelectMany(x => x).ToArray();
            return combinations;
        }

        private string ApplyMask(string mask, string input)
        {
            var combo = new Dictionary<string, string>
            {
                {"00", "0"},
                {"01", "1"},
                {"10", "1"},
                {"11", "1"},
                {"X0", "X"},
                {"X1", "X"},
            };
            var binInput = Convert.ToString(int.Parse(input), 2).PadLeft(mask.Length, '0');
            var x = mask.Zip(binInput)
                .Select(zip => $"{zip.First}{zip.Second}")
                .Select(cmb => combo[cmb])
                .Aggregate((b, xx) => b + xx);
            return x;
        }
    }
}
