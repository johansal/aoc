using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace day_18
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt", Encoding.UTF8);
            long part1 = 0;
            for (var i = 0; i < lines.Length; i++)
            {
                var tmp = ParseParenthesis(lines[i], 0);
                //Console.WriteLine("Parsed lines: " + tmp.Item2);
                part1 += long.Parse(tmp.Item2);
            }
            Console.WriteLine("Part 2: " + part1);
        }
        public static (int, string) ParseParenthesis(string s, int i)
        {
            string result = "";
            while (i < s.Length)
            {
                if (s[i] == '(')
                {
                    var tmp = ParseParenthesis(s, i + 1);
                    i = tmp.Item1;
                    result += tmp.Item2;
                }
                else if (s[i] == ')')
                {
                    result = CalculateString(result);
                    return (i, result);
                }
                else
                {
                    result += s[i];
                }
                i++;
            }
            result = CalculateString(result);
            return (i, result);
        }
        public static string CalculateString(string s)
        {
            List<string> operands = new List<string>(s.Split(" "));
            long tmp = int.Parse(operands[0]);
            while (operands.Count > 2)
            {
                if (operands.Contains("+"))
                {
                    var i = operands.FindIndex(0, operands.Count, x => x.Equals("+"));
                    operands[i] = (long.Parse(operands[i - 1]) + long.Parse(operands[i + 1])).ToString();
                    operands.RemoveAt(i+1);
                    operands.RemoveAt(i-1);
                }
                else if (operands.Contains("*"))
                {
                    var i = operands.FindIndex(0, operands.Count, x => x.Equals("*"));
                    operands[i] = (long.Parse(operands[i - 1]) * long.Parse(operands[i + 1])).ToString();
                    operands.RemoveAt(i+1);
                    operands.RemoveAt(i-1);
                }
            }
            //Console.WriteLine(s + " calculated: " + operands[0]);
            return operands[0];
        }
    }
}
