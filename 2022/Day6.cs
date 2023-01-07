using SuperLinq;

namespace _2022;


public class Day6 : ISolve
{
    public static string Solve1(string input, params object[] args)
    {
        for (var i = 0; i < input.Length; i++)
        {
            // 총 4개의 문자가 준비될 때까지 처리하지 않음
            if (i < 4)
                continue;

            var packet = input[(i - 4)..i];
            if (IsMarker(packet) is true)
                return i.ToString();
        }

        return (-1).ToString();

        bool IsMarker(string packet)
        {
            return packet.Distinct().SequenceEqual(packet);
        }
    }

    public static string Solve2(string input, params object[] args)
    {
        for (var i = 0; i < input.Length; i++)
        {
            // 총 14개의 문자가 준비될 때까지 처리하지 않음
            if (i < 14)
                continue;

            var packet = input[(i - 14)..i];
            if (IsMarker(packet) is true)
                return i.ToString();
        }

        return (-1).ToString();

        bool IsMarker(string packet)
        {
            return packet.Distinct().SequenceEqual(packet);
        }
    }

    public static string Solve1_LINQ(string input, params object[] args)
    {
        var markerSize = 4;
        var result = input.Window(markerSize)
            .TakeUntil(x => x.ToHashSet().Count == markerSize)
            .Count() + markerSize - 1;

        return result.ToString();
    }
}
