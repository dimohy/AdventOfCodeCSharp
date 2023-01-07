using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2022;

public class Day15 : ISolve
{
    public static string Solve1(string input, params object[] args)
    {
        var y = (int)args[0];

        var (grid, points) = Parse(input);
        //Console.WriteLine(grid);

        //Mark(grid, points); // 작은 맵의 경우 효과적이나 큰 맵의 경우 너무 오래 걸림
        //Console.WriteLine(grid);

        //return grid.GetAllPoints()
        //    .Where(x => x.Y == y && x.C is '#')
        //    .Count()
        //    .ToString();

        var count = FastCheck(grid, y, points).Where(x => x is true).Count();
        return count.ToString();
    }

    public static string Solve2(string input, params object[] args)
    {
        var (grid, points) = Parse(input);

        var result = points
            .Select(x => GetOutsides(x.Item1, x.Item1.Distance(x.Item2)).ToArray())
            .SelectMany(x => x)
            .ToArray()
            .GroupBy(x => x).Select(g => (g.Key, g.Count())).OrderByDescending(x => x.Item2).ToArray().First();


        return (result.Key.X * 4000000 + result.Key.Y).ToString();
    }

    static IEnumerable<Point> GetOutsides(Point sp, int distance)
    {
        var x = sp.X;
        var y = sp.Y - distance - 1;
        
        for (; y < sp.Y; x++, y++)
            yield return new Point(x, y);

        for (; x > sp.X; x--, y++)
            yield return new Point(x, y);

        for (; y > sp.Y; x--, y--)
            yield return new Point(x, y);

        for (; x < sp.X; x++, y--)
            yield return new Point(x, y);
    }

    static IEnumerable<bool> FastCheck(Grid grid, int y, IEnumerable<(Point Sensor, Point Beacon)> points)
    {
        // x 최소 최대는 센서에서 비콘간의 -거리 ~ +거리 만큼이다.
        var minX = points.Select(x => x.Sensor.X - x.Sensor.Distance(x.Beacon)).Min();
        var maxX = points.Select(x => x.Sensor.X + x.Sensor.Distance(x.Beacon)).Max();

        foreach (var x in Enumerable.Range(minX, maxX - minX + 1))
        {
            var cp = new Point(x, y);

            // grid를 사용하지 않고 points로도 가능
            if (grid[x,y] is 'S' or 'B')
            {
                yield return false;
                continue;
            }

            var bFound = false;
            foreach (var point in points)
            {
                var maxDistance = point.Sensor.Distance(point.Beacon);
                var checkDistance = point.Sensor.Distance(cp);
                if (checkDistance <= maxDistance)
                {
                    bFound = true;
                    break;
                }
            }

            yield return bFound;
        }
    }

    static void Mark(Grid grid, IEnumerable<(Point, Point)> points)
    {
        foreach (var p in points)
        {
            Console.WriteLine(p);
            MarkScan(p.Item1, p.Item2);
        }

        void MarkScan(Point sp, Point bp)
        {
            var distance = sp.Distance(bp);

            foreach (var y in Enumerable.Range(sp.X - distance, distance * 2 + 1))
            {
                foreach (var x in Enumerable.Range(sp.X - distance, distance * 2 + 1))
                {
                    if (sp.Distance(new(x, y)) > distance)
                        continue;

                    if (grid[x, y] is '.')
                        grid[x, y] = '#';
                }

                Console.WriteLine(y);
            }
        }
    }

    record Point(int X, int Y)
    {
        public int Distance(Point another) => Math.Abs(X - another.X) + Math.Abs(Y - another.Y);
    }

    static (Grid, IReadOnlyList<(Point, Point)> Points) Parse(string input, params object[] args)
    {
        var items = input.Split(Environment.NewLine)
            .Select(x => Regex.Match(x, @"Sensor at x=(-?[0-9]+), y=(-?[0-9]+): closest beacon is at x=(-?[0-9]+), y=(-?[0-9]+)"))
            .Select(x => (Sensor: new Point(int.Parse(x.Groups[1].Value), int.Parse(x.Groups[2].Value)), Becon: new Point(int.Parse(x.Groups[3].Value), int.Parse(x.Groups[4].Value))))
            .ToArray();

        var grid = new Grid('.');

        foreach (var p in items)
        {
            grid[p.Sensor.X, p.Sensor.Y] = 'S';
            grid[p.Becon.X, p.Becon.Y] = 'B';
        }

        return (grid, items);
    }

    class Grid
    {
        private readonly Dictionary<(int X, int Y), char> _grid = new();
        private char _clearChar;
        private int _padding;

        public (int Y, char C) Bottom { get; set; }

        public Grid(char clearChar = '.', int padding = 0) => (_clearChar, _padding) = (clearChar, padding);

        public char this[int x, int y]
        {
            get
            {
                if (Bottom.C is not '\0' && y == Bottom.Y)
                    return Bottom.C;

                var bResult = _grid.TryGetValue((x, y), out var value);
                if (bResult is false)
                    return _clearChar;

                return value;
            }
            set => _grid[(x, y)] = value;
        }

        public IEnumerable<(int X, int Y, char C)> GetAllPoints(bool withBorder = false)
        {
            var result = _grid.Select(x => (x.Key.X, x.Key.Y, x.Value));
            if (withBorder is true)
            {
                if (Bottom.C is not '\0')
                    result = result.Append((-1, Bottom.Y, Bottom.C));
            }

            return result;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var positions = GetAllPoints().Where(x => x.C != _clearChar);
            var left = positions.Min(x => x.X) - _padding;
            var right = positions.Max(x => x.X) + _padding;
            var top = positions.Min(x => x.Y) + _padding;
            var bottom = positions.Max(x => x.Y) + _padding;
            var width = right - left + 1;
            var height = bottom - top + 1;


            var printRowNum = (Func<int, char> func) =>
            {
                sb.Append("    ");
                foreach (var xNum in Enumerable.Range(left, width))
                {
                    if (xNum % 5 is 0)
                        sb.Append(func(xNum));
                    else
                        sb.Append(' ');
                }
                sb.AppendLine();
            };

            var strRight = right.ToString();
            for (var i = 0; i < strRight.Length; i++)
            {
                printRowNum(xNum => string.Format("{0," + strRight.Length.ToString() + "}", xNum)[i]);
                //printRowNum(xNum => $"{xNum,strRight.Length}"[i];
            }

            for (var y = top; y <= bottom; y++)
            {
                sb.Append($"{y,3} ");

                for (var x = left; x <= right; x++)
                    sb.Append(this[x, y]);

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
