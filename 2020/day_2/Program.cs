using System;
using System.IO;
using System.Text;

namespace day_2
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var inputLines = File.ReadAllLines("input.txt", Encoding.UTF8);
            int validCountPart1 = 0;
            int validCountPart2 = 0;
            for (var i = 0; i < inputLines.Length; i++)
            {
                //Parse policy and password
                var temp = inputLines[i].Split(": ");
                string password = temp[1];
                temp = temp[0].Split(" ");
                char c = temp[1][0];
                temp = temp[0].Split("-");
                int min = Int32.Parse(temp[0]);
                int max = Int32.Parse(temp[1]);
                if(Test(password, c, min, max))
                    validCountPart1++;
                if(Test2(password, c, min, max))
                    validCountPart2++;
            }
            Console.WriteLine("Part1:");
            Console.WriteLine(validCountPart1);
            Console.WriteLine("Part2:");
            Console.WriteLine(validCountPart2);
        }
        private static bool Test(string s, char c, int min, int max) {
            int count = 0;
            for(var i = 0; i < s.Length; i++) {
                if(s[i] == c)
                    count++;
            }
            return count >= min && count <= max;
        }
        private static bool Test2(string s, char c, int first, int second) {
            return s[first-1] == c ^ s[second-1] == c;
        }
    }
}
