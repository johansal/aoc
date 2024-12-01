using System;
using System.IO;
using System.Text;

namespace day_8
{
    class Program
    {
        public static int accumulator = 0;
        public static int index = 0;
        public static void Main()
        {
            string[] lines = File.ReadAllLines("input.txt", Encoding.UTF8);
            for(var i = 0; i < lines.Length; i++) {
                string[] tryLines = new string[lines.Length];
                Array.Copy(lines, tryLines, lines.Length);
                if(lines[i].StartsWith("jmp"))
                    tryLines[i] = "nop " + lines[i].Split(" ")[1];
                else if(lines[i].StartsWith("nop"))
                    tryLines[i] = "jmp " + lines[i].Split(" ")[1];
                bool terminated = Run(tryLines);
                if(terminated) {
                    Console.WriteLine("Terminated! Accumulator: " + accumulator);
                    break;
                }
            }
        }
        public static void Acc(int x)
        {
            accumulator += x;
            index++;
        }
        public static void Jmp(int x)
        {
            index += x;
        }
        public static void Nop()
        {
            index++;
        }
        public static void ParseCmd(string cmd)
        {
            var cmdArr = cmd.Split(" ");
            switch (cmdArr[0])
            {
                case "acc":
                    Acc(int.Parse(cmdArr[1]));
                    break;
                case "jmp":
                    Jmp(int.Parse(cmdArr[1]));
                    break;
                case "nop":
                    Nop();
                    break;
            }
        }
        public static bool Run(string[] lines) {
            accumulator = 0;
            index = 0;
            while (index >= 0 && index < lines.Length && !string.IsNullOrEmpty(lines[index]))
            {
                var tmp = lines[index];
                lines[index] = "";
                ParseCmd(tmp);
            }
            return index == lines.Length;
        }
    }
}
