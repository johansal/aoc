using System;
using System.IO;
using System.Linq;

namespace day_11
{
    class Program
    {
        public static void Main(string[] args)
        {
            int counter = 0;
            var strIn = File.ReadAllLines("input.txt");
            var input = IntArray(strIn);
            bool allFlashed = false;
            var steps = 0;

            while (!allFlashed)
            {
                allFlashed = true;
                steps++;
                foreach (var line in input)
                {
                    for (var c = 0; c < line.Length; c++)
                    {
                        line[c]++;
                    }
                }
                for (var i = 0; i < input.Length; i++)
                {
                    for (var c = 0; c < input[i].Length; c++)
                    {
                        if (input[i][c] > 9)
                        {
                            input[i][c] = 0;
                            counter++;
                            Flash(ref input, ref counter, i, c);
                        }
                    }
                }
                if(steps == 100) Console.WriteLine(counter);
                foreach (var line in input)
                {
                    for (var c = 0; c < line.Length; c++)
                    {
                        if(line[c] != 0) {
                            allFlashed = false;
                            break;
                        }
                    }
                    if(!allFlashed) break;
                }
            }   
            Console.WriteLine("All flashed: " + steps);
        }
        public static void Flash(ref int[][] arr, ref int counter, int i, int j)
        {
            for (var a = i - 1; a <= i + 1; a++)
            {
                for (var b = j - 1; b <= j + 1; b++)
                {
                    if (a >= 0 && a < arr.Length && b >= 0 && b < arr[a].Length)
                    {
                        if (arr[a][b] > 0)
                        {
                            arr[a][b]++;
                            if (arr[a][b] > 9)
                            {
                                arr[a][b] = 0;
                                counter++;
                                Flash(ref arr, ref counter, a, b);
                            }
                        }
                    }
                }
            }
        }
        public static int[][] IntArray(string[] arr)
        {
            int[][] ret = new int[arr.Length][];
            for (var line = 0; line < arr.Length; line++)
            {
                int[] l = new int[arr[line].Length];
                ret[line] = l;
                for (var c = 0; c < arr[line].Length; c++)
                {
                    string r = arr[line][c].ToString();
                    //Console.Write(r);
                    ret[line][c] = int.Parse(r);
                }
                //Console.WriteLine();
            }
            return ret;
        }
    }
}
