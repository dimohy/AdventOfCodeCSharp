using SuperLinq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022
{
    /// <summary>
    /// 풀이 전략 :
    /// 1. 입력 값으로 동굴을 시각화
    /// 2. 시각화된 화면으로 모래의 떨어짐을 시뮬레이션
    /// 3. 시간의 흐름은 스트림으로 처리
    /// </summary>
    public class Day14 : ISolve
    {
        public static string Solve1(string input, params object[] args)
        {
            var grid = Parse(input);
            Console.WriteLine(grid);

            var count = Simulate(grid).Where(x => x is true).Count();

            Console.WriteLine(grid);

            return count.ToString(); ;
        }

        public static string Solve2(string input, params object[] args)
        {
            var grid = Parse(input);
            Console.WriteLine(grid);

            var maxRockY = grid.GetAllPotisions(true).Where(x => x.C is '#').Max(x => x.Y);
            grid.Bottom = (maxRockY + 2, '#');

            var count = Simulate(grid).Where(x => x is true).Count();
            Console.WriteLine(grid);

            return count.ToString();
        }

        static Grid Parse(string input)
        {
            var parsed = input.Split(Environment.NewLine)
                .Select(x => x.Split(" -> ")
                    .Select(y => y.Split(",")
                        .Chunk(2)
                        .Select(z => (X: int.Parse(z[0]), Y: int.Parse(z[1])))
                        .First()
                    ).ToArray()
                ).ToArray();

            var grid = new Grid();

            foreach (var rock in parsed)
            {
                foreach (var p in rock.Window(2))
                    grid.DrawLine(p[0].X, p[0].Y, p[1].X, p[1].Y, '#');
            }

            return grid;
        }

        static IEnumerable<bool> Simulate(Grid grid)
        {
            var sp = (X: 500, Y: 0);
            var maxRockY = grid.GetAllPotisions(true).Where(x => x.C is '#').Max(x => x.Y);

            while (true)
            {
                // 모래가 모두 쌓이면 종료
                if (grid[sp.X, sp.Y] is 'o')
                    break;

                foreach (var (isFixed, isFallInfinity) in MoveSand(grid, sp, maxRockY))
                {
                    if (isFallInfinity is true)
                        yield break;

                    yield return isFixed;
                }
            }

            static IEnumerable<(bool IsFixed, bool IsFallInfinity)> MoveSand(Grid grid, (int X, int Y) sp, int maxRockY)
            {
                while (true)
                {
                    if (sp.Y + 1 > maxRockY)
                    {
                        yield return (false, true);
                        break;
                    }

                    if (grid[sp.X, sp.Y + 1] is '.')
                        sp = (sp.X, sp.Y + 1);
                    else if (grid[sp.X - 1, sp.Y + 1] is '.')
                        sp = (sp.X - 1, sp.Y + 1);
                    else if (grid[sp.X + 1, sp.Y + 1] is '.')
                        sp = (sp.X + 1, sp.Y + 1);
                    else
                    {
                        grid[sp.X, sp.Y] = 'o';
                        
                        yield return (true, false);
                        break;
                    }

                    yield return (false, false);
                }
            }
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

            public IEnumerable<(int X, int Y, char C)> GetAllPotisions(bool withBorder = false)
            {
                var result = _grid.Select(x => (x.Key.X, x.Key.Y, x.Value));
                if (withBorder is true)
                {
                    if (Bottom.C is not '\0')
                        result = result.Append((-1, Bottom.Y, Bottom.C));
                }

                return result;
            }

            public void DrawLine(int x1, int y1, int x2, int y2, char drawChar)
            {
                // 수직 그리기
                if (x1 == x2)
                {
                    if (y1 > y2)
                        (y1, y2) = (y2, y1);
                    foreach (var y in Enumerable.Range(y1, y2 - y1 + 1))
                        this[x1, y] = drawChar;
                    
                }
                // 수평 그리기
                else
                {
                    if (x1 > x2)
                        (x1, x2) = (x2, x1);
                    foreach (var x in Enumerable.Range(x1, x2 - x1 + 1))
                        this[x, y1] = drawChar;
                }
            }

            public override string ToString()
            {
                var sb = new StringBuilder();

                var positions = GetAllPotisions().Where(x => x.C != _clearChar);
                var left = positions.Min(x => x.X) - _padding;
                var right = positions.Max(x => x.X) + _padding;
                var bottom = positions.Max(x => x.Y) + _padding;
                var width = right - left + 1;


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

                printRowNum(xNum => xNum.ToString()[0]);
                printRowNum(xNum => xNum.ToString()[1]);
                printRowNum(xNum => xNum.ToString()[2]);

                foreach (var i in Enumerable.Range(0, width * (bottom + 1)))
                {
                    var c = this[i % width + left, i / width];

                    if (i % width is 0)
                        sb.Append($"{i / width, 3} ");

                    sb.Append(c);
                    
                    if (i % width == (width - 1))
                        sb.AppendLine();
                }

                return sb.ToString();
            }
        }
    }
}
