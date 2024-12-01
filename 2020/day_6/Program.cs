using System;
using System.IO;
using System.Text;

namespace day_6
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt", Encoding.UTF8);
            Console.WriteLine("Part 1:" + Part1(lines));
            Console.WriteLine("Part 2:" + Part2(lines));
        }

        static int Part1(string[] lines)
        {
            int count = 0;
            string answers = "";
            for (var i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    count += answers.Length;
                    answers = "";
                }
                else
                {
                    for (var j = 0; j < lines[i].Length; j++)
                    {
                        if (!answers.Contains(lines[i][j]))
                            answers += lines[i][j];
                    }
                }
            }
            if (!string.IsNullOrEmpty(answers))
            {
                count += answers.Length;
                answers = "";
            }
            return count;
        }

        static int Part2(string[] lines)
        {
            int count = 0;
            string answers = lines[0];
            for (var i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    //Console.WriteLine("Group: " + answers);
                    count += answers.Length;
                    answers = lines[i+1];
                }
                else
                {
                    
                        string tmp = "";
                        for (var j = 0; j < answers.Length; j++)
                        {
                            if (lines[i].Contains(answers[j]))
                                tmp += answers[j];
                        }
                        answers = tmp;
                    
                }
            }
            if (!string.IsNullOrEmpty(answers))
            {
                count += answers.Length;
                answers = "";
            }
            return count;
        }
    }
}
