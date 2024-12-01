using System;
using System.IO;
using System.Collections.Generic;

namespace Day15
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Dijkstra to find shortest path from top left to down right corner
            var input = IntArray(File.ReadAllLines("input.txt"));
            int h = input.Length * 5;
            int l = input[0].Length * 5;
            int count = h * l;
            int[] distance = new int[count];
            bool[] sptSet = new bool[count];
            for (var i = 0; i < count; i++)
            {
                distance[i] = int.MaxValue; //init distance with int.Max so first path will always be less than this
                sptSet[i] = false; //init each node as not checked
            }
            //set distance of top left to zero since we are there already
            distance[0] = 0;

            //find miinimum distance for all nodes - top left
            for (var c = 0; c < count-1; c++)
            {
                //get node u which has minimum distance but adjacent nodes hasn't been checked yet
                var u = MinimumDistance(distance, sptSet, count);
                sptSet[u] = true; //u is checked after this round, no need tocheck it anymore

                //check adjacent nodes (v) of u that have not been checked yet and calculate their distance
                for (var v = 0; v < count; v++)
                {
                    if (!sptSet[v] &&
                    IsAdjacent(u, v, l))
                    {
                        var i = v/l;
                        var j = v%l;
                        var iMod = i%input.Length;
                        var jMod = j%input[0].Length;
                        var risk = input[iMod][jMod] + (i/input.Length) + (j/input[0].Length);
                        if(risk > 9) risk %= 9;
                        var tmp = distance[u] + risk; //update distance only if it was less than some previous distance too this node
                        distance[v] = tmp < distance[v] ? tmp : distance[v];
                    }
                }
            }
            //Print(distance,l);
            Console.WriteLine(distance[^1]);
        }
        private static bool IsAdjacent(int current, int other, int columns)
        {
            return (other - 1 == current && other % columns != 0) ||
            (other + 1 == current && current % columns != 0) ||
            other - columns == current ||
            other + columns == current;
        }
        private static int MinimumDistance(int[] dist, bool[] sptSet, int count)
        {
            int min = int.MaxValue;
            int minIndex = 0;

            for (int v = 0; v < count; ++v)
            {
                if (!sptSet[v] && dist[v] <= min)
                {
                    min = dist[v];
                    minIndex = v;
                }
            }
            return minIndex;
        }
        public static int[][] IntArray(String[] arr)
        {
            var h = arr.Length;
            int[][] ret = new int[h][];
            for (var i = 0; i < h; i++)
            {
                var l = arr[i].Length;
                ret[i] = new int[l];
                for (var j = 0; j < l; j++)
                {
                    ret[i][j] = arr[i][j] - '0';
                }
            }
            return ret;
        }
        public static void Print(int[] arr, int column) {
            for(int i = 0; i < arr.Length; i++) {
                var str = arr[i].ToString();
                while(str.Length < 4) {
                    str = " " + str;
                }
                Console.Write(str);
                if((i+1) % column == 0) Console.WriteLine();
            }
        }
    }
}
