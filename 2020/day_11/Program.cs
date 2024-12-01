using System;
using System.IO;
using System.Text;

namespace day_11
{
    public static class Program
    {
        public static void Main()
        {
            string[] lines = File.ReadAllLines("input.txt", Encoding.UTF8);
            var placesChanged = true;
            var seatsTaken = 0;
            while (placesChanged)
            {
                string[] newLines = new string[lines.Length];
                seatsTaken = 0;
                placesChanged = false;
                for (int i = 0; i < lines.Length; i++)
                {
                    for (int j = 0; j < lines[i].Length; j++)
                    {
                        var c = lines[i][j];
                        if (ChangeState2(lines, i, j)) //Call ChangeState here for part 1 answer
                        {
                            placesChanged = true;
                            if (c == '#')
                            {
                                newLines[i] += 'L';
                            }
                            else
                            {
                                newLines[i] += '#';
                                seatsTaken++;
                            }
                        }
                        else
                        {
                            newLines[i] += c;
                            if (c == '#')
                                seatsTaken++;
                        }
                    }
                }
                Array.Copy(newLines, lines, lines.Length);
            }
            Console.WriteLine("Part 2: " + seatsTaken); //Call ChangeState instead of ChangeState2 for part 1 answer
        }
        private static bool ChangeState(string[] lines, int x, int y)
        {
            char c = lines[x][y];
            switch (c)
            {
                case '.':
                    return false;
                default:
                    var occupied = 0;
                    if (x - 1 >= 0 && y - 1 >= 0 && lines[x - 1][y - 1] == '#')
                        occupied++;
                    if (x - 1 >= 0 && lines[x - 1][y] == '#')
                        occupied++;
                    if (x - 1 >= 0 && y + 1 < lines[0].Length && lines[x - 1][y + 1] == '#')
                        occupied++;
                    if (y - 1 >= 0 && lines[x][y - 1] == '#')
                        occupied++;
                    if (y + 1 < lines[0].Length && lines[x][y + 1] == '#')
                        occupied++;
                    if (x + 1 < lines.Length && y - 1 >= 0 && lines[x + 1][y - 1] == '#')
                        occupied++;
                    if (x + 1 < lines.Length && lines[x + 1][y] == '#')
                        occupied++;
                    if (x + 1 < lines.Length && y + 1 < lines[0].Length && lines[x + 1][y + 1] == '#')
                        occupied++;
                    return (c == '#' && occupied >= 4) || (c == 'L' && occupied == 0);
            }
        }
        private static bool ChangeState2(string[] lines, int x, int y)
        {
            char c = lines[x][y];
            switch (c)
            {
                case '.':
                    return false;
                default:
                    var occupied = 0;
                    var i = x;
                    var j = y;
                    while (i - 1 >= 0 && j - 1 >= 0)
                    {
                        if (lines[i - 1][j - 1] == '#')
                        {
                            occupied++;
                            break;
                        }
                        else if (lines[i - 1][j - 1] == 'L')
                        {
                            break;
                        }
                        i--;
                        j--;
                    }
                    i = x;
                    j = y;
                    while (i - 1 >= 0) {
                        if(lines[i - 1][j] == '#') {
                            occupied++;
                            break;
                        }
                        else if(lines[i - 1][j] == 'L') {
                            break;
                        }
                        i--;
                    }
                    i = x;
                    j = y;
                    while (i - 1 >= 0 && j + 1 < lines[0].Length) {
                        if(lines[i - 1][j + 1] == '#') {
                            occupied++;
                            break;
                        }
                        else if(lines[i - 1][j + 1] == 'L') {
                            break;
                        }
                        i--;
                        j++;
                    }
                    i = x;
                    j = y;
                    while (j - 1 >= 0) {
                        if(lines[i][j-1] == '#') {
                            occupied++;
                            break;
                        }
                        else if(lines[i][j-1] == 'L') {
                            break;
                        }
                        j--;
                    }
                    i = x;
                    j = y;
                    while (j + 1 < lines[0].Length) {
                        if(lines[i][j+1] == '#') {
                            occupied++;
                            break;
                        }
                        else if(lines[i][j+1] == 'L') {
                            break;
                        }
                        j++;
                    }
                    i = x;
                    j = y;
                    while (i + 1 < lines.Length && j - 1 >= 0) {
                        if(lines[i + 1][j - 1] == '#') {
                            occupied++;
                            break;
                        }
                        else if(lines[i + 1][j - 1] == 'L') {
                            break;
                        }
                        i++;
                        j--;
                    }
                    i = x;
                    j = y;
                    while (i + 1 < lines.Length) {
                        if(lines[i + 1][j] == '#') {
                            occupied++;
                            break;
                        }
                        else if(lines[i + 1][j] == 'L') {
                            break;
                        }
                        i++;
                    }
                    i = x;
                    j = y;
                    while (i + 1 < lines.Length && j + 1 < lines[0].Length) {
                        if(lines[i + 1][j + 1] == '#') {
                            occupied++;
                            break;
                        }
                        else if(lines[i + 1][j + 1] == 'L') {
                            break;
                        }
                        i++;
                        j++;
                    }
                    return (c == '#' && occupied >= 5) || (c == 'L' && occupied == 0);
            }
        }

    }
}