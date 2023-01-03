using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _2022
{
    public class Day9 : ISolve
    {
        public static string Solve1(string input)
        {
            // 입력에서 헤드의 이동 얻기
            var headPoints = input
                .Split(Environment.NewLine)
                .Select(x => ParseHeadMove(x))
                .SelectMany(x => x)
                .Aggregate(Enumerable.Empty<Point>().Append(new Point(0, 0)), (items, item) => items.Append(items.Last() + item)) // 좀 더 적절한 LINQ 함수 찾아야 함
                .ToArray();

            var tailPoints = new List<Point>() { new Point(0, 0) };
            Point beforeHeadPoint = headPoints.First();
            foreach (var headPoint in headPoints.Skip(1))
            {
                var lastTailPoint = tailPoints.Last();

                if (lastTailPoint.IsAdjacency(headPoint) is false)
                    tailPoints.Add(beforeHeadPoint);

                beforeHeadPoint = headPoint;
            }

            return tailPoints.Distinct().Count().ToString();
        }

        static IEnumerable<Point> ParseHeadMove(string command)
        {
            var (cmd, move) = command.Split(' ')
                .Chunk(2)
                .Select(x => (x[0], int.Parse(x[1])))
                .First();

            var moving = cmd switch
            {
                "L" => new Point(-1, 0),
                "R" => new Point(1, 0),
                "U" => new Point(0, -1),
                "D" => new Point(0, 1),
                _ => throw new InvalidOperationException()
            };

            return Enumerable.Repeat(moving, move);
        }

        record struct Point(int X, int Y)
        {
            public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
            public bool IsAdjacency(Point a) => Math.Abs(X - a.X) <= 1 && Math.Abs(Y - a.Y) <= 1;
            public Point GetNearest(Point to) => (to.X - X, to.Y - Y) switch
            {
                (< 0, < 0) => new(X - 1, Y - 1),
                (< 0, 0) => new(X - 1, Y),
                (< 0, > 0) => new(X - 1, Y + 1),
                (0, > 0) => new(X, Y + 1),
                (> 0, > 0) => new(X + 1, Y + 1),
                (> 0, 0) => new(X + 1, Y),
                (> 0, < 0) => new(X + 1, Y - 1),
                (0, < 0) => new(X, Y - 1),
                _ => throw new InvalidOperationException()
            };
        }

        static void Draw(IEnumerable<Point> points)
        {
            var xLeft = points.Min(x => x.X);
            var xRight = points.Max(x => x.X);
            var yTop = points.Min(x => x.Y);
            var yBottom = points.Max(x => x.Y);

            var map = new string[xRight - xLeft + 1, yBottom - yTop + 1];
            foreach (var point in points)
            {
                map[-xLeft + point.X, -yTop + point.Y] = "#";
            }
            for (var y = 0; y < map.GetLength(1); y++)
            {
                for (var x = 0; x < map.GetLength(0); x++)
                {
                    if (map[x, y] == "#")
                        Console.Write("#");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }

        }

        public static string Solve2(string input)
        {
            // 입력에서 헤드의 이동 얻기
            var headHistory = input
                .Split(Environment.NewLine)
                .Select(x => ParseHeadMove(x))
                .SelectMany(x => x)
                .Aggregate(Enumerable.Empty<Point>().Append(new Point(0, 0)), (items, item) => items.Append(items.Last() + item)) // 좀 더 적절한 LINQ 함수 찾아야 함
                .ToArray();

            var tails = new List<Point>(Enumerable.Repeat(new Point(0, 0), 9));
            var tailHistory = new List<Point>(tails);

            foreach (var head in headHistory)
            {
                Point before = head;
                for (var i = 0; i < tails.Count; i++)
                {
                    var tail = tails[i];

                    if (tail.IsAdjacency(before) is false)
                    {
                        tails[i] = tail = tail.GetNearest(before);
                        
                        if (i == tails.Count - 1)
                            tailHistory.Add(tail);
                    }

                    before = tail;
                }
            }

            Draw(tailHistory);

            return tailHistory.Distinct().Count().ToString();
        }
    }
}
