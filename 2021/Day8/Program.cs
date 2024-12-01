using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            int counter = 0;
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var arr = line.Split(" | ");
                var unresolved = arr[0].Split(" ").ToList();
                var outputValues = arr[1].Split(" ").ToList();
                string[] resolved = new string[10];
                while (unresolved.Count > 0)
                {
                    var value = SortString(unresolved[0]);
                    switch (value.Length)
                    {
                        case 2:
                            //output 1
                            resolved[1] = value;
                            unresolved.RemoveAt(0);
                            break;
                        case 3:
                            //output 7
                            resolved[7] = value;
                            unresolved.RemoveAt(0);
                            break;
                        case 4:
                            //output 4
                            resolved[4] = value;
                            unresolved.RemoveAt(0);
                            break;
                        case 7:
                            //output 8
                            resolved[8] = value;
                            unresolved.RemoveAt(0);
                            break;
                        case 5:
                            //output 2,3,5
                            if (!string.IsNullOrEmpty(resolved[1]) &&
                                !string.IsNullOrEmpty(resolved[4]))
                            {
                                //3
                                if (value.Contains(resolved[1][0]) && value.Contains(resolved[1][1]))
                                {
                                    resolved[3] = value;
                                    unresolved.RemoveAt(0);
                                }
                                //2,5
                                else
                                {
                                    var diff = resolved[4].Where(c => !resolved[1].Contains(c));
                                    //5
                                    if (!diff.Except(value).Any())
                                    {
                                        resolved[5] = value;
                                        unresolved.RemoveAt(0);
                                    }
                                    //2
                                    else
                                    {
                                        resolved[2] = value;
                                        unresolved.RemoveAt(0);
                                    }
                                }
                            }
                            else
                            {
                                unresolved.Add(value);
                                unresolved.RemoveAt(0);
                            }
                            break;
                        case 6:
                            //output 0,6,9
                            if (!string.IsNullOrEmpty(resolved[1]) &&
                                !string.IsNullOrEmpty(resolved[4]))
                            {
                                //9
                                if (!resolved[4].Except(value).Any())
                                {
                                    resolved[9] = value;
                                    unresolved.RemoveAt(0);
                                }
                                //0,6
                                else
                                {
                                    var diff = resolved[4].Where(c => !resolved[1].Contains(c));
                                    //6
                                    if (!diff.Except(value).Any())
                                    {
                                        resolved[6] = value;
                                        unresolved.RemoveAt(0);
                                    }
                                    //0
                                    else
                                    {
                                        resolved[0] = value;
                                        unresolved.RemoveAt(0);
                                    }
                                }
                            }
                            else
                            {
                                unresolved.Add(value);
                                unresolved.RemoveAt(0);
                            }
                            break;
                        default:
                            throw new Exception("wtf");
                    }
                }
                var tmpCounter = 0;
                int multi = 1000;
                foreach (var output in outputValues)
                {
                    var o = SortString(output);
                    //Console.WriteLine(o);
                    for (var i = 0; i < resolved.Length; i++)
                    {
                        //Console.WriteLine(i + " " + resolved[i]);
                        if (resolved[i].Equals(o))
                        {
                            //Console.Write(i);
                            tmpCounter += i * multi;
                            multi /= 10;
                            break;
                        }
                    }
                }
                //Console.WriteLine("\n"+tmpCounter);
                counter += tmpCounter;
            }
            Console.WriteLine(counter);
        }
        public static string SortString(string s)
        {
            char[] tmp = s.ToArray();
            Array.Sort(tmp);
            return new string(tmp);
        }
    }
}
