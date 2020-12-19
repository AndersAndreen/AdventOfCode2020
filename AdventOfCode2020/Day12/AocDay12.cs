using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day12
{
    class AocDay12
    {
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 12");

            var input = File
                .ReadAllLines(@"Day12\AocDay12.txt")
                .ToArray();
            var boat1 = new Boat1();
            foreach (var command in input)
            {
                boat1.Navigate(command);
            }
            var resultStep1 = boat1.ManhattanDistance;
            Console.WriteLine($"Result part 1: {resultStep1}");

            var boat2 = new Boat2();
            foreach (var command in input)
            {
                boat2.Navigate(command);
            }
            var pos = boat2.Position;
            var resultStep2 = boat2.ManhattanDistance;
            Console.WriteLine($"Result part 2: {resultStep2}");
        }
    }

    class Boat1
    {
        public (int X, int Y) Position { get; private set; }
        public int ManhattanDistance => Math.Abs(Position.X) + Math.Abs(Position.Y);
        private int _direction;
        private readonly Dictionary<char, Action<int>> _commands;

        public Boat1() //2879
        {
            _commands = new Dictionary<char, Action<int>>
            {
                {'N', param => Position = (Position.X, Position.Y + param)},
                {'S', param => Position = (Position.X, Position.Y - param)},
                {'E', param => Position = (Position.X + param, Position.Y)},
                {'W', param => Position = (Position.X - param, Position.Y)},
                {'L', param => _direction = (_direction + param) % 360},
                {'R', param => _direction = (_direction - param) % 360},
                {
                    'F', param =>
                    {
                        var dx = (int) Math.Round(Math.Cos(Math.PI / 180 * _direction) * param);
                        var dy = (int) Math.Round(Math.Sin(Math.PI / 180 * _direction) * param);
                        Position = (Position.X + dx, Position.Y + dy);
                    }
                },
            };
        }

        public void Navigate(string command) => _commands[command[0]](int.Parse(command[1..]));
    }

    class Boat2
    {
        public (int X, int Y) Position { get; private set; }
        public (int X, int Y) Waypoint { get; private set; } = (10, 1);
        public int ManhattanDistance => Math.Abs(Position.X) + Math.Abs(Position.Y);
        private readonly Dictionary<char, Action<int>> _commands;

        public Boat2()
        {
            _commands = new Dictionary<char, Action<int>>
            {
                {'N', param => Waypoint = (Waypoint.X, Waypoint.Y + param)},
                {'S', param => Waypoint = (Waypoint.X, Waypoint.Y - param)},
                {'E', param => Waypoint = (Waypoint.X + param, Waypoint.Y)},
                {'W', param => Waypoint = (Waypoint.X - param, Waypoint.Y)},
                {'R', param => Turn(-param)},
                {'L', param => Turn(param)},
                {'F', param => Position = (Position.X + Waypoint.X * param, Position.Y + Waypoint.Y * param)}
            };
        }

        private void Turn(int param)
        {
            Waypoint = (
                (int)Math.Round(Math.Cos(Math.PI / 180 * (param))) * Waypoint.X +
                (int)Math.Round(Math.Cos(Math.PI / 180 * (param + 90))) * Waypoint.Y,
                (int)Math.Round(Math.Sin(Math.PI / 180 * (param))) * Waypoint.X +
                (int)Math.Round(Math.Sin(Math.PI / 180 * (param + 90)) * Waypoint.Y));
        }

        public void Navigate(string command) => _commands[command[0]](int.Parse(command[1..]));
    }
}


