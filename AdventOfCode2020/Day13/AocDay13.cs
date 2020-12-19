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
                .Select(row => row.ToCharArray())
                .ToArray();

            var departureTime = int.Parse(input[0]);
            var buses = new string(input.Skip(1).First()).Split(',')
                .Where(busNr => busNr != "x")
                .Select(int.Parse)
                .Select(busNr => (busNr, WaitTime: busNr - departureTime % busNr))
                .ToList();
            var minTime = buses.Min(bus => bus.WaitTime);
            var resultStep1 = buses.First(bus => bus.WaitTime == minTime).busNr * minTime;
            Console.WriteLine($"Result part 1: {resultStep1}");

            var resultStep2 = 2;
            Console.WriteLine($"Result part 2: {resultStep2}");
        }
    }
}