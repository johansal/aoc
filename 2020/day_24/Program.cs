using System;
using System.IO;
using System.Collections.Generic;

namespace day_24
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new("input.txt");
            string line;
            Dictionary<(int, int, int), bool> pattern = new();
            int count = 0;
            while ((line = sr.ReadLine()) != null)
            {
                (int x, int y, int z) currentTile = (0, 0, 0);
                for (int i = 0; i < line.Length; i++)
                {
                    string direction = line[i].ToString();
                    if (direction == "s" || direction == "n")
                    {
                        i++;
                        direction += line[i];
                    }
                    currentTile = Traverse(currentTile, direction);
                }
                if (pattern.TryGetValue(currentTile, out bool tmp))
                {
                    if (tmp)
                    {
                        pattern[currentTile] = false;
                        count--;
                    }
                    else
                    {
                        pattern[currentTile] = true;
                        count++;
                    }
                }
                else
                {
                    pattern[currentTile] = true;
                    count++;
                }
            }

            Console.WriteLine("Part 1: " + count);
            for (int days = 0; days < 100; days++)
            {
                Dictionary<(int, int, int), bool> nextPattern = new();
                //käy läpi kaikki tilet patternissa ja kaikkien patternissa olevien 
                //tilejen naapurit
                //flippaa sääntöjen mukaan uudeksi patterniksi

                foreach (var tile in pattern)
                {
                    nextPattern[tile.Key] = Flip(tile.Key, pattern);
                    string[] neighbour = new string[] { "e", "se", "sw", "w", "nw", "ne" };
                    for (int i = 0; i < neighbour.Length; i++)
                    {
                        var n = Traverse(tile.Key, neighbour[i]);
                        nextPattern[n] = Flip(n, pattern);
                    }
                }
                pattern = nextPattern;
            }
            count = 0;
            foreach (var tile in pattern)
            {
                if(tile.Value)
                    count++;
            }
            Console.WriteLine("Part 2:" + count);
        }
        //return true for black and false for white
        static bool Flip((int x, int y, int z) currentTile, Dictionary<(int, int, int), bool> pattern)
        {
            //laske mustat naapurit, jos currentTile on musta ja naapureista 0 tai 2 on mustia, se flippaa valkoiseksi
            //jos currentTile on valkoinen ja naapureista 2 on mustia, se flippaa mustaksi

            string[] neighbour = new string[] { "e", "se", "sw", "w", "nw", "ne" };
            int blacks = 0;
            for (int i = 0; i < neighbour.Length; i++)
            {
                var n = Traverse(currentTile, neighbour[i]);
                if (pattern.TryGetValue(n, out bool tmp))
                {
                    if (tmp)
                    {
                        blacks++;
                    }
                }
            }
            if (pattern.TryGetValue(currentTile, out bool currentValue))
            {
                //black
                if (currentValue)
                {
                    return blacks == 1 || blacks == 2;
                }
                //white
                else
                {
                    return blacks == 2;
                }
            }
            else
            {
                //white
                return blacks == 2;
            }
        }

        static (int x, int y, int z) Traverse((int x, int y, int z) currentTile, string direction)
        {
            switch (direction)
            {
                case "e":
                    currentTile.x++;
                    currentTile.y--;
                    break;
                case "se":
                    currentTile.z++;
                    currentTile.y--;
                    break;
                case "sw":
                    currentTile.x--;
                    currentTile.z++;
                    break;
                case "w":
                    currentTile.x--;
                    currentTile.y++;
                    break;
                case "nw":
                    currentTile.z--;
                    currentTile.y++;
                    break;
                case "ne":
                    currentTile.x++;
                    currentTile.z--;
                    break;
            }
            return currentTile;
        }
    }
}
