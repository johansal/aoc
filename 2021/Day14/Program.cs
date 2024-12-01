using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day14
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").ToList();
            var template = input[0];
            input.RemoveAt(0);
            input.RemoveAt(0);
            Dictionary<string, string> polymers = new();
            Dictionary<string, long> pairCount = new();
            Dictionary<char, long> charCount = new();
            foreach (var line in input)
            {
                var tmp = line.Split(" -> ");
                polymers.Add(tmp[0], tmp[1]);
            }
            for (var i = 0; i < template.Length; i++)
            {
                charCount[template[i]] = charCount.Read(template[i]) + 1;
            }
            for (var i = 1; i < template.Length; i++)
            {
                string p = $"{template[i-1]}{template[i]}";
                pairCount[p] = pairCount.Read(p) + 1;
            }

            for (var steps = 0; steps < 40; steps++)
            {
                Console.WriteLine("Step " + steps);
                CreateNewPolymers();
            }
            var sorted = charCount.OrderBy(i => i.Value).ToList();
            Console.WriteLine(sorted.Last().Value - sorted.First().Value);

            void CreateNewPolymers()
            {
                var newPolymers = new Dictionary<string, long>();
                foreach (var pair in pairCount)
                {
                    if (polymers.ContainsKey(pair.Key))
                    {
                        char res = polymers[pair.Key][0];
                        string a = $"{pair.Key[0]}{res}";
                        string b = $"{res}{pair.Key[1]}";
                        newPolymers[a] = newPolymers.Read(a) + pair.Value;
                        newPolymers[b] = newPolymers.Read(b) + pair.Value;
                        charCount[res] = charCount.Read(res) + pair.Value;
                    }
                    else
                    {
                        newPolymers[pair.Key] = newPolymers[pair.Key];
                    }
                }
                pairCount = newPolymers;
            }
        }
        public static V Read<K, V>(this Dictionary<K, V> dic, K key)
        {
            if (dic.ContainsKey(key)) return dic[key];
            return default;
        }
    }
}
