using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Template
{
    public class Day_2019_11
    {
        public static string firstPuzzle(string input)
        {
            long[] intcode = input.Split(',').Select(long.Parse).ToArray();
            IntComputer ic = new IntComputer(intcode);
            int i = 499;
            int j = 499;
            int compass = 2;
            List<Tuple<int, int>> painted = new List<Tuple<int, int>>();
            int[,] hull = new int[1000, 1000];
            ic.inputs.Add(hull[i, j]);
            ic.compute();
            while (ic.isWaiting)
            {
                //Paint
                //elem1: 0 = blck, 1 = wht, elem2: 0 = left, 1 = right
                if (hull[i, j] != (int)ic.outputs.ElementAt(0))
                {
                    hull[i, j] = (int)ic.outputs.ElementAt(0);
                    painted.Add(new Tuple<int, int>(i, j));
                }
                ic.outputs.RemoveAt(0);
                //Rotate
                compass = rotateCompass(compass, (int)ic.outputs.ElementAt(0));
                ic.outputs.RemoveAt(0);
                //Move
                if (compass == 1)
                    j--;
                else if (compass == 2)
                    i--;
                else if (compass == 3)
                    j++;
                else if (compass == 4)
                    i++;
                ic.inputs.Add(hull[i, j]);
                ic.compute();
            }
            var result = painted.GroupBy(key => key, item => 1)
                               .Select(group => new
                               {
                                   group.Key,
                                   Duplicates = group.Count()
                               }).ToList();

            return result.Count.ToString();
        }

        public static int rotateCompass(int compass, int direction)
        {
            if (direction == 0)
                compass--;
            else
                compass++;
            if (compass < 1)
                compass = 4;
            else if (compass > 4)
                compass = 1;
            return compass;
        }


        public static string secondPuzzle(string input)
        {
            long[] intcode = input.Split(',').Select(long.Parse).ToArray();
            IntComputer ic = new IntComputer(intcode);
            int i = 499;
            int j = 499;
            int compass = 2;
            List<Tuple<int, int>> painted = new List<Tuple<int, int>>();
            int[,] hull = new int[1000, 1000];
            hull[i, j] = 1;
            ic.inputs.Add(hull[i, j]);
            ic.compute();
            while (ic.isWaiting)
            {
                //Paint
                //elem1: 0 = blck, 1 = wht, elem2: 0 = left, 1 = right
                if (hull[i, j] != (int)ic.outputs.ElementAt(0))
                {
                    hull[i, j] = (int)ic.outputs.ElementAt(0);
                    painted.Add(new Tuple<int, int>(i, j));
                }
                ic.outputs.RemoveAt(0);
                //Rotate
                compass = rotateCompass(compass, (int)ic.outputs.ElementAt(0));
                ic.outputs.RemoveAt(0);
                //Move
                if (compass == 1)
                    j--;
                else if (compass == 2)
                    i--;
                else if (compass == 3)
                    j++;
                else if (compass == 4)
                    i++;
                ic.inputs.Add(hull[i, j]);
                ic.compute();
            }
            List<string> lines = new List<string>();
            //Print hull
            int minFirstOne = 1000;
            int maxLastOne = 0;
            for (i = 0; i < hull.GetLength(0); i++)
            {
                string line = "";
                int firstOne = 1000;
                int lastOne = 0;
                for (j = 0; j < hull.GetLength(1); j++)
                {
                    if(hull[i,j] == 1 && j < firstOne )
                        firstOne = j;
                    if(hull[i,j] == 1 && j > lastOne)
                        lastOne = j;
                    line += hull[i, j] == 0 ? " " : "#";
                }
                if(firstOne != 1000)
                    lines.Add(line);
                if(firstOne < minFirstOne)
                    minFirstOne = firstOne;
                if(lastOne > maxLastOne)
                    maxLastOne = lastOne;
            }
            Console.WriteLine("min first one: " + minFirstOne + ", max last one: " + maxLastOne + " lines " + lines.Count);
            List<string> trimmedLines = new List<string>();
            foreach(var line in lines) {
                trimmedLines.Add(line.Substring(0, maxLastOne + 4).Substring(minFirstOne - 3));
            }
            return string.Join('\n',trimmedLines);
        }
    }
}
