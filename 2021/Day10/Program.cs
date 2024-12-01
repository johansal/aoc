using System;
using System.IO;
using System.Collections.Generic;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var open = "([{<";
            var close = ")]}>";
            var input = File.ReadAllLines("input.txt");
            var syntaxErrorScore = 0;
            List<long> autoCorrectScore = new();
            foreach (var line in input)
            {
                List<char> read = new();
                var corrupted = false;
                foreach (var c in line)
                {
                    var found = close.IndexOf(c);
                    if (found >= 0)
                    {
                        if (read.Count > 0 && read[0] == open[found])
                        {
                            //line is ok
                            //Console.WriteLine(line + " close tag " + c + " last read " + read[0]);
                            read.RemoveAt(0);
                        }
                        else
                        {
                            //corrupted
                            //Console.WriteLine(line + " corrupted " + c + " last read " + read[0]);
                            corrupted = true;
                            if (found == 0)
                            {
                                syntaxErrorScore += 3;
                            }
                            else if (found == 1)
                            {
                                syntaxErrorScore += 57;
                            }
                            else if (found == 2)
                            {
                                syntaxErrorScore += 1197;
                            }
                            else
                            {
                                syntaxErrorScore += 25137;
                            }
                        }
                    }
                    else
                    {
                        //Console.WriteLine(c);
                        read.Insert(0, c);
                    }
                    if (corrupted)
                    {
                        break;
                    }
                }
                if (read.Count > 0 && !corrupted)
                {
                    //line is incomplete
                    long score = 0;
                    for (var i = 0; i < read.Count; i++)
                    {
                        var point = 1 + open.IndexOf(read[i]);
                        //Console.WriteLine(read[i] + " scores " + point + " points" );
                        score = (score * 5) + point;
                    }
                    //Console.WriteLine("score: " + score);
                    autoCorrectScore.Add(score);
                }
            }
            Console.WriteLine("part 1: " + syntaxErrorScore);
            var index = (int)(autoCorrectScore.Count / 2);
            //Console.WriteLine("index is " + index + " value before sort " + autoCorrectScore[index]);
            autoCorrectScore.Sort();
            Console.WriteLine("part 2: " + autoCorrectScore[index]);
        }
    }
}
