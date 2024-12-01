using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Template
{
    public class Day_2019_6
    {
        public static string firstPuzzle(string location)
        {
            string[] tmp = File.ReadAllLines(@location, Encoding.UTF8);
            List<string[]> orbits = new List<string[]>();
            for (int i = 0; i < tmp.Length; i++)
            {
                orbits.Add(tmp[i].Split(')'));
            }
            int counter = 0;
            foreach (var orbit in orbits)
            {
                counter += routeToCOM(orbits, orbit).Count;
            }
            return counter.ToString(); //261306
        }

        public static string secondPuzzle(string location)
        {
            //init input and list of orbits
            string[] tmp = File.ReadAllLines(@location, Encoding.UTF8);
            List<string[]> orbits = new List<string[]>();
            for (int i = 0; i < tmp.Length; i++)
            {
                orbits.Add(tmp[i].Split(')'));
            }

            //find YOU and SAN(ta)
            string[] you = orbits.First(array => array[1] == "YOU");
            string[] san = orbits.First(array => array[1] == "SAN");

            //Find routes to com
            List<string[]> yourRoute = routeToCOM(orbits, you);
            List<string[]> santaRoute = routeToCOM(orbits, san);

            //Count jumps to first common orbit
            int counter = 0;
            foreach (var orbit in yourRoute)
            {
                counter++;
                if (santaRoute.Contains(orbit))
                {
                    counter += santaRoute.FindIndex(a => a[0] == orbit[0]);
                    counter -= 3; //orbits for YOU, SAN and first common, shouldn't be counted
                    break;
                }
            }
            return counter.ToString();
        }

        public static List<string[]> routeToCOM(List<string[]> orbits, string[] o)
        {
            //Console.WriteLine("counting orbits for " + string.Join(")", o));
            List<string[]> r = new List<string[]>();
            while (!o[0].Equals("COM"))
            {
                r.Add(o);
                o = orbits.First(array => array[1] == o[0]);
            }
            r.Add(o);
            return r;
        }

    }
}
