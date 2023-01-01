using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022;

public class Day4
{
    public static void Solve1()
    {
        var lines = File.ReadAllLines("day4_input.txt");
        //var lines = new[]
        //{
        //    "2-4,6-8",
        //    "2-3,4-5",
        //    "5-7,7-9",
        //    "2-8,3-7",
        //    "6-6,4-6",
        //    "2-6,4-8",
        //};

        var count = lines.Select(x => x.Split(','))
            .Select(x => new
            {
                A = x[0].Split('-').Select(y => int.Parse(y)).ToArray(),
                B = x[1].Split('-').Select(y => int.Parse(y)).ToArray()
            })
            .Count(x => IsInclude(x.A, x.B));

        Console.WriteLine(count);


        static bool IsInclude(int[] r1, int[] r2) => (r1, r2) switch
        {
            _ when r1[0] <= r2[0] && r1[1] >= r2[1] => true,
            _ when r2[0] <= r1[0] && r2[1] >= r1[1] => true,
            _ => false
        };
    }

    public static void Solve2()
    {
        var lines = File.ReadAllLines("day4_input.txt");
        //var lines = new[]
        //{
        //    "2-4,6-8",
        //    "2-3,4-5",
        //    "5-7,7-9",
        //    "2-8,3-7",
        //    "6-6,4-6",
        //    "2-6,4-8",
        //};

        var count = lines.Select(x => x.Split(','))
            .Select(x => new
            {
                A = x[0].Split('-').Select(y => int.Parse(y)).ToArray(),
                B = x[1].Split('-').Select(y => int.Parse(y)).ToArray()
            })
            .Count(x => IsOverlap(x.A, x.B));

        Console.WriteLine(count);


        static bool IsOverlap(int[] r1, int[] r2)
        {
            var r1List = Enumerable.Range(r1[0], r1[1] - r1[0] + 1);
            var r2List = Enumerable.Range(r2[0], r2[1] - r2[0] + 1);

            return r1List.Intersect(r2List).Any();
        }    
    }
}
