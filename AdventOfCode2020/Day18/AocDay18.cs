using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day18
{
    class AocDay18
    {
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 18");
            var input = File
                .ReadAllLines(@"Day18\AocDay18.txt")
                .ToList();

            var result1 = input.Sum(row => new Scope(row, Rule.Part1).Calc());
            Console.WriteLine($"Result part 1: {result1}");

            var result2 = input.Sum(row => new Scope(row, Rule.Part2).Calc());
            Console.WriteLine($"Result part 2: {result2}");
        }

        public enum Rule { Part1, Part2 }

        public class Scope
        {
            private readonly Queue<char> _queue;
            private readonly Dictionary<char, Func<long>> _operations = new Dictionary<char, Func<long>>();
            private readonly Func<bool> _stopCondition;
            private long _sum;

            public Scope(string input, Rule rule)
            {
                _queue = new Queue<char>(input.Replace(" ", "").ToCharArray());
                _stopCondition = () => _queue.Count > 0;
                Init(rule);
            }

            private Scope(Queue<char> queue, Rule rule, Func<bool> stopCondition)
            {
                _queue = queue;
                _stopCondition = stopCondition;
                Init(rule);
            }

            private void Init(Rule rule)
            {
                foreach (var key in "0123456789")
                {
                    var val = int.Parse(key.ToString());
                    _operations.Add(key, () => val);
                }
                switch (rule)
                {
                    case Rule.Part1:
                        _operations.Add('*', () => _sum *= _operations[_queue.Dequeue()]());
                        break;
                    default:
                        _operations.Add('*', () => _sum *= new Scope(_queue, rule, () => _queue.Count > 0 && _queue.Peek() != '*' && _queue.Peek() != ')').Calc());
                        break;
                }
                _operations.Add('+', () => _sum += _operations[_queue.Dequeue()]());
                _operations.Add('(', () => new Scope(_queue, rule, () => (_queue.Peek() != ')' || _queue.Dequeue() != ')')).Calc());
                _operations.Add(')', () =>
                {
                    _queue.Dequeue();
                    return _sum;
                });
            }

            public long Calc()
            {
                _sum = _operations['+']();
                while (_stopCondition())
                {
                    _operations[_queue.Dequeue()]();
                }
                return _sum;
            }
        }
    }
}
