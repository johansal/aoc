using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Template
{
    public class Day_2019_5
    {
        const int ADD = 1;
        const int MULTIPLY = 2;
        const int INPUT = 3;
        const int OUTPUT = 4;
        const int JUMP_IF_T = 5;
        const int JUMP_IF_F = 6;
        const int LESS_THAN = 7;
        const int EQUALS = 8;
        const int STOP = 99;
        public static string firstPuzzle(string input)
        {
            int[] intcode = input.Split(',').Select(Int32.Parse).ToArray();
            List<int> output = intcodeProgram(intcode, 1);
            return string.Join(",", output);
        }

        public static string secondPuzzle(string input)
        {
            int[] intcode = input.Split(',').Select(Int32.Parse).ToArray();
            List<int> output = intcodeProgram(intcode, 5);
            return string.Join(",", output);
        }

        private static List<int> intcodeProgram(int[] intCodes, int input)
        {
            List<int> outputs = new List<int>();
            for (int i = 0; i < intCodes.Length; i++)
            {
                //Parse int code and parameter mode (0 position, 1 immediate)
                var digits = intCodes[i].ToString().Select(t => int.Parse(t.ToString())).ToArray();
                int intCode = digits.Length >= 2 ? digits[digits.Length-2] * 10 + digits[digits.Length-1] : digits[0];
                //Console.WriteLine(i + ":" + intCode + "(=" + intCodes[i] + ")");
                if (intCode == ADD)
                {
                    int param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? intCodes[i+1] : intCodes[intCodes[i + 1]];
                    int param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? intCodes[i+2] : intCodes[intCodes[i + 2]];
                    intCodes[intCodes[i + 3]] = param1 + param2;
                    i += 3;
                }
                else if (intCode == MULTIPLY)
                {
                    int param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? intCodes[i+1] : intCodes[intCodes[i + 1]];
                    int param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? intCodes[i+2] : intCodes[intCodes[i + 2]];
                    intCodes[intCodes[i + 3]] = param1 * param2;
                    i += 3;
                }
                else if (intCode == INPUT)
                {
                    intCodes[intCodes[i + 1]] = input;
                    i++;
                }
                else if (intCode == OUTPUT)
                {
                    outputs.Add(intCodes[intCodes[i + 1]]);
                    i++;
                }
                else if (intCode == JUMP_IF_T)
                {
                    int param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? intCodes[i+1] : intCodes[intCodes[i + 1]];
                    int param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? intCodes[i+2] : intCodes[intCodes[i + 2]];
                    if(param1 != 0)
                        i = param2-1;
                    else
                        i += 2;
                }
                else if (intCode == JUMP_IF_F)
                {
                    int param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? intCodes[i+1] : intCodes[intCodes[i + 1]];
                    int param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? intCodes[i+2] : intCodes[intCodes[i + 2]];
                    if(param1 == 0)
                        i = param2-1;
                    else
                        i += 2;
                }
                else if (intCode == LESS_THAN)
                {
                    int param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? intCodes[i+1] : intCodes[intCodes[i + 1]];
                    int param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? intCodes[i+2] : intCodes[intCodes[i + 2]];
                    intCodes[intCodes[i + 3]] = param1 < param2 ? 1 : 0;
                    i += 3;
                }
                else if (intCode == EQUALS)
                {
                    int param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? intCodes[i+1] : intCodes[intCodes[i + 1]];
                    int param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? intCodes[i+2] : intCodes[intCodes[i + 2]];
                    intCodes[intCodes[i + 3]] = param1 == param2 ? 1 : 0;
                    i += 3;
                }
                else if (intCode == STOP)
                {
                    return outputs;
                }
                else
                {
                    throw new Exception("Unsupported opcode (" + intCodes[i-1] + ")" + intCodes[i].ToString() + " at " + i);
                }
            }
            return outputs;
        }
    }
}
