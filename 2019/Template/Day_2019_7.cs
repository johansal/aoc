using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Template
{
    public class Day_2019_7
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
            IEnumerable<IEnumerable<int>> phases = GetPermutations(Enumerable.Range(0, 5), 5);
            int maxThrusterSignal = 0;
            foreach (IEnumerable<int> phase in phases)
            {
                int amplifierOutput = 0;
                foreach (int i in phase)
                {
                    int[] intcode = input.Split(',').Select(Int32.Parse).ToArray();
                    List<int> inputs = new List<int>();
                    inputs.Add(i);
                    inputs.Add(amplifierOutput);
                    amplifierOutput = amplifier(intcode, inputs);
                }
                if (amplifierOutput > maxThrusterSignal)
                    maxThrusterSignal = amplifierOutput;
            }
            return maxThrusterSignal.ToString(); //21000
        }

        public static string secondPuzzle(string input)
        {
            IEnumerable<IEnumerable<int>> phases = GetPermutations(Enumerable.Range(5, 5), 5);
            long maxThrusterSignal = 0;
            foreach (IEnumerable<int> phase in phases)
            {

                long amplifierOutput = 0;
                long[] intcode = input.Split(',').Select(long.Parse).ToArray();
                List<IntComputer> amplifiers = new List<IntComputer>();
                amplifiers.Add(new IntComputer(intcode));
                amplifiers.Add(new IntComputer(intcode));
                amplifiers.Add(new IntComputer(intcode));
                amplifiers.Add(new IntComputer(intcode));
                amplifiers.Add(new IntComputer(intcode));

                for (int i = 0; i < amplifiers.Count; i++)
                {
                    amplifiers.ElementAt(i).inputs.Add(phase.ElementAt(i));
                    amplifiers.ElementAt(i).inputs.Add(amplifierOutput);
                    if (amplifiers.ElementAt(i).compute())
                    {
                        amplifierOutput = amplifiers.ElementAt(i).outputs.ElementAt(0);
                        amplifiers.ElementAt(i).outputs.RemoveAt(0);
                    }
                    else
                        Console.WriteLine("Something went wrong");
                }
                while (!amplifiers.ElementAt(0).isHalted)
                {
                    for (int i = 0; i < amplifiers.Count; i++)
                    {
                        amplifiers.ElementAt(i).inputs.Add(amplifierOutput);
                        if (amplifiers.ElementAt(i).compute())
                        {

                            amplifierOutput = amplifiers.ElementAt(i).outputs.ElementAt(0);
                            amplifiers.ElementAt(i).outputs.RemoveAt(0);
                        }
                        else
                            Console.WriteLine("Something went wrong");
                    }
                }
                if(!amplifiers.ElementAt(0).isHalted || !amplifiers.ElementAt(1).isHalted || !amplifiers.ElementAt(2).isHalted || !amplifiers.ElementAt(3).isHalted || !amplifiers.ElementAt(4).isHalted)
                    Console.WriteLine("All amps are not halted!");
                if (amplifierOutput > maxThrusterSignal)
                    maxThrusterSignal = amplifierOutput;
            }
            return maxThrusterSignal.ToString();
        }
        private static int amplifier(int[] intCodes, List<int> input)
        {
            List<int> output = intcodeProgram(intCodes, input);
            return output.First();
        }
        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
        private static List<int> intcodeProgram(int[] intCodes, List<int> input)
        {
            List<int> outputs = new List<int>();
            for (int i = 0; i < intCodes.Length; i++)
            {
                //Parse int code and parameter mode (0 position, 1 immediate)
                var digits = intCodes[i].ToString().Select(t => int.Parse(t.ToString())).ToArray();
                int intCode = digits.Length >= 2 ? digits[digits.Length - 2] * 10 + digits[digits.Length - 1] : digits[0];
                //Console.WriteLine(i + ":" + intCode + "(=" + intCodes[i] + ")");
                if (intCode == ADD)
                {
                    int param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? intCodes[i + 1] : intCodes[intCodes[i + 1]];
                    int param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? intCodes[i + 2] : intCodes[intCodes[i + 2]];
                    intCodes[intCodes[i + 3]] = param1 + param2;
                    i += 3;
                }
                else if (intCode == MULTIPLY)
                {
                    int param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? intCodes[i + 1] : intCodes[intCodes[i + 1]];
                    int param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? intCodes[i + 2] : intCodes[intCodes[i + 2]];
                    intCodes[intCodes[i + 3]] = param1 * param2;
                    i += 3;
                }
                else if (intCode == INPUT)
                {
                    intCodes[intCodes[i + 1]] = input.First();
                    input.RemoveAt(0);
                    i++;
                }
                else if (intCode == OUTPUT)
                {
                    outputs.Add(intCodes[intCodes[i + 1]]);
                    i++;
                }
                else if (intCode == JUMP_IF_T)
                {
                    int param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? intCodes[i + 1] : intCodes[intCodes[i + 1]];
                    int param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? intCodes[i + 2] : intCodes[intCodes[i + 2]];
                    if (param1 != 0)
                        i = param2 - 1;
                    else
                        i += 2;
                }
                else if (intCode == JUMP_IF_F)
                {
                    int param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? intCodes[i + 1] : intCodes[intCodes[i + 1]];
                    int param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? intCodes[i + 2] : intCodes[intCodes[i + 2]];
                    if (param1 == 0)
                        i = param2 - 1;
                    else
                        i += 2;
                }
                else if (intCode == LESS_THAN)
                {
                    int param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? intCodes[i + 1] : intCodes[intCodes[i + 1]];
                    int param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? intCodes[i + 2] : intCodes[intCodes[i + 2]];
                    intCodes[intCodes[i + 3]] = param1 < param2 ? 1 : 0;
                    i += 3;
                }
                else if (intCode == EQUALS)
                {
                    int param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? intCodes[i + 1] : intCodes[intCodes[i + 1]];
                    int param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? intCodes[i + 2] : intCodes[intCodes[i + 2]];
                    intCodes[intCodes[i + 3]] = param1 == param2 ? 1 : 0;
                    i += 3;
                }
                else if (intCode == STOP)
                {
                    return outputs;
                }
                else
                {
                    throw new Exception("Unsupported opcode (" + intCodes[i - 1] + ")" + intCodes[i].ToString() + " at " + i);
                }
            }
            return outputs;
        }
    }
}
