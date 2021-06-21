using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace Hackerrankweek4
{
    class Result
    {

        /*
        * Complete the 'staircase' function below.
        *
        * The function accepts INTEGER n as parameter.
        */
        public static void staircase(int n)
        {
            for(int i = 1; i <= n; i++)
            {
                string space = String.Concat(Enumerable.Repeat(" ", n - i));
                string sign = String.Concat(Enumerable.Repeat("#", i));
                Console.WriteLine(space + sign);
            }
        }
    }

    class Solution
    {
        public static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine().Trim());

            Result.staircase(n);
        }
    }
}
