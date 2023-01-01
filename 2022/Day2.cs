using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022;

public class Day2
{
    /// <summary>
    /// 가위바위보 게임에서 승리하자
    /// </summary>
    public static void Solve1()
    {
        var lines = File.ReadAllLines("day2_input.txt");

        var sum = 0;
        foreach (var line in lines)
        {
            var temp = line.Split();
            var y = temp[0] switch { "A" => Kind.바위, "B" => Kind.보, "C" => Kind.가위, _ => throw new InvalidOperationException() };
            var i = temp[1] switch { "X" => Kind.바위, "Y" => Kind.보, "Z" => Kind.가위, _ => throw new InvalidOperationException() };

            var score = (int)i;

            // 이긴 경우
            if ((i, y) is (Kind.바위, Kind.가위) ||
                (i, y) is (Kind.보, Kind.바위) ||
                (i, y) is (Kind.가위, Kind.보))
                score += 6;
            // 비긴 경우
            else if (i == y)
                score += 3;

            sum += score;
        }

        Console.WriteLine(sum);
    }

    public static void Solve1_1()
    {
        var lines = File.ReadAllLines("day2_input.txt");

        var sum = 0;
        foreach (var line in lines)
        {
            var y = line[0] - 'A';
            var i = line[2] - 'X';

            var score = 0;
            // 비긴경우
            if (i == y)
                score = 3;
            // 이긴경우
            else if (i == (y + 1) % 3)
                score = 6;

            sum += score + i + 1;
        }

        Console.WriteLine(sum);
    }

    public static void Solve2()
    {
        var lines = File.ReadAllLines("day2_input.txt");

        var sum = 0;
        foreach (var line in lines)
        {
            var y = line[0] - 'A';  // 0:바위, 1:보, 2:가위
            var r = line[2] - 'X';  // 0:짐, 1:비김, 2:이김

            // 0, 0 : 바위, i = 2
            // 0, 1 : 바위, i = 0
            // 0, 2 : 바위, i = 1
            // 1, 0 : 보, i = 0
            // 1, 1 : 보, i = 1
            // 1, 2 : 보, i = 2
            // 2, 0 : 가위, i = 1
            // 2, 1 : 가위, i = 2
            // 2, 2 : 가위, i = 0

            var i = (y + r + 2) % 3;
            sum += r * 3 + i + 1;
        }

        Console.WriteLine(sum);
    }
}

internal enum  Kind
{
    바위 = 1,
    보 = 2,
    가위 = 3
}
