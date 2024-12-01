using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace day_5
{
    class Program
    {
        static void Main(string[] args)
        {
            //binary space partitioning
            var highesId = 0;
            string[] lines = File.ReadAllLines("input.txt", Encoding.UTF8);
            List<int> ids = new List<int>();
            for (var i = 0; i < lines.Length; i++)
            {
                var row = findSeat(lines[i].Substring(0, 7), 0, 127, 'F', 'B');
                var column = findSeat(lines[i].Substring(7), 0, 7, 'L', 'R');
                var id = row * 8 + column;
                //Console.WriteLine(string.Format("Boarding pass {0}: row {1}, column {2}, id {3}",i,row,column,id));
                if (id > highesId)
                    highesId = id;
                ids.Add(id);
            }
            Console.WriteLine("Part 1: " + highesId);
            
            ids.Sort();
            int myId = 0;
            for(var i = 1; i < ids.Count - 1; i++) {
                if(ids[i]-1 != ids[i-1]) {
                    myId = ids[i] - 1;
                    break;
                }
                else if(ids[i]+1 != ids[i+1]) {
                    myId = ids[i] + 1;
                    break;
                }
            }
            Console.WriteLine("Part 2: " + myId);
        }
        private static int findSeat(string range, int rangeMin, int rangeMax, char low, char high)
        {
            //Console.WriteLine(string.Format("range {0}, min {1}, max {2}", range, rangeMin, rangeMax));
            if (range.Length > 1)
            {
                if (range[0] == low)
                {
                    return findSeat(range.Substring(1), rangeMin, (rangeMax - rangeMin) / 2 + rangeMin, low, high);
                }
                else if (range[0] == high)
                {
                    //the +1 to high half min is important, othervice the middle is allocated to both halves
                    return findSeat(range.Substring(1), (rangeMax - rangeMin) / 2 + rangeMin + 1, rangeMax, low, high);
                }
                else
                {
                    throw new Exception(string.Format("Expected {0} or {1}, found ", low, high) + range[0]);
                }
            }
            else
            {
                if (range[0] == low)
                {
                    return rangeMin;
                }
                else if (range[0] == high)
                {
                    return rangeMax;
                }
                else
                {
                    throw new Exception(string.Format("Expected {0} or {1}, found ", low, high) + range[0]);
                }
            }
        }
    }
}
