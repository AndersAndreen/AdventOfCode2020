using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day11
{
    class AocDay11
    {
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 11");

            var input = File
                .ReadAllLines(@"Day11\AocDay11.txt")
                .Select(row => row.ToCharArray())
                .ToArray();
            var resultStep1 = ProcessMap1(input);
            Console.WriteLine($"Result part 1: {resultStep1}");

            input = File
                .ReadAllLines(@"Day11\AocDay11.txt")
                .Select(row => row.ToCharArray())
                .ToArray();

            bool change;
            do
            {
                var output = ProcessMap2(input);
                change = !input
                    .Select((row, i) => row.SequenceEqual(output[i]))
                    .All(alike => alike);
                input = output;
            } while (change);

            var resultStep2 = input.SelectMany(row => row).Count(ch => ch == '#');
            Console.WriteLine($"Result part 2: {resultStep2}");
        }

        private static char[][] ProcessMap2(char[][] input)
        {
            var output = input
                .Select(t => Enumerable.Repeat('.', input[0].Length).ToArray())
                .ToArray();
            for (var row = 0; row < input.Length; row++)
            {
                for (var col = 0; col < input[0].Length; col++)
                {
                    var vectors = new Func<(int drow, int dcol), (int, int)>[]
                    {
                        v => (v.drow - 1, v.dcol),
                        v => (v.drow - 1, v.dcol + 1),
                        v => (v.drow,     v.dcol + 1),
                        v => (v.drow + 1, v.dcol + 1),
                        v => (v.drow + 1, v.dcol),
                        v => (v.drow + 1, v.dcol - 1),
                        v => (v.drow,     v.dcol - 1),
                        v => (v.drow - 1, v.dcol - 1),
                    };
                    var visibleSeats = new List<char>();
                    foreach (var vector in vectors)
                    {
                        var peek = '.';
                        (int Row, int Col) peekPos = (row, col);
                        peekPos = vector(peekPos);
                        while (
                            peek == '.'
                            && peekPos.Row >= 0 && peekPos.Row < input.Length
                            && peekPos.Col >= 0 && peekPos.Col < input[0].Length)
                        {
                            peek = input[peekPos.Row][peekPos.Col];
                            peekPos = vector(peekPos);
                        }
                        visibleSeats.Add(peek);
                    }

                    var t = input[row][col];
                    output[row][col] = ProcessPos2(input[row][col], visibleSeats);
                }
            }
            //output.ToList().ForEach(row => Console.WriteLine(new string(row)));
            //Console.WriteLine();
            return output;
        }

        private static char ProcessPos2(char currentSeat, IReadOnlyCollection<char> visibleSeats)
        {
            return currentSeat switch
            {
                'L' => visibleSeats.All(ch => ch != '#') ? '#' : currentSeat,
                '#' => visibleSeats.Count(ch => ch == '#') > 4 ? 'L' : currentSeat,
                _ => currentSeat
            };
        }

        private static int ProcessMap1(char[][] input)
        {
            var emptyLine = new string('.', input[0].Length).ToCharArray();

            int diffCount;
            do
            {
                var currentUpdatedLine = new string('.', input[0].Length).ToCharArray();
                diffCount = 0;
                var previousUpdatedLine = ProcessLine1(emptyLine, input[0], input[1]);
                diffCount += input[0].SequenceEqual(previousUpdatedLine) ? 0 : 1;
                for (var i = 1; i < input.Length - 1; i++)
                {
                    currentUpdatedLine = ProcessLine1(input[i - 1], input[i], input[i + 1]);
                    diffCount += input[i].SequenceEqual(currentUpdatedLine) ? 0 : 1;
                    input[i - 1] = previousUpdatedLine;
                    previousUpdatedLine = currentUpdatedLine;
                }
                currentUpdatedLine = ProcessLine1(input[^2], input[^1], emptyLine);
                diffCount += input[^1].SequenceEqual(currentUpdatedLine) ? 0 : 1;
                input[^2] = previousUpdatedLine;
                input[^1] = currentUpdatedLine;
                //input.ToList().ForEach(row => Console.WriteLine(new string(row)));
                //Console.WriteLine();
            } while (diffCount > 0);
            return input.SelectMany(x => x).Count(x => x == '#');
        }
        private static char[] ProcessLine1(char[] previousLine, char[] currentLine, char[] nextLine)
        {
            var updatedLine = new string('.', previousLine.Length).ToCharArray();
            updatedLine[0] = ProcessPos1(
                new char[] { '.', previousLine[0], previousLine[1] },
                new char[] { '.', currentLine[0], currentLine[1] },
                new char[] { '.', nextLine[0], nextLine[1] });
            for (var i = 1; i < currentLine.Length - 1; i++)
            {
                updatedLine[i] = ProcessPos1(previousLine[(i - 1)..(i + 2)], currentLine[(i - 1)..(i + 2)], nextLine[(i - 1)..(i + 2)]);
            }
            updatedLine[^1] = ProcessPos1(
                new char[] { previousLine[^2], previousLine[^1], '.' },
                new char[] { currentLine[^2], currentLine[^1], '.' },
                new char[] { nextLine[^2], nextLine[^1], '.' });
            return updatedLine;
        }

        private static char ProcessPos1(char[] previous, char[] current, char[] next)
        {
            return current[1] switch
            {
                'L' => previous.Concat(current).Concat(next).All(ch => ch != '#') ? '#' : current[1],
                '#' => previous.Concat(current).Concat(next).Count(ch => ch == '#') > 4 ? 'L' : current[1],
                _ => current[1]
            };
        }
    }
}
