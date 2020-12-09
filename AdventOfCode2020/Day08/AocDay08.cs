using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day08
{
    // Requires ar least .Net5.0(C#9) to run
    class AocDay08
    {
        public static void Run()
        {
            ConsoleOutput.Heading1("Advent of Code day 8");
            var program = File
                .ReadAllLines(@"Day08\AocDay08.txt")
                .Select(line => new Command(line))
                .ToList();

            var computer1 = new Computer();
            computer1.Load(program);
            computer1.Run();
            var resultStep1 = computer1.Accumulator;
            Console.WriteLine($"Result part 1: {resultStep1}");

            var computer2 = new Computer();
            var program2 = new List<Command>(program);
            foreach (var tuple in program.Select((command, index) => (command, index)))
            {
                program2[tuple.index] = program2[tuple.index].Operation switch
                {
                    Mnemonic.jmp => program2[tuple.index] with { Operation = Mnemonic.nop },
                    Mnemonic.nop => program2[tuple.index] with { Operation = Mnemonic.jmp },
                    _ => program2[tuple.index]
                };
                computer2.Load(program2);
                var suceeded = computer2.Run();
                if (suceeded)
                {
                    break;
                }
                program2[tuple.index] = program2[tuple.index].Operation switch
                {
                    Mnemonic.jmp => program2[tuple.index] with { Operation = Mnemonic.nop },
                    Mnemonic.nop => program2[tuple.index] with { Operation = Mnemonic.jmp },
                    _ => program2[tuple.index]
                };
            }
            var resultStep2 = computer2.Accumulator;
            Console.WriteLine($"Result part 2: {resultStep2}");
        }
    }

    public class Computer
    {
        public int ProgramCounter { get; private set; } = 0;
        public int Accumulator { get; private set; } = 0;
        private List<Command> _memory = new List<Command>();
        public Computer() { }
        public void Load(IEnumerable<Command> memory)
        {
            _memory = memory.Select(instruction => instruction.Reset()).ToList();
            ProgramCounter = 0;
            Accumulator = 0;
        }

        public bool Run()
        {
            try
            {
                do { } while (ExecuteCommand());
                return false; // No exception means brake because of an endless loop
            }
            catch (ArgumentOutOfRangeException e)
            {
                return true; // ArgumentOutOfRangeException means program ran to the end 
            }
        }

        private bool ExecuteCommand()
        {
            var pcChange = _memory[ProgramCounter].Execute();
            if (pcChange == 0) { return false; }
            Accumulator += _memory[ProgramCounter].AccChange;
            ProgramCounter += pcChange;
            return true;
        }
    }

    public record Command
    {
        public bool _hasExecuted { get; private set; } = false;
        public Mnemonic Operation { get; init; }
        public int Param { get; }

        public int AccChange = 0;

        public Command(string line)
        {
            var command = line.Trim().Split(' ');
            if (command.Length != 2) throw new ArgumentException("Unable to parse input line.");
            Operation = Enum.Parse<Mnemonic>(command[0]);
            Param = int.Parse(command[1]);
        }

        public int Execute()
        {
            if (_hasExecuted) return 0;
            _hasExecuted = true;
            // nop & acc: return 1 (next instruction)
            // jmp: return +-N (never null. throw ArgumentOutOfRangeException if index is out of range)
            // if _hasExecuted error return 0;
            AccChange = Operation switch
            {
                Mnemonic.nop => 0,
                Mnemonic.acc => Param,
                Mnemonic.jmp => 0,
                _ => throw new ArgumentOutOfRangeException(),
            };
            return Operation switch
            {
                Mnemonic.nop => 1,
                Mnemonic.acc => 1,
                Mnemonic.jmp => Param,
                _ => throw new ArgumentOutOfRangeException(),
            };

        }

        internal Command Reset()
        {
            _hasExecuted = false;
            return this;
        }
    }

    public enum Mnemonic
    // have a look at https://en.wikipedia.org/wiki/MOS_Technology_6502for some real mnemonics:
    // "A 6502 assembly language statement consists of a three-character instruction mnemonic, followed by any operands."
    {
        nop,
        acc,
        jmp
    }
}