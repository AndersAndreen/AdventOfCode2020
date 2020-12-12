using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day09
{
    public class AocDay09
    {
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 9");
            var encryptedInput = File
                .ReadAllLines(@"Day09\AocDay09.txt")
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .Select(long.Parse)
                .ToList();

            var lookBehindSize = 25;
            var resultStep1 = Solver.GetFirstNonConformingNumber(encryptedInput, lookBehindSize);
            Console.WriteLine($"Result part 1: {resultStep1}");

            var resultStep2 = Solver.GetEncryptionWeakness(encryptedInput, resultStep1);
            Console.WriteLine($"Result part 2: {resultStep2}");
        }
    }

    public class Solver
    {
        public static long GetFirstNonConformingNumber(IEnumerable<long> encryptedInput, int lookBehindSize)
        {
            var input = encryptedInput.ToList();
            if (input.Count < 3) { return -1; }
            if (lookBehindSize < 2) { return -1; }
            if (input.Count - lookBehindSize < 1) { return -1; }
            foreach (var (value, index1) in input.Skip(lookBehindSize).Select((value, index) => (value, index)))
            {
                if (!ValidateEncodedNr(input.Skip(index1).Take(lookBehindSize), value))
                {
                    return value;
                }
            }
            return -1;
        }

        public static bool ValidateEncodedNr(IEnumerable<long> previousNrs, long number)
        {
            var nrs = previousNrs.ToList();
            return nrs.Count >= 2 && nrs.Any(nr1 => nrs.Any(nr2 => nr1 + nr2 == number && nr1 != nr2));
        }

        public static long GetEncryptionWeakness(IEnumerable<long> encryptedInput, long resultStep1)
        {
            var input = encryptedInput.ToList();
            if (input.Count < 2) { return -1; }
            if (resultStep1 < 3) { return -1; }
            for (var index1 = 0; index1 <= input.Count - 2; index1++)
            {
                for (var index2 = 2; index2 <= input.Count; index2++)
                {
                    var slice = input.Skip(index1).Take(index2).ToList();
                    if (slice.Sum() == resultStep1)
                    {
                        return slice.Min() + slice.Max();
                    }
                }
            }
            return -1;
        }
    }
}