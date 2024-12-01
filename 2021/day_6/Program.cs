using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day_6
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] fishes = { 0, 0, 0, 0, 0, 0, 0, 0, 0};
            var initial = File.ReadAllText("input.txt").Split(",");

            foreach (var state in initial)
            {
                fishes[int.Parse(state)]++;
            }
            for (int day = 1; day <= 256; day++)
            {
                long tmp = 0;
                for (int j = 0; j < 9; j++)
                {
                    //Console.Write(fishes[j] + " ");
                    if (j == 0)
                    {
                        tmp = fishes[j];
                        fishes[j] = fishes[j+1];
                    }
                    else if(j < 8){
                        fishes[j] = fishes[j+1];
                    }
                    else {
                        fishes[j] = 0;
                    }
                }
                //Console.Write("\n");
                fishes[6] += tmp;
                fishes[8] += tmp;
            }
            long counter = 0;
            foreach (var fish in fishes)
            {
                counter += fish;
            }
            Console.WriteLine(counter);
        }
    }
}
