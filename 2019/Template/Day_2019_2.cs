using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Template
{
    public class Day_2019_2
    {
        const int ADD = 1;
        const int MULTIPLY = 2;
        const int STOP = 99;
        public static string firstPuzzle(string input)
        {

            int[] intcode = input.Split(',').Select(Int32.Parse).ToArray();
            intcode[1] = 12;
            intcode[2] = 2;
            int[] output = intcodeProgram(intcode);

            return output[0].ToString(); //3166704
        }

        public static string secondPuzzle(string input)
        {
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    int[] initMemory = input.Split(',').Select(Int32.Parse).ToArray();
                    initMemory[1] = noun;
                    initMemory[2] = verb;
                    int[] output = intcodeProgram(initMemory);
                    if (output[0] == 19690720)
                    {
                        //Console.WriteLine("debug::found " + noun.ToString() + " & " + verb.ToString());
                        return (100 *noun + verb).ToString();
                    }
                }
            }
            Console.WriteLine("Didn't find correct answer!");
            return "-999";
        }

        private static int[] intcodeProgram(int[] intCodes)
        {
            for (int i = 0; i < intCodes.Length; i += 4)
            {
                //Console.WriteLine("debug::" + string.Join(",", intCodes));
                if (intCodes[i] == ADD)
                {
                    intCodes[intCodes[i + 3]] = intCodes[intCodes[i + 1]] + intCodes[intCodes[i + 2]];
                }
                else if (intCodes[i] == MULTIPLY)
                {
                    intCodes[intCodes[i + 3]] = intCodes[intCodes[i + 1]] * intCodes[intCodes[i + 2]];
                }
                else if (intCodes[i] == STOP)
                {
                    return intCodes;
                }
                else
                {
                    throw new Exception("Unsupported opcode " + intCodes[i].ToString());
                }
            }
            return intCodes;
        }
    }
}
