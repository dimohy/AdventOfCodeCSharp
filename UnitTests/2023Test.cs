using _2023;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests;

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
}
