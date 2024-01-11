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
    public void Day3_Test2(string input)
    {
        var result = Day03.Solve2(input);
        Assert.That(result, Is.EqualTo("467835"));
    }

    [TestCase("""
                Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
                Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
                Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
                Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
                Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
                Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
                """)]
    public void Day4_Test1(string input)
    {
        var result = Day04.Solve1(input);
        Assert.That(result, Is.EqualTo("13"));
    }

    [TestCase("""
                Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
                Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
                Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
                Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
                Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
                Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
                """)]
    public void Day4_Test2(string input)
    {
        var result = Day04.Solve2(input);
        Assert.That(result, Is.EqualTo("30"));
    }
}
