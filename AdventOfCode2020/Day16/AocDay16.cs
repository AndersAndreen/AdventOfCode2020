using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day16
{
    class AocDay16
    {
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 16");

            var input = File
                .ReadAllLines(@"Day16\AocDay16.txt")
                .ToArray();
            var resultStep1 = new TicketSolver(input).SolvePart1();
            Console.WriteLine($"Result part 1: {resultStep1}");

            var resultStep2 = new TicketSolver(input).SolvePart2();
            Console.WriteLine($"Result part 2: {resultStep2}");
        }
    }

    public class TicketSolver
    {
        private readonly List<TicketRule> _rules = new List<TicketRule>();
        private readonly int[] _yourTicket = new int[]{};
        private List<int[]> _tickets = new List<int[]>();

        public TicketSolver(string[] input)
        {
            var partNr = 0;
            foreach (var row in input.Where(row => !string.IsNullOrWhiteSpace(row)))
            {
                switch (row)
                {
                    case "your ticket:":
                        partNr = 1;
                        continue;
                    case "nearby tickets:":
                        partNr = 2;
                        continue;
                    default:
                        switch (partNr)
                        {
                            case 0:
                                _rules.Add(new TicketRule(row));
                                break;
                            case 1:
                                _yourTicket = row.Split(',').Select(int.Parse).ToArray();
                                break;
                            default:
                                _tickets.Add(row.Split(',').Select(int.Parse).ToArray());
                                break;
                        }
                        break;
                }
            }
        }

        public long SolvePart1() => _tickets.SelectMany(nrs => nrs.Where(nr => !_rules.Any(rule => rule.Matches(nr)))).Sum();
        public long SolvePart2()
        {
            var correctTickets = _tickets.Where(nrs => nrs.All(nr => _rules.Any(rule => rule.Matches(nr)))).ToList();
            //correctTickets.Add(_yourTicket);
            var numFields = _tickets.First().Length;
            var fieldNames = new List<(int, string)>();
            var fieldNameCandidates = new List<(int, string[])>();
            for (var i = 0; i < numFields; i++)
            {
                var matchingFieldName = (index: i, fieldNames: _rules
                    .Where(rule => correctTickets.Select(ticket => ticket[i]).All(rule.Matches))
                    .Select(rule => rule.RuleName).ToArray());
                fieldNameCandidates.Add(matchingFieldName);
            }

            do
            {
                var singles = fieldNameCandidates
                    .Where(fnc => fnc.Item2.Length == 1)
                    .ToList();
                singles.ForEach(single => fieldNameCandidates.Remove(single));
                fieldNames.AddRange(singles.Select(fnc => (fnc.Item1, fnc.Item2.Single())));
                fieldNameCandidates = fieldNameCandidates.Select(c => (c.Item1,
                    c.Item2.Where(name => !fieldNames.Select(fn => fn.Item2).Contains(name)).ToArray())).ToList();
            } while (fieldNameCandidates.Count > 0);


            var result = fieldNames
                .Where(fn => fn.Item2.StartsWith("departure"))
                .Select(fn => (long)_yourTicket[fn.Item1])
                .Aggregate((long accumulator, long item) => accumulator * item);
            return result;
        }
    }

    internal class TicketRule
    {
        public string RuleName;
        public (int, int) rule1;
        public (int, int) rule2;
        public TicketRule(string row)
        {
            var x = row.Split(':');
            RuleName = x[0];
            var y = x[1].Split("or");
            var r1 = y[0].Split('-').Select(int.Parse).ToArray();
            rule1 = (r1[0], r1[1]);
            var r2 = y[1].Split('-').Select(int.Parse).ToArray();
            rule2 = (r2[0], r2[1]);
        }

        public bool Matches(int value)
        {
            var xx = (value >= rule1.Item1 && value <= rule1.Item2)
                || (value >= rule2.Item1 && value <= rule2.Item2);
            return xx;
        }
    }
}
