using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Template
{
    public class Day_2019_10
    {
        public static string firstPuzzle(string location)
        {
            //Find asteroid with max line of sight to other asteroids
            string[] lines = File.ReadAllLines(@location, Encoding.UTF8);
            List<Tuple<int, int>> asteroids = new List<Tuple<int, int>>();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '#')
                    {
                        asteroids.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            Tuple<int, int> winner;
            int winnerCanSee = 0;

            foreach (var asteroid in asteroids)
            {
                List<Tuple<int, int>> canSee = new List<Tuple<int, int>>();
                foreach (var asteroid2 in asteroids)
                {
                    if (!asteroid.Equals(asteroid2))
                    {
                        if(canSee.Count == 0)
                            canSee.Add(asteroid2);
                        else {
                            bool sameAtan = false;
                            foreach(var seen in canSee)
                            {
                                if(Math.Atan2(asteroid2.Item2-asteroid.Item2,asteroid2.Item1-asteroid.Item1) == Math.Atan2(seen.Item2-asteroid.Item2,seen.Item1-asteroid.Item1)) 
                                {
                                    sameAtan = true;
                                    break;
                                }
                            }
                            if(!sameAtan)
                            {
                                canSee.Add(asteroid2);
                            }
                        }
                    }
                }
                if(winnerCanSee == 0 || canSee.Count > winnerCanSee) {
                    winner = asteroid;
                    winnerCanSee = canSee.Count;
                }
            }

            return winnerCanSee.ToString();
        }

        public static string secondPuzzle(string location)
        {
            //Find asteroid with max line of sight to other asteroids
            string[] lines = File.ReadAllLines(@location, Encoding.UTF8);
            List<Tuple<int, int>> asteroids = new List<Tuple<int, int>>();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '#')
                    {
                        asteroids.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            Tuple<int, int> winner = new Tuple<int, int>(0,0);
            List<Tuple<int, int>> winnerCanSee = new List<Tuple<int, int>>();

            foreach (var asteroid in asteroids)
            {
                List<Tuple<int, int>> canSee = new List<Tuple<int, int>>();
                foreach (var asteroid2 in asteroids)
                {
                    if (!asteroid.Equals(asteroid2))
                    {
                        if(canSee.Count == 0)
                            canSee.Add(asteroid2);
                        else {
                            bool sameAtan = false;
                            foreach(var seen in canSee)
                            {
                                if(Math.Atan2(asteroid2.Item2-asteroid.Item2,asteroid2.Item1-asteroid.Item1) == Math.Atan2(seen.Item2-asteroid.Item2,seen.Item1-asteroid.Item1)) 
                                {
                                    sameAtan = true;
                                    break;
                                }
                            }
                            if(!sameAtan)
                            {
                                canSee.Add(asteroid2);
                            }
                        }
                    }
                }
                if(winnerCanSee.Count == 0 || canSee.Count > winnerCanSee.Count) {
                    winner = asteroid;
                    winnerCanSee = canSee;
                }
            }
            
            var sortedAsteroids = winnerCanSee.OrderBy(i => Math.Atan2(i.Item2-winner.Item2,i.Item1-winner.Item1)).ToList();

            return (sortedAsteroids.ElementAt(sortedAsteroids.Count-200).Item2 * 100 + sortedAsteroids.ElementAt(sortedAsteroids.Count-200).Item1).ToString();
        }
    }
}
