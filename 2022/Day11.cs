using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _2022;

public class Day11 : ISolve
{
    public static string Solve1(string input)
    {
        var monkeys = input.Split(Environment.NewLine)
            .Chunk(7)
            .Select(x => Monkey.Parse(x, 3))
            .ToArray();

        var targetRound = 20;

        foreach (var _ in Enumerable.Range(1, targetRound))
        {
            foreach (var monkey in monkeys)
            {
                monkey.Test((number, value) =>
                {
                    var handingMonkey = monkeys.First(x => x.Number == number);
                    handingMonkey.Has(value);
                });
            }
        }

        return monkeys
            .OrderByDescending(x => x.Times)
            .Take(2)
            .Aggregate(1, (multi, monkey) => multi * monkey.Times)
            .ToString();
    }

    public static string Solve2(string input)
    {
        var monkeys = input.Split(Environment.NewLine)
            .Chunk(7)
            .Select(x => Monkey.Parse(x, 1))
            .ToArray();

        var mulDivision = monkeys.Aggregate(1UL, (multi, monkey) => multi * (ulong)monkey.TestDivision);

        var targetRound = 10000;

        foreach (var round in Enumerable.Range(1, targetRound))
        {
            //Console.WriteLine(round);
            foreach (var monkey in monkeys)
            {
                monkey.Test((number, value) =>
                {
                    var handingMonkey = monkeys.First(x => x.Number == number);
                    handingMonkey.Has(value % mulDivision);
                });
            }
        }

        return monkeys
            .OrderByDescending(x => x.Times)
            .Take(2)
            .Aggregate(1L, (multi, monkey) => multi * monkey.Times)
            .ToString();
    }

    class Monkey
    {
        private Func<ulong, ulong> _operation;
        private Queue<ulong> _items;
        private int _ridiculousLevel;
        private int _testDivision;
        private int _divisibleTrueMonkeyNumber;
        private int _divisibleFalseMonkeyNumber;

        public int Number { get; }

        public int Times { get; private set; }

        public int TestDivision => _testDivision;


        private Monkey(int number, ulong[] items, Func<ulong, ulong> operation, int ridiculousLevel, int testDivision, int divisibleTrueMonkeyNumber, int divisibleFalseMonkeyNumber)
        {
            Number = number;
            _items = new Queue<ulong>(items);
            _operation = operation;
            _ridiculousLevel = ridiculousLevel;
            _testDivision = testDivision;
            _divisibleTrueMonkeyNumber = divisibleTrueMonkeyNumber;
            _divisibleFalseMonkeyNumber = divisibleFalseMonkeyNumber;
        }

        public void Test(Action<int, ulong> handingFunc)
        {
            var count = _items.Count;

            while (_items.Count > 0)
            {
                var item = _items.Dequeue();
                var result = _operation(item);
                var testResult = result / (ulong)_ridiculousLevel;
                if (testResult % (ulong)_testDivision == 0)
                    handingFunc(_divisibleTrueMonkeyNumber, testResult);
                else
                    handingFunc(_divisibleFalseMonkeyNumber, testResult);
            }

            Times += count;
        }

        public void Has(ulong value)
        {
            _items.Enqueue(value);
        }

        public static Monkey Parse(string[] strings, int ridiculousLevel)
        {
            var number = int.Parse(strings[0][7..8]);
            
            var items = strings[1][18..]
                .Split(',', StringSplitOptions.TrimEntries)
                .Select(ulong.Parse)
                .ToArray();
            
            var strOperation = strings[2][19..];
            
            var operation = (ulong x) => x;
            if (strOperation is "old * old")
                operation = x => x * x;
            else if (strOperation.StartsWith("old +") is true)
            {
                var value = ulong.Parse(strOperation[5..]);
                operation = x => x + value;
            }
            else if (strOperation.StartsWith("old *") is true)
            {
                var value = ulong.Parse(strOperation[5..]);
                operation = x => x * value;
            }
            else
                throw new InvalidOperationException();

            var testDivision = int.Parse(strings[3][21..]);
            var divisibleTrueMonkeyNumber = int.Parse(strings[4][29..]);
            var divisibleFalseMonkeyNumber = int.Parse(strings[5][30..]);

            return new Monkey(number, items, operation, ridiculousLevel, testDivision, divisibleTrueMonkeyNumber, divisibleFalseMonkeyNumber);
        }
    }
}
