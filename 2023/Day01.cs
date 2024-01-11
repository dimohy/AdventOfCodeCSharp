using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _2023;

public sealed class Day01 : ISolve
{
    public static string Solve1(string input, params object[] args)
    {
        var sum = 0;
        var lines = input.Split(Environment.NewLine);
        foreach (var line in lines)
        {
            var onlyNumbers = line.Where(x => char.IsNumber(x));
            var fN = onlyNumbers.First();
            var lN = onlyNumbers.Last();
            var num = int.Parse($"{fN}{lN}");
            sum += num;
        }

        return sum.ToString();
    }

    public static string Solve2(string input, params object[] args)
    {
        var sum = 0;
        var lines = input.Split(Environment.NewLine);
        foreach (var line in lines)
        {
            var fN = GetNumers(line, false).First();
            var lN = GetNumers(line, true).First();
            var num = int.Parse($"{fN}{lN}");
            sum += num;
        }

        return sum.ToString();

        static IEnumerable<char> GetNumers(string input, bool isReverse)
        {
            Func<string, string, bool> checkFunc = isReverse is false ? (string text, string s) => text.StartsWith(s) : (string text, string s) => text.EndsWith(s);
            Func<int, string> getTextFunc = isReverse is false ? (int index) => input[index..] : (int index) => input[..^index];
            Func<int, char> getNumFunc = isReverse is false ? index => input[index] : index => input[^(index + 1)]; 

            for (var i = 0; i < input.Length; i++)
            {
                var c = getNumFunc(i);
                if (c >= '1' && c <= '9')
                {
                    yield return c;
                    continue;
                }

                var text = getTextFunc(i);
                if (checkFunc(text, "one"))
                {
                    yield return '1';
                    i += 2;
                }
                else if (checkFunc(text, "two"))
                {
                    yield return '2';
                    i += 2;
                }
                else if (checkFunc(text, "three"))
                {
                    yield return '3';
                    i += 4;
                }
                else if (checkFunc(text, "four"))
                {
                    yield return '4';
                    i += 3;
                }
                else if (checkFunc(text, "five"))
                {
                    yield return '5';
                    i += 3;
                }
                else if (checkFunc(text, "six"))
                {
                    yield return '6';
                    i += 2;
                }
                else if (checkFunc(text, "seven"))
                {
                    yield return '7';
                    i += 4;
                }
                else if (checkFunc(text, "eight"))
                {
                    yield return '8';
                    i += 4;
                }
                else if (checkFunc(text, "nine"))
                {
                    yield return '9';
                    i += 3;
                }
                else if (checkFunc(text, "zero"))
                {
                    yield return '0';
                    i += 3;
                }
            }
        }
    }
}
