using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022
{
    public interface ISolve
    {
        static abstract string Solve1(string input, params object[] args);
        static abstract string Solve2(string input, params object[] args);
    }
}
