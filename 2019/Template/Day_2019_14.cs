using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Template
{
    public class Day_2019_14
    {
        public static string firstPuzzle(string location)
        {
            string[] lines = File.ReadAllLines(@location, Encoding.UTF8);
            Refinery refinery = new Refinery(lines);
            refinery.ProduceMolecule(new Molecule() { Name = "FUEL", Amount = 1 });
            return refinery.NeededOre.ToString();
        }
        public static string secondPuzzle(string location)
        {
            string[] lines = File.ReadAllLines(@location, Encoding.UTF8);
            Refinery refinery = new Refinery(lines);
            long maxFuel = 0;
            int factor = 10000;
            Dictionary<string, long> oldExtra = null;
            long oldNeededOre = 0;

            while (factor >= 1)
            {
                while (refinery.NeededOre < 1000000000000)
                {
                    oldExtra = new Dictionary<string, long>(refinery.Extra);
                    oldNeededOre = refinery.NeededOre;
                    refinery.ProduceMolecule(new Molecule() { Name = "FUEL", Amount = factor });
                    maxFuel += factor; 
                }
                //produced too much, reset old state and donwgrade factor / 10
                if(factor >= 1)
                {
                    refinery.Extra = new Dictionary<string, long>(oldExtra);
                    refinery.NeededOre = oldNeededOre;
                    maxFuel -= factor;
                    factor /= 10;
                }
            }
            return maxFuel.ToString();
        }

    }

    public class Refinery
    {
        public Dictionary<string, Reaction> Reactions = new Dictionary<string, Reaction>();
        public Dictionary<string, long> Extra = new Dictionary<string, long>();
        public long NeededOre = 0;

        public Refinery(string[] reactionArr)
        {
            Regex r = new Regex(@"(\d+ [A-Z]+)");
            foreach (var line in reactionArr)
            {
                List<Molecule> preProductions = new List<Molecule>();
                var match = r.Matches(line);
                var output = match.Last();

                var split = output.Value.Split(' ');
                int amount = int.Parse(split[0]);
                string name = split[1];

                var outputMolecule = new Molecule() { Name = name, Amount = amount };

                for (int i = 0; i < match.Count - 1; i++)
                {
                    split = match[i].Value.Split(' ');
                    amount = int.Parse(split[0]);
                    name = split[1];

                    preProductions.Add(new Molecule() { Name = name, Amount = amount });
                }

                Reactions.Add(outputMolecule.Name, new Reaction() { Output = outputMolecule, PreProductions = preProductions });
            }
        }

        public void ProduceMolecule(Molecule request)
        {
            if (request.Name.Equals("ORE"))
            {
                NeededOre += request.Amount;
                return;
            }

            if (Extra.ContainsKey(request.Name))
            {
                if (Extra[request.Name] >= request.Amount)
                {
                    Extra[request.Name] -= request.Amount;
                    return;
                }
                else
                {
                    request.Amount -= Extra[request.Name];
                    Extra[request.Name] = 0;
                }
            }
            var reaction = Reactions[request.Name];

            var reactionRepeat = (int)Math.Ceiling((double)request.Amount / (double)reaction.Output.Amount);

            foreach (var p in reaction.PreProductions)
            {
                ProduceMolecule(new Molecule() { Amount = p.Amount * reactionRepeat, Name = p.Name });
            }

            /* if we produced more than needed, adjust our running surplus */
            if (reaction.Output.Amount * reactionRepeat > request.Amount)
            {
                if (Extra.ContainsKey(reaction.Output.Name))
                {
                    Extra[reaction.Output.Name] += (reaction.Output.Amount * reactionRepeat) - request.Amount;
                }
                else
                {
                    Extra.Add(reaction.Output.Name, (reaction.Output.Amount * reactionRepeat) - request.Amount);
                }
            }
        }
    }

    public class Molecule
    {
        public string Name;
        public long Amount;
        public override string ToString()
        {
            return $"{Amount} {Name}";
        }
    }
    public class Reaction
    {
        public List<Molecule> PreProductions = new List<Molecule>();
        public Molecule Output;
    }
}
