using System;
using System.IO;
using System.Text;

namespace day_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Part 1: " + Trees(1,3));
            Console.WriteLine("Part 2: " + (Trees(1,1)*Trees(1,3)*Trees(1,5)*Trees(1,7)*Trees(2,1)));
        }
        private static int Trees(int xd, int yd) {
            var map = File.ReadAllLines("input.txt", Encoding.UTF8);
            var x = 0;
            var y = 0;
            var treeCounter = 0;
            while(x < map.Length) {
                if(map[x][y] == '#')
                    treeCounter++;
                y += yd;
                if(y >= map[x].Length)
                    y -= map[x].Length;
                x += xd;
            }
            return treeCounter;
        }
    }
}
