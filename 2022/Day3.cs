using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022;

public class Day3
{
    public static void Solve1()
    {
        var lines = File.ReadAllLines("day3_input.txt");
        //var lines = new[]
        //{
        //    "vJrwpWtwJgWrhcsFMMfFFhFp",
        //    "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
        //    "PmmdzqPrVvPwwTWBwg",
        //    "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
        //    "ttgJtRGJQctTZtZT",
        //    "CrZsJsPPZsGzwwsLwLmpwMDw",
        //};
        var sum = 0;
        foreach (var line in lines)
        {
            var rucksackSize = line.Length / 2;
            var aList = line[..rucksackSize];
            var bList = line[rucksackSize..];

            //var result = from a in aList
            //             join b in bList
            //             on a equals b
            //             select a;
            //var c = result.FirstOrDefault();
            //var c = aList
            //        .Join(bList, x => x, y => y, (x, y) => x)
            //        .FirstOrDefault();
            var c = aList
                .Intersect(bList)
                .FirstOrDefault();
            var priority = GetPriorityNumber(c);
            sum += priority;
        }

        Console.WriteLine(sum);

        static int GetPriorityNumber(char c) => c switch
        {
            >= 'a' and <= 'z' => c - 'a' + 1,
            >= 'A' and <= 'Z' => c - 'A' + 27,
            _ => 0
        };
    }

    public static void Solve2()
    {
        var lines = File.ReadAllLines("day3_input.txt");
        //var lines = new[]
        //{
        //    "vJrwpWtwJgWrhcsFMMfFFhFp",
        //    "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
        //    "PmmdzqPrVvPwwTWBwg",
        //    "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
        //    "ttgJtRGJQctTZtZT",
        //    "CrZsJsPPZsGzwwsLwLmpwMDw",
        //};

        var sum = lines.Chunk(3)
            .Select(x => x[0].Intersect(x[1]).Intersect(x[2]).FirstOrDefault())
            .Sum(x => GetPriorityNumber(x));

        Console.WriteLine(sum);


        static int GetPriorityNumber(char c) => c switch
        {
            >= 'a' and <= 'z' => c - 'a' + 1,
            >= 'A' and <= 'Z' => c - 'A' + 27,
            _ => 0
        };
    }
}
