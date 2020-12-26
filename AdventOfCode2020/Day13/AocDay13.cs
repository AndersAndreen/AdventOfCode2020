using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day13
{
    class AocDay13
    {
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 13");

            var input = File
                .ReadAllLines(@"Day13\AocDay13.txt")
                .ToArray();
            var resultStep1 = SolvePart1(input);
            Console.WriteLine($"Result part 1: {resultStep1}");

            var resultStep2 = SolvePart2(input);
            Console.WriteLine($"\nResult part 2: {resultStep2}");
        }

        private static int SolvePart1(string[] input)
        {
            var departureTime = int.Parse(input[0]);
            var (i, waitTime) = input.Skip(1).First().Split(',')
                .Where(busNr => busNr != "x")
                .Select(int.Parse)
                .Select(busNr => (busNr, WaitTime: busNr - departureTime % busNr))
                .OrderBy(bus => bus.WaitTime)
                .First();
            return i * waitTime;
        }

        private static ulong SolvePart2(string[] input)
        {
            Console.WriteLine("\nStep 2:");
            var buses = input.Skip(1).First().Split(',')
                .Select((id, offset) => (id, offset: (ulong)offset))
                .Where(bus => bus.id != "x")
                .Select(bus => (id: ulong.Parse(bus.id), bus.offset))
                .Reverse()
                .ToList();

            var step = buses[0].id;
            ulong offs = 0;
            for (int i = 0; i < buses.Count - 1; i++)
            {
                var t = offs;
                ulong prev = 0;
                do
                {
                    t += step;
                    if ((t + buses[i + 1].offset - buses[0].offset) % buses[i + 1].id == 0)
                    {
                        prev = t;
                    }
                } while (prev == 0);
                step *= buses[i + 1].id;
                offs = prev;
                Console.WriteLine($"t{i} = {offs}, step size next round: {step}");
            }
            return offs - buses[0].offset;
        }
    }
}