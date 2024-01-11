using _2023;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023;

internal class _2023Test
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase("""
                1abc2
                pqr3stu8vwx
                a1b2c3d4e5f
                treb7uchet
                """)]
    public void Day1_Test1(string input)
    {
        var result = Day01.Solve1(input);
        Assert.That(result, Is.EqualTo("142"));
    }

    [TestCase("""
                two1nine
                eightwothree
                abcone2threexyz
                xtwone3four
                4nineeightseven2
                zoneight234
                7pqrstsixteen
                """)]
    public void Day1_Test2(string input)
    {
        var result = Day01.Solve2(input);
        Assert.That(result, Is.EqualTo("281"));
    }

    [TestCase("""
                Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
                Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
                Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
                Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
                """)]
    public void Day2_Test1(string input)
    {
        var result = Day02.Solve1(input);
        Assert.That(result, Is.EqualTo("8"));
    }

    [TestCase("""
                Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
                Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
                Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
                Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
                """)]
    public void Day2_Test2(string input)
    {
        var result = Day02.Solve2(input);
        Assert.That(result, Is.EqualTo("2286"));
    }

    [TestCase("""
                467..114..
                ...*......
                ..35..633.
                ......#...
                617*......
                .....+.58.
                ..592.....
                ......755.
                ...$.*....
                .664.598..
                """)]
    public void Day3_Test1(string input)
    {
        var result = Day03.Solve1(input);
        Assert.That(result, Is.EqualTo("4361"));
    }
}
