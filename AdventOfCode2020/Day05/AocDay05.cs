using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day05
{
    public static class AocDay05
    {
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 5");
            var takenSeatNrs = File.ReadLines(@"Day05\AocDay05.txt")
                    .Select(seat => seat
                        .Replace("L", "0")
                        .Replace("R", "1")
                        .Replace("F", "0")
                        .Replace("B", "1"))
                    .Select(seat => (Row: seat[0..7], Column: seat[7..]))
                    .Select(seat => (Convert.ToInt16(seat.Row, 2), Convert.ToByte(seat.Column, 2)))
                    .Select(seat => seat.Item1 * 8 + seat.Item2)
                .ToList();
            string a = default;
            var resultStep1 = takenSeatNrs.Max();
            Console.WriteLine($"Result part 1: {resultStep1}");

            var allSeatNrs = Enumerable.Range(0, 1023).ToList();
            var nonTakenSeats = allSeatNrs
                .Where(seatNr => !takenSeatNrs.Contains(seatNr))
                .Where(seatNr => takenSeatNrs.Contains(seatNr - 1) && takenSeatNrs.Contains(seatNr + 1));
            var validPassportsStep2 = nonTakenSeats.Single();
            Console.WriteLine($"Result part 2: {validPassportsStep2}");
        }
    }
}
