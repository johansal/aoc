using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace day_19
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            string line = "";
            List<string> rules = new List<string>();
            List<string> lines = new List<string>();
            bool isRule = true;

            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    isRule = false;
                }
                else if (isRule)
                {
                    var tmp = line.Split(": ");
                    var ruleIndex = int.Parse(tmp[0]);
                    var rule = tmp[1];
                    while (rules.Count - 1 < ruleIndex)
                    {
                        rules.Add("");
                    }
                    rules[ruleIndex] = rule;
                }
                else
                {
                    lines.Add(line);
                }
            }

            int count = 0;
            Regex rg = new Regex("^" + ParseRule(0, rules) + "$");
            foreach (string l in lines)
            {
                if (rg.IsMatch(l)) count++;
            }
            Console.WriteLine("Part 1: " + count);
            rules[8] = "42 +";
            rules[11] = "42 {n} 31 {n}";

            count = 0;
            string ogRgS = ParseRule(0, rules);

            foreach (string l in lines)
            {
                for (var i = '1'; i < '5'; i++)
                {
                    var rgS = ogRgS.Replace('n', i);
                    rg = new Regex("^" + rgS + "$");
                    Match m = rg.Match(l);
                    //Console.WriteLine(m.Groups.Count + " groups found.");
                    if (m.Success)
                    {
                        count++;
                    }
                }
            }
            Console.WriteLine("Part 2: " + count);//363 < x < 424
        }

        static string ParseRule(int ruleI, List<String> rules)
        {
            //Console.WriteLine("parsing rule: " + rules[ruleI] + " @" + ruleI);
            string[] rule = rules[ruleI].Split(" ");
            if (rule.Length == 1)
            {
                //this might be leaf rule, need to tryparse int first to see
                //Console.WriteLine("rule[0]: " + rule[0]);
                if (rule[0].Equals("\"a\""))
                {
                    return "a";
                }
                else if (rule[0].Equals("\"b\""))
                {
                    return "b";
                }
                else
                {
                    return ParseRule(int.Parse(rule[0]), rules);
                }
            }
            else
            {
                //this is not leaf, parse rule for all subrules
                for (int i = 0; i < rule.Length; i++)
                {
                    if (rule[i] != "|" && rule[i] != "+" && rule[i] != "{n}")
                    {
                        //Console.WriteLine("debug: " + rule[i]);
                        rule[i] = ParseRule(int.Parse(rule[i]), rules);
                    }
                }
                var tmp = string.Join("", rule);
                tmp = "(?:" + tmp + ")";
                return tmp;
            }
        }
    }
}
