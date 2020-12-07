using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class ConsoleOutput
    {
        public static void Heading1(string heading)
        {
            Console.WriteLine();
            Console.WriteLine(new string('=', 30));
            Console.WriteLine($"  {heading}");
            Console.WriteLine(new string('=', 30));
        }
    }
}
