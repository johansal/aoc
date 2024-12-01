using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace day_21
{
    class Program
    {
        static void Main()
        {
            List<string> lines = File.ReadAllLines("input.txt", Encoding.UTF8).ToList();
            List<string> unknownIngredients = new();
            List<(string, List<string>)> allergens = new();
            List<(string,string)> dangerousIngredients = new();

            foreach (var line in lines)
            {
                string[] splittedLine = line.Split(" (contains ");
                string[] a = splittedLine[1].Split(" ");
                var ingredients = splittedLine[0].Split(" ").ToList();
                unknownIngredients = unknownIngredients.Concat(ingredients).ToList();
                for (var i = 0; i < a.Length; i++)
                {
                    a[i] = a[i].Remove(a[i].Length - 1);
                    //Console.WriteLine("Adding allergen: " + a[i] + ", incredients " + string.Join(",", ingredients.ToArray()));
                    if (allergens.Any(x => x.Item1.Equals(a[i])))
                    {
                        var allergen = allergens.First(x => x.Item1.Equals(a[i]));
                        allergens.Remove(allergen);
                        //Console.WriteLine("Allergen already exists, with incredients: " + string.Join(",", allergen.Item2.ToArray()));
                        allergen.Item2 = allergen.Item2.Intersect(ingredients).ToList();
                        //Console.WriteLine("After intersect: " + string.Join(",", allergen.Item2.ToArray()));
                        allergens.Add(allergen);
                    }
                    else
                    {
                        allergens.Add((a[i], ingredients));
                    }
                }
            }

            bool flag = true;

            while (flag)
            {
                flag = false;
                //Console.WriteLine("Unknowns: " + string.Join(",", unknownIngredients.ToArray()));
                for (var i = 0; i < allergens.Count; i++)
                {
                    //Console.WriteLine("Allergen: " + allergens[i].Item1 + ", " + string.Join(",", allergens[i].Item2.ToArray()));
                    if (allergens[i].Item2.Count == 1)
                    {
                        var ingred = allergens[i].Item2[0];
                        dangerousIngredients.Add((allergens[i].Item1, ingred));
                        foreach (var a in allergens)
                        {
                            a.Item2.RemoveAll(x => x.Equals(ingred));
                        }
                        unknownIngredients.RemoveAll(x => x.Equals(ingred));
                        allergens[i].Item2.Clear();
                    }
                    else if (allergens[i].Item2.Count > 1)
                    {
                        flag = true;
                    }
                }
            }
            Console.WriteLine("Part 1: " + unknownIngredients.Count);
            Console.WriteLine("Part 2: " + string.Join(",", dangerousIngredients.OrderBy(s => s.Item1).Select(s => s.Item2).ToArray()));
        }
    }
}
