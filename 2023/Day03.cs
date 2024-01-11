namespace _2023;

public class Day03 : ISolve
{
    public static string Solve1(string input, params object[] args)
    {
        // 심볼을 중심으로 주위 숫자를 얻어와 중복된 숫자를 제거한 후 합산한다.
        var lines = input.Split(Environment.NewLine);
        var npList = new List<Symbol>();
        foreach (var line in lines.Select((line, y) => (line, y)))
        {
            //Console.WriteLine(line);
            foreach (var item in GetSymbols(line.line, line.y))
                npList.Add(item);
        }

        var numbers = npList.Where(x => x.Number > 0).ToArray();
        var symbols = npList.Where(x => x.Number is 0).ToArray();

        var result = new List<Symbol>();
        foreach (var symbol in symbols)
        {
            foreach (var number in numbers)
            {
                if (symbol.IsAdjacent(number) is true)
                    result.Add(number);
            }
        }
        result = result.Distinct().ToList();
        //foreach (var number in result)
        //{
        //    Console.WriteLine(number);
        //}
        
        return result.Sum(x => x.Number).ToString();
    }

    static IEnumerable<Symbol> GetSymbols(string line, int y)
    {
        var sx = -1;
        for (var x = 0; x < line.Length; x++)
        {
            if (sx is -1 && char.IsNumber(line[x]) is true)
                sx = x;
            else if (sx >= 0 && char.IsNumber(line[x]) is false)
            {
                yield return new Symbol(sx, y, int.Parse(line[sx..x]));
                sx = -1;
            }

            if (sx >= 0 || line[x] is '.')
                continue;

            yield return new Symbol(x, y, 0, line[x]);
        }
        if (sx >= 0)
        {
            yield return new Symbol(sx, y, int.Parse(line[sx..]));
        }
    }

    public static string Solve2(string input, params object[] args)
    {
        var lines = input.Split(Environment.NewLine);
        var npList = new List<Symbol>();
        foreach (var line in lines.Select((line, y) => (line, y)))
        {
            foreach (var item in GetSymbols(line.line, line.y))
                npList.Add(item);
        }

        var gears = npList.Where(x => x.S is '*').ToArray();
        var numbers = npList.Where(x => x.Number > 0).ToArray();
        var sum = 0;
        foreach (var gear in gears)
        {
            var nums = numbers.Where(x => gear.IsAdjacent(x) is true).ToArray();
            if (nums.Length != 2)
                continue;

            sum += nums[0].Number * nums[1].Number;
        }

        return sum.ToString();
    }

    record Symbol(int X, int Y, int Number, char S = 'N')
    {
        public bool IsAdjacent(Symbol symbol)
        {
            if (Number > 0)
                return false;

            if (symbol.Number is 0)
                return false;

            var number = symbol.Number.ToString();

            (int, int)[] zone = [(-1, -1), (0, -1), (1, -1), (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0)];

            foreach (var (fx, fy) in zone)
            {
                if (Y + fy == symbol.Y)
                {
                    if (X + fx >= symbol.X && X + fx <= symbol.X + number.Length - 1)
                        return true;
                }
            }

            return false;
        }
    }
}