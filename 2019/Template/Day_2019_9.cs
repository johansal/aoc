using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Template
{
    public class Day_2019_9
    {
        public static string firstPuzzle(string input)
        {

            long[] intcode = input.Split(',').Select(long.Parse).ToArray();
            IntComputer ic = new IntComputer(intcode);
            ic.inputs.Add((long)1);
            ic.compute();
            List<long> output = ic.outputs;
            return string.Join(',', output);
        }

        public static string secondPuzzle(string input)
        {
            long[] intcode = input.Split(',').Select(long.Parse).ToArray();
            IntComputer ic = new IntComputer(intcode);
            ic.inputs.Add((long)2);
            ic.compute();
            List<long> output = ic.outputs;
            return string.Join(',', output);
        }
    }
}
