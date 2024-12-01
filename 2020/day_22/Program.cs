using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day_22
{
    public static class Program
    {
        public static void Main()
        {
            List<int> playerOne = new();
            List<int> playerTwo = new();

            StreamReader sr = new("input.txt");
            string line;
            bool p2Hand = false;
            while ((line = sr.ReadLine()) != null)
            {
                if (line == "Player 1:") { p2Hand = false; }
                else if (line == "Player 2:") { p2Hand = true; }
                else if (line.Length == 0) { continue; }
                else
                {
                    int card = int.Parse(line);
                    if (p2Hand) playerTwo.Add(card);
                    else playerOne.Add(card);
                }
            }
            HashSet<(int, int)> decks = new();
            while (playerOne.Count > 0 && playerTwo.Count > 0)
            {
                (int, int) deck = (playerOne.ToList().GetSequenceHashCode(), playerTwo.ToList().GetSequenceHashCode());
                if (decks.Contains(deck))
                {
                    break;
                }
                else
                {
                    decks.Add(deck);
                }
                if (Combat(playerOne, playerTwo))
                {
                    playerOne.Add(playerOne[0]);
                    playerOne.RemoveAt(0);
                    playerOne.Add(playerTwo[0]);
                    playerTwo.RemoveAt(0);
                }
                else
                {
                    playerTwo.Add(playerTwo[0]);
                    playerTwo.RemoveAt(0);
                    playerTwo.Add(playerOne[0]);
                    playerOne.RemoveAt(0);
                }
            }

            int score = 0;
            if (playerOne.Count == 0) playerOne = playerTwo;
            for (var i = 0; i < playerOne.Count; i++)
            {
                score += playerOne[i] * (playerOne.Count - i);
            }
            Console.WriteLine("Part 2: " + score); //<36009
        }
        public static bool Combat(List<int> p1, List<int> p2)
        {
            HashSet<(int, int)> decks = new();
            if (p1[0] < p1.Count && p2[0] < p2.Count)
            {
                List<int> p1c = p1.Take(p1[0]+1).ToList();
                p1c.RemoveAt(0);
                List<int> p2c = p2.Take(p2[0]+1).ToList();
                p2c.RemoveAt(0);
                while (p1c.Count > 0 && p2c.Count > 0)
                {
                    (int, int) deck = (p1c.ToList().GetSequenceHashCode(), p2c.ToList().GetSequenceHashCode());
                    if (decks.Contains(deck))
                    {
                        //Console.WriteLine("Decks " + decks.Count + " contains " + deck);
                        return true;
                    }
                    else
                    {
                        decks.Add(deck);
                    }
                    if (Combat(p1c, p2c))
                    {
                        p1c.Add(p1c[0]);
                        p1c.RemoveAt(0);
                        p1c.Add(p2c[0]);
                        p2c.RemoveAt(0);
                    }
                    else
                    {
                        p2c.Add(p2c[0]);
                        p2c.RemoveAt(0);
                        p2c.Add(p1c[0]);
                        p1c.RemoveAt(0);
                    }
                }
                return p1c.Count > 0;
            }
            else
            {
                return p1[0] > p2[0];
            }
        }
        public static int GetSequenceHashCode<T>(this IList<T> sequence)
        {
            const int seed = 487;
            const int modifier = 31;

            unchecked
            {
                return sequence.Aggregate(seed, (current, item) =>
                    (current*modifier) + item.GetHashCode());
            }
        }
    }
}
