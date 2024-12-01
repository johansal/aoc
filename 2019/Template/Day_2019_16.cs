using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Template
{
    public class Day_2019_16
    {
        public static string firstPuzzle(string input)
        {
            for (int i = 1; i <= 100; i++)
            {
                input = fftPhase(input);
            }
            return input.Substring(0, 8);
        }

        public static string secondPuzzle(string input)
        {
            int messageOffSet = 0;//int.Parse(input.Substring(0, 7));
            List<int> tmp = new List<int>();
            for (int i = 0; i < 10000; i++)
            {
                foreach (char c in input)
                {
                    tmp.Add((int)Char.GetNumericValue(c));
                }
            }
            Console.WriteLine(tmp.Count);
            for (int i = 1; i <= 100; i++)
            {
                tmp = fft2(tmp);
                //Console.WriteLine("Phase " + i + " completed..");
            }
            string output = "";
            for (int i = 0; i < 8; i++)
            {
                output += tmp.ElementAt(messageOffSet + i);
            }

            return output;
        }

        public static string fftPhase(string input)
        {
            int[] pattern = { 0, 1, 0, -1 };
            string output = "";

            for (int i = 1; i <= input.Length; i++)
            {
                int signal = 0;
                int offset = 0;

                for (int j = 1; j <= input.Length; j++)
                {

                    if (j % i == 0)
                        offset = offset == 3 ? 0 : offset + 1;
                    //Console.Write(offset + " (" + pattern[offset] + ") ");
                    //Console.Write(input[j-1].ToString() + "*" + pattern[offset] + " + ");
                    signal += int.Parse(input[j - 1].ToString()) * pattern[offset];
                }
                signal = Math.Abs(signal);
                //Console.Write("\n");
                //Console.Write("=" + signal + " (" + (signal % 10).ToString() + ")\n");
                output += (signal % 10).ToString();
            }

            return output;
        }

        public static List<int> fft2(List<int> input)
        {
            int[] pattern = { 0, 1, 0, -1 };
            List<int> output = new List<int>();

            for (int i = 1; i <= input.Count; i++)
            {
                int signal = 0;
                int j = 0;
                while (j < input.Count - 1)
                {
                    //partial sums
                    if (j == 0)
                    {
                        signal += input.Skip(j + i - 1).Take(i).Sum() - input.Skip(3 * i + j - 1).Take(i).Sum();
                        j += i * 4 - 1;
                    }
                    else
                    {
                        signal += input.Skip(j + i).Take(i).Sum() - input.Skip(3 * i + j).Take(i).Sum();
                        j += i * 4;
                    }
                }
                signal = Math.Abs(signal);
                output.Add(signal % 10);
            }

            return output;
        }

        /*
        The first observation is that on row N, we skip the first N-1 elements, 
        then sum the next N ones, skip N, subtract N-1, etc, until at the end of the row. 
        We don't need any multiplications, just sums.

        The second observation is that we can compute the partial sums of the input 
        once at the start of one of the 100 steps, 
        and then use cumulative_sum[k + n] - cumulative_sum[k] to compute the sum of elements 
        from k to k + n in a single operation.

        With that, the work for the last 2/3 lines needs a single operation 
        (since the last 2/3 lines only contain a single large block of ones, see picture). 
        The next 1/3 - 1/5 need 2 operations, the next 1/5 - 1/7 need 3, and so on.
        */
    }
}