using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022
{
    public class Day12 : ISolve
    {
        public static string Solve1(string input)
        {
            var mountain = Mountain.Parse(input, false);

            var directions = new (int X, int Y, char Mark)[] { (1, 0, '>'), (0, 1, 'v'), (0, -1, '^'), (-1, 0, '<') };
            var result = MoveTrail(mountain, mountain.StartPos, directions);
            if (result is null)
                throw new InvalidOperationException();

            Console.WriteLine(result.M);

            return result.GetAllMarkPositions()
                .Count(c => c is '^' or '>' or 'v' or '<')
                .ToString();

            static Mountain? MoveTrail(Mountain mountain, (int X, int Y) cp, (int X, int Y, char Mark)[] directions)
            {
                var visited = new Dictionary<(int X, int Y), bool>
                {
                    { cp, true }
                };
                var stack = new Queue<(Mountain mountain, (int X, int Y) cp)>();
                stack.Enqueue((mountain, cp));

                while (stack.Count > 0)
                {
                    var count = stack.Count;
                    foreach (var _ in Enumerable.Range(1, count))
                    {
                        (mountain, cp) = stack.Dequeue();

                        foreach (var direction in directions)
                        {
                            var bResult = mountain.FindTrail(cp, (direction.X, direction.Y));
                            (int X, int Y) mv = (cp.X + direction.X, cp.Y + direction.Y);
                            if (bResult is true && visited.ContainsKey(mv) is false)
                            {
                                var newMountain = new Mountain(mountain);
                                newMountain.M[cp.X, cp.Y] = direction.Mark;

                                if (newMountain[mv.X, mv.Y] == newMountain.EndMark)
                                    return newMountain;

                                visited[mv] = true;
                                stack.Enqueue((newMountain, mv));
                            }
                        }
                    }
                }

                return null;
            }
        }

        public static string Solve2(string input)
        {
            var mountain = Mountain.Parse(input, true);

            var directions = new (int X, int Y, char Mark)[] { (1, 0, '>'), (0, 1, 'v'), (0, -1, '^'), (-1, 0, '<') };

            var result = MoveTrail(mountain, mountain.StartPos, directions);
            if (result is null)
                throw new InvalidOperationException();

            Console.WriteLine(result.M);

            return result.GetAllMarkPositions()
                .Count(c => c is '^' or '>' or 'v' or '<')
                .ToString();

            static Mountain? MoveTrail(Mountain mountain, (int X, int Y) cp, (int X, int Y, char Mark)[] directions)
            {
                var visited = new Dictionary<(int X, int Y), bool>
                {
                    { cp, true }
                };
                var stack = new Queue<(Mountain mountain, (int X, int Y) cp)>();
                stack.Enqueue((mountain, cp));

                while (stack.Count > 0)
                {
                    var count = stack.Count;
                    foreach (var _ in Enumerable.Range(1, count))
                    {
                        (mountain, cp) = stack.Dequeue();

                        foreach (var direction in directions)
                        {
                            var bResult = mountain.FindTrail(cp, (direction.X, direction.Y));
                            (int X, int Y) mv = (cp.X + direction.X, cp.Y + direction.Y);
                            if (bResult is true && visited.ContainsKey(mv) is false)
                            {
                                var newMountain = new Mountain(mountain);
                                newMountain.M[cp.X, cp.Y] = direction.Mark;

                                if (newMountain[mv.X, mv.Y] == newMountain.EndMark)
                                    return newMountain;

                                visited[mv] = true;
                                stack.Enqueue((newMountain, mv));
                            }
                        }
                    }
                }

                return null;
            }
        }

        class Mountain
        {
            private readonly string _grid;
            private readonly Mark _mark;
            private char _endMark = 'E';
            private bool _isReverse;

            public char EndMark => _endMark;

            public int Width { get; }
            public int Height { get; }

            public int Left => 0;
            public int Top => 0;
            public int Right => Width - 1;
            public int Bottom => Height - 1;

            public (int X, int Y) StartPos { get; }
            public (int X , int Y) EndPos { get; }
            


            private Mountain(string input, bool isReverse)
            {
                Height = input.Count(x => x is '\n') + 1;
                _grid = input.Replace(Environment.NewLine, "");
                Width = _grid.Length / Height;
                _mark = new Mark(this);

                var startPosition = _grid.IndexOf('S');
                StartPos = (startPosition % Width, startPosition / Width);
                var endPosition = _grid.IndexOf('E');
                EndPos = (endPosition % Width, endPosition / Width);

                _mark[StartPos.X, StartPos.Y] = 'S';
                _mark[EndPos.X, EndPos.Y] = 'E';

                _isReverse = isReverse;
                if (isReverse is true)
                {
                    _endMark = 'a';
                    StartPos = EndPos;
                }
            }

            public Mountain(Mountain grid)
            {
                Width = grid.Width;
                Height = grid.Height;
                _grid = grid._grid;
                _mark = new Mark(grid._mark);
                _endMark = grid._endMark;
                _isReverse = grid._isReverse;
            }

            public Mark M => _mark;

            public char this[int x, int y]
            {
                get
                {
                    if (x < Left || x > Right || y < Top || y > Bottom)
                        return '#';

                    return _grid[y * Width + x];
                }
            }

            public IEnumerable<char> GetAllMarkPositions() => M.GetAllPositions();


            public bool FindTrail((int X, int Y) cp, (int X, int Y) tp)
            {
                var currentTrail = this[cp.X, cp.Y];
                var findTrail = currentTrail switch
                {
                    'E' => 'z',
                    'S' => 'a',
                    'z' => 'E',
                    _ => _isReverse is false ? (char)(currentTrail + 1) : (char)(currentTrail - 1),
                };
                (tp.X, tp.Y) = (cp.X + tp.X, cp.Y + tp.Y);
                var nextTrail = this[tp.X, tp.Y];

                if (nextTrail is '#')
                    return false;

                if (M[tp.X, tp.Y] is not '.' && M[tp.X, tp.Y] != _endMark)
                    return false;

                if (_isReverse is false)
                {
                    if (nextTrail == findTrail || (nextTrail != _endMark && nextTrail <= currentTrail))
                        return true;
                }
                else
                {
                    if (nextTrail == findTrail || (nextTrail != _endMark && nextTrail >= currentTrail))
                        return true;
                }

                return false;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                foreach (var (c, i) in _grid.Select((c, i) => (c, i)))
                {
                    sb.Append(c);
                    if ((i + 1) % Width is 0)
                        sb.AppendLine();
                }
                
                return sb.ToString();
            }

            public static Mountain Parse(string input, bool isReverse) => new(input, isReverse);

            public class Mark
            {
                private readonly char[] _mark;
                private readonly int _width;

                public Mark(Mountain mountain)
                {
                    _width = mountain.Width;
                    _mark = Enumerable.Repeat('.', mountain._grid.Length).ToArray();
                }

                public Mark(Mark mark)
                {
                    _width = mark._width;
                    _mark = mark._mark.ToArray();
                }

                public char this[int x, int y]
                {
                    get => _mark[y * _width + x];
                    set => _mark[y * _width + x] = value;
                }

                public IEnumerable<char> GetAllPositions() => _mark;

                public override string ToString()
                {
                    var sb = new StringBuilder();
                    foreach (var (c, i) in _mark.Select((c, i) => (c, i)))
                    {
                        sb.Append(c);
                        if ((i + 1) % _width is 0)
                            sb.AppendLine();
                    }

                    return sb.ToString();
                }
            }
        }
    }
}
