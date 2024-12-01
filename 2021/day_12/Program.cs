using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace day_12
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            List<List<string>> paths = new();
            List<List<string>> newPaths;

            paths.Add(new List<string>());
            paths[0].Add("start");
            bool addedNode = true;
            while (addedNode)
            {
                addedNode = false;
                newPaths = new();
                foreach (var path in paths)
                {
                    var position = path[path.Count - 1];
                    if (!position.Equals("end"))
                    {
                        var connections = input.Where(x => x.Contains(position)).ToList();
                        //Console.WriteLine(position + " has " + connections.Count + " connections");
                        foreach (var connection in connections)
                        {
                            var nextCave = ParseCave(position, connection);
                            if (nextCave.Equals(nextCave.ToLower()))
                            {
                                //small cave, check if we have been here allready
                                if (!path.Contains(nextCave))
                                {
                                    newPaths.Add(new List<string>(path));
                                    newPaths[newPaths.Count - 1].Add(nextCave);
                                    addedNode = true;
                                }
                                //if we have, check if we have been on any small cave twice
                                else if (!nextCave.Equals("start") &&
                                    !nextCave.Equals("end") &&
                                    (path.Where(x => x.Equals(x.ToLower())).Distinct().Count() == path.Where(x => x.Equals(x.ToLower())).Count()))
                                {
                                    newPaths.Add(new List<string>(path));
                                    newPaths[newPaths.Count - 1].Add(nextCave);
                                    addedNode = true;
                                }
                            }
                            else
                            {
                                //big cave
                                newPaths.Add(new List<string>(path));
                                newPaths[newPaths.Count - 1].Add(nextCave);
                                addedNode = true;
                            }
                        }
                    }
                    else
                    {
                        newPaths.Add(new List<string>(path));
                    }
                }
                if (addedNode) paths = newPaths;
            }
            //PrintPaths(paths);
            Console.WriteLine(paths.Count);
        }
        public static void PrintPaths(List<List<string>> paths)
        {
            foreach (var path in paths)
            {
                foreach (var i in path)
                {
                    Console.Write(i + ",");
                }
                Console.WriteLine();
            }
        }
        public static string ParseCave(string start, string connection)
        {
            return connection.Split("-").Where(x => x != start).FirstOrDefault();
        }
    }
}
