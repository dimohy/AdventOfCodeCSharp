using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace _2023;

public class Day02 : ISolve
{
    public static string Solve1(string input, params object[] args)
    {
        var maxRgbCubes = (Red: 12, Green: 13, Blue: 14);

        var lines = input.Split(Environment.NewLine);
        var sumNumbers = 0;
        foreach (var line in lines)
        {
            var temp = line.Split(":");
            var gameNumbers = int.Parse(temp[0].Split(" ")[1]);
            var groups = temp[1].Split(";");
            var bBad = false;
            foreach (var group in groups.Select(x => x.Trim()))
            {
                var cubes = group.Split(",");
                var (rCount, gCount, bCount) = (0, 0, 0);
                foreach (var cube in cubes.Select(x => x.Trim()))
                {
                    if (cube.EndsWith("red") is true)
                        rCount = int.Parse(cube.Split(" ")[0]);
                    else if (cube.EndsWith("green") is true)
                        gCount = int.Parse(cube.Split(" ")[0]);
                    else if (cube.EndsWith("blue") is true)
                        bCount = int.Parse(cube.Split(" ")[0]);
                }

                if (rCount > maxRgbCubes.Red || gCount > maxRgbCubes.Green || bCount > maxRgbCubes.Blue)
                {
                    bBad = true;
                    break;
                }
            }

            if (bBad is false)
                sumNumbers += gameNumbers;
        }

        return sumNumbers.ToString();
    }

    public static string Solve2(string input, params object[] args)
    {
        var lines = input.Split(Environment.NewLine);
        var sumNumbers = 0;
        foreach (var line in lines)
        {
            var temp = line.Split(":");
            var gameNumbers = int.Parse(temp[0].Split(" ")[1]);
            var groups = temp[1].Split(";");
            var (maxRCount, maxGCount, maxBCount) = (0, 0, 0);
            foreach (var group in groups.Select(x => x.Trim()))
            {
                var cubes = group.Split(",");
                foreach (var cube in cubes.Select(x => x.Trim()))
                {
                    var count = int.Parse(cube.Split(" ")[0]);
                    if (cube.EndsWith("red") is true && count > maxRCount)
                        maxRCount = count;
                    else if (cube.EndsWith("green") is true && count > maxGCount)
                        maxGCount = count;
                    else if (cube.EndsWith("blue") is true && count > maxBCount)
                        maxBCount = count;
                }
            }
            sumNumbers += maxRCount * maxGCount * maxBCount;
        }

        return sumNumbers.ToString();
    }
}
