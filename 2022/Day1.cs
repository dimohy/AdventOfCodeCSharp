using MoreLinq.Extensions;

namespace _2022;

public static class Day1
{
    /// <summary>
    /// 가장 많은 칼로리 수 반환
    /// </summary>
    public static void Solve1()
    {
        var lines = File.ReadAllLines("day1_input.txt");
        var (max, sum) = (0, 0);
        foreach (var line in lines)
        {
            if (line is "")
            {
                if (sum > max)
                    max = sum;
                sum = 0;

                continue;
            }

            sum += int.Parse(line);
        }

        Console.WriteLine(max);
    }

    public static void Solve1_1()
    {
        var max = File.ReadAllText("day1_input.txt")
            .Split(Environment.NewLine + Environment.NewLine)
            .Select(x =>
                x.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Sum(y => int.Parse(y))
            )
            .Max();

        Console.WriteLine(max);
    }

    public static void Solve1_2_MoreLINQ()
    {
        var max = File.ReadAllLines("day1_input.txt")
            .Split(x => x is "")
            .Select(x =>
                x.Select(y => int.Parse(y))
                .Sum())
            .Max();

        Console.WriteLine(max);
    }

    public static void Solve2()
    {
        var max = File.ReadAllText("day1_input.txt")
            .Split(Environment.NewLine + Environment.NewLine)
            .Select(x =>
                x.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Sum(y => int.Parse(y))
            )
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();

        Console.WriteLine(max);
    }
}
