using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022;

public class Day8 : ISolve
{
    public static string Solve1(string input)
    {
        var map = MakeMap(input);
        var check = new bool[map.GetLength(0), map.GetLength(1)];

        for (var y = 0; y < map.GetLength(1); y++)
            ScanH(y);
        for (var x = 0; x < map.GetLength(0); x++)
            ScanV(x);

        var sum = 0;
        for (var y = 0; y < check.GetLength(1); y++)
            for (var x = 0; x < check.GetLength(0); x++)
                sum += check[x, y] is true ? 1 : 0;

        return sum.ToString();

        void ScanH(int y)
        {
            var high1 = -1;
            var high2 = -1;
            var length = map.GetLength(0);
            for (var x = 0; x < length; x++)
            {
                if (map[x, y] > high1)
                {
                    high1 = map[x, y];
                    check[x, y] = true;
                }
                if (map[length - x - 1, y] > high2)
                {
                    high2 = map[length - x - 1, y];
                    check[length - x - 1, y] = true;
                }
            }
        }

        void ScanV(int x)
        {
            var high1 = -1;
            var high2 = -1;
            var length = map.GetLength(1);
            for (var y = 0; y < length; y++)
            {
                if (map[x, y] > high1)
                {
                    high1 = map[x, y];
                    check[x, y] = true;
                }
                if (map[x, length - y - 1] > high2)
                {
                    high2 = map[x, length - y - 1];
                    check[x, length - y - 1] = true;
                }
            }
        }
    }

    static int[,] MakeMap(string input)
    {
        var lines = input.Split(Environment.NewLine);

        var map = new int[lines[0].Length, lines.Length];
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                map[x, y] = int.Parse(line[x].ToString());
            }
        }

        return map;
    }

    public static string Solve2(string input)
    {
        var map = MakeMap(input);

        var maxScore = 0;
        for (var y = 0; y < map.GetLength(1); y++)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                var score = ScanScore(x, y);
                if (score > maxScore)
                    maxScore = score;
            }
        }

        return maxScore.ToString();

        int ScanScore(int x, int y)
        {
            var xLength = map.GetLength(0);
            var yLength = map.GetLength(1);

            var score = 1;
            var center = map[x, y];

            // 왼쪽 탐색
            var (sx, sy) = (x - 1, y);
            while (sx >= 0)
            {
                var another = map[sx, sy];
                sx--;

                if (another >= center)
                    break;
            }
            score *= x - (sx + 1);

            // 오른쪽 탐색
            (sx, sy) = (x + 1, y);
            while (sx < xLength)
            {
                var another = map[sx, sy];
                sx++;

                if (another >= center)
                    break;
            }
            score *= (sx - 1) - x;

            // 위 탐색
            (sx, sy) = (x, y - 1);
            while (sy >= 0)
            {
                var another = map[sx, sy];
                sy--;

                if (another >= center)
                    break;
            }
            score *= y - (sy + 1);

            // 아래 탐색
            (sx, sy) = (x, y + 1);
            while (sy < yLength)
            {
                var another = map[sx, sy];
                sy++;

                if (another >= center)
                    break;
            }
            score *= (sy - 1) - y;

            return score;
        }
    }
}
