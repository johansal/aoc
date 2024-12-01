using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace day_16
{
    class Program
    {
        static void Main(string[] args)
        {
            //Read rules to dictionary
            Dictionary<string, ((int, int) low, (int, int) high)> rules = new Dictionary<string, ((int, int), (int, int))>();

            var sr = new StreamReader("input.txt");
            string line;
            List<string[]> validTickets = new List<string[]>();
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Length == 0)
                    break;
                var splittedLine = line.Split(": ");
                var name = splittedLine[0];
                splittedLine = splittedLine[1].Split(" or ");
                var lowerRange = splittedLine[0];
                var higherRange = splittedLine[1];
                var splittedRangeL = lowerRange.Split("-");
                var splittedRangeH = higherRange.Split("-");
                rules.Add(
                    name,
                    (
                        (int.Parse(splittedRangeL[0]), int.Parse(splittedRangeL[1])),
                        (int.Parse(splittedRangeH[0]), int.Parse(splittedRangeH[1]))
                    )
                );
            }

            string[] myTicket = Array.Empty<string>();
            //Read your ticket
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Equals("your ticket:"))
                {
                    myTicket = sr.ReadLine().Split(",");
                    validTickets.Add(myTicket);
                    sr.ReadLine();
                    sr.ReadLine();
                    break;
                }
            }

            //read nearby tickets and calculate ticket scanning error rate
            var tser = 0;
            while ((line = sr.ReadLine()) != null)
            {
                bool isValid = true;
                string[] fields = line.Split(",");
                foreach (var field in fields)
                {
                    int fieldI = int.Parse(field);
                    bool isValidForAny = false;
                    foreach (var rule in rules)
                    {
                        //Console.WriteLine("check if " + field + " is in " + rule.Key);
                        if ((rule.Value.low.Item1 <= fieldI && rule.Value.low.Item2 >= fieldI) || (rule.Value.high.Item1 <= fieldI && rule.Value.high.Item2 >= fieldI))
                        {
                            isValidForAny = true;
                        }
                    }
                    if (!isValidForAny)
                    {
                        tser += fieldI;
                        isValid = false;
                    }
                }
                if (isValid)
                    validTickets.Add(fields);
            }
            Console.WriteLine("Part 1:" + tser);

            Dictionary<string, List<int>> ruleIds = new Dictionary<string, List<int>>();
            List<int> unknownIds = Enumerable.Range(0, myTicket.Length).ToList();

            foreach (var rule in rules)
            {
                List<int> possibleIds = new List<int>();
                for (var i = 0; i < myTicket.Length; i++)
                {
                    bool thisRuleId = true;
                    foreach (var valid in validTickets)
                    {
                        int fieldI = int.Parse(valid[i]);
                        if (!((rule.Value.low.Item1 <= fieldI && rule.Value.low.Item2 >= fieldI) || (rule.Value.high.Item1 <= fieldI && rule.Value.high.Item2 >= fieldI)))
                        {
                            thisRuleId = false;
                        }
                    }
                    if (thisRuleId)
                        possibleIds.Add(i);
                }
                ruleIds.Add(rule.Key, possibleIds);
            }

            while (unknownIds.Count > 0)
            {
                foreach (var rule in ruleIds)
                {
                    if (rule.Value.Count == 1)
                    {
                        unknownIds.Remove(rule.Value[0]);
                    }
                    else {
                        rule.Value.RemoveAll(x => !unknownIds.Contains(x));
                    }
                }
            }

            long departureCount = 1;
            foreach (var ruleId in ruleIds)
            {
                Console.WriteLine(ruleId.Key + " is file id " + ruleId.Value[0] + ", value " + int.Parse(myTicket[ruleId.Value[0]]) + " in my ticket.");
                if (ruleId.Key.StartsWith("departure"))
                {
                    departureCount *= int.Parse(myTicket[ruleId.Value[0]]);
                }
            }
            Console.WriteLine("Part 2: " + departureCount);
        }
    }
}
