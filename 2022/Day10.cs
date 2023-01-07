using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022;

public class Day10 : ISolve
{
    public static string Solve1(string input, params object[] args)
    {
        var commands = input.Split(Environment.NewLine);
        var machine = new Machine();
        machine.Load(commands);
        var sum = machine.Run()
            .Take(220)
            .Where(x => x.Cycle is 20 or 60 or 100 or 140 or 180 or 220)
            .Select(x => x.Cycle * x.Value)
            .Sum();

        return sum.ToString();
    }

    public static string Solve2(string input, params object[] args)
    {
        var commands = input.Split(Environment.NewLine);
        var machine = new Machine();
        machine.Load(commands);
        
        var sb = new StringBuilder();
        foreach (var (cycle, value) in machine.Run().Take(240))
        {
            var xOffset = (cycle - 1) % 40;

            if (xOffset >= value - 1 && xOffset <= value + 1)
                sb.Append('#');
            else
                sb.Append('.');

            if (xOffset is 39)
                sb.AppendLine();
        }

        return sb.ToString();
    }

    class Machine
    {
        private int _rX = 1;
        private Command[]? _commands;

        public int RegisterX => _rX;

        public void Load(string[] commands)
        {
            _commands = commands.Select(Command.Parse).ToArray();
        }

        private IEnumerable<Command> NextCommand()
        {
            if (_commands is null || _commands.Length is 0)
                yield break;

            var index = 0;
            while (true)
            {
                var command = _commands[index % _commands.Length];
                yield return command;
                
                index++;
            }
        }

        public IEnumerable<(int Cycle, int Value)> Run()
        {
            var rX = 1;
            var cycle = 0;

            foreach (var command in NextCommand())
            {
                foreach(var _ in Enumerable.Range(1, command.Cycles))
                {
                    cycle++;

                    yield return (cycle, rX);
                }

                if (command is AddxCommand addxCommand)
                    rX += addxCommand.IncValue;
            }
        }
    }

    abstract record Command()
    {
        public abstract int Cycles { get; }

        public static Command Parse(string input)
        {
            var tokens = input.Split(' ', StringSplitOptions.TrimEntries);
            if (tokens[0] is "noop")
                return new NoopCommand();
            else if (tokens[0] is "addx")
                return new AddxCommand(int.Parse(tokens[1]));

            throw new InvalidOperationException();
        }
    }
    record NoopCommand() : Command
    {
        public override int Cycles => 1;
    }
    record AddxCommand(int IncValue) : Command
    {
        public override int Cycles => 2;
    }
}
