using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022;

public class Day5 : ISolve
{
    public static string Solve1(string input)
    {
        //var lines = File.ReadAllText("day5_input.txt");
        //var lines = """
        //        [D]    
        //    [N] [C]    
        //    [Z] [M] [P]
        //     1   2   3 

        //    move 1 from 2 to 1
        //    move 3 from 1 to 3
        //    move 2 from 2 to 1
        //    move 1 from 1 to 2
        //    """;

        // 스택부와 명령부 라인 얻음
        var (stacksLines, commandLines) = input
            .Split(Environment.NewLine + Environment.NewLine)
            .Chunk(2)
            .Select(x =>
                (x[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries),
                x[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)))
            .First();

        // 스택부에서 박스 정보 얻음
        var stackBoxes = stacksLines
            .Select(x => x
                .Chunk(4)
                .Select((y, i) => (Number: i + 1, Box: y[1]))
                .Where(x => x.Box is not ' ') // 빈 슬롯 제외
            )
            .SkipLast(1) // 마지막 숫자부 제외
            .SelectMany(x => x)
            .Reverse(); // 아래부터 담아야 하므로 반전

        var stackCount = stackBoxes.Max(x => x.Number);

        // 박스를 스택에 담음
        var stacks = new Stack<char>[stackCount];
        foreach (var stackBox in stackBoxes)
        {
            ref var stack = ref stacks[stackBox.Number - 1];
            if (stack is null)
                stack = new Stack<char>();

            stack.Push(stackBox.Box);
        }

        // 명령부 처리
        foreach (var commandLine in commandLines)
        {
            var (moves, from, to) = commandLine.Split(' ')
                .Chunk(6)
                .Select(x => (int.Parse(x[1]), int.Parse(x[3]), int.Parse(x[5])))
                .First();

            foreach (var count in Enumerable.Range(1, moves))
            {
                var stack = stacks[from - 1].Pop();
                stacks[to - 1].Push(stack);
            }
        }

        var result = string.Concat(stacks.Select(x => x.First().ToString()));

        return result;
    }

    public static string Solve2(string input)
    {
        //var lines = File.ReadAllText("day5_input.txt");
        //var lines = """
        //        [D]    
        //    [N] [C]    
        //    [Z] [M] [P]
        //     1   2   3 

        //    move 1 from 2 to 1
        //    move 3 from 1 to 3
        //    move 2 from 2 to 1
        //    move 1 from 1 to 2
        //    """;

        // 스택부와 명령부 라인 얻음
        var (stacksLines, commandLines) = input
            .Split(Environment.NewLine + Environment.NewLine)
            .Chunk(2)
            .Select(x =>
                (x[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries),
                x[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)))
            .First();

        // 스택부에서 박스 정보 얻음
        var stackBoxes = stacksLines
            .Select(x => x
                .Chunk(4)
                .Select((y, i) => (Number: i + 1, Box: y[1]))
                .Where(x => x.Box is not ' ') // 빈 슬롯 제외
            )
            .SkipLast(1) // 마지막 숫자부 제외
            .SelectMany(x => x);

        var stackCount = stackBoxes.Max(x => x.Number);

        // 박스를 스택에 담음
        var stacks = new List<char>[stackCount];
        foreach (var stackBox in stackBoxes)
        {
            ref var stack = ref stacks[stackBox.Number - 1];
            if (stack is null)
                stack = new List<char>();

            stack.Add(stackBox.Box);
        }

        // 명령부 처리
        foreach (var commandLine in commandLines)
        {
            var (moves, from, to) = commandLine.Split(' ')
                .Chunk(6)
                .Select(x => (int.Parse(x[1]), int.Parse(x[3]), int.Parse(x[5])))
                .First();

            var fromStacks = stacks[from - 1];
            var toStacks = stacks[to - 1];

            var movingBoxes = fromStacks.GetRange(0, moves);
            fromStacks.RemoveRange(0, moves);
            toStacks.InsertRange(0, movingBoxes);
        }

        //var result = string.Concat(stacks.Select(x => x.First().ToString()));
        var result = stacks.Aggregate("", (x, stack) => x + stack.First());

        return result;
    }

}
