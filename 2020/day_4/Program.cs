using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace day_4
{
    class Program
    {
        static void Main(string[] args)
        {
            //Read line, split fields and add to passport untill empty tow changes passport
            int validCounter = 0;
            int hasManhatoryFieldsCounter = 0;
            string line;
            var sr = new StreamReader("input.txt", Encoding.UTF8);
            Dictionary<string, string> passport = new Dictionary<string, string>();
            while ((line = sr.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    var fields = line.Split(" ");
                    for (var i = 0; i < fields.Length; i++)
                    {
                        var field = fields[i].Split(":");
                        passport.Add(field[0], field[1]);
                    }
                }
                else
                {
                    //validate passport
                    if (PassportHasMandatoryFields(passport))
                        hasManhatoryFieldsCounter++;
                    if (PassportIsValid(passport))
                        validCounter++;
                    passport = new Dictionary<string, string>();
                }
            }
            if (passport.Keys.Count != 0)
            {
                if (PassportHasMandatoryFields(passport))
                    hasManhatoryFieldsCounter++;
                if (PassportIsValid(passport))
                    validCounter++;
            }
            Console.WriteLine("Part1: " + hasManhatoryFieldsCounter);
            Console.WriteLine("Part2: " + validCounter);
        }

        public static bool PassportHasMandatoryFields(Dictionary<string, string> passport)
        {
            return passport.ContainsKey("byr") &&
                    passport.ContainsKey("iyr") &&
                    passport.ContainsKey("eyr") &&
                    passport.ContainsKey("hgt") &&
                    passport.ContainsKey("hcl") &&
                    passport.ContainsKey("ecl") &&
                    passport.ContainsKey("pid");
        }
        public static bool PassportIsValid(Dictionary<string, string> passport)
        {
            var hclRx = new Regex("^#[0-9a-f]{6}$");
            var pidRx = new Regex("^[0-9]{9}$");
            var validEyeColors = new List<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            return PassportHasMandatoryFields(passport) &&
                passport["byr"].Length == 4 && Int32.TryParse(passport["byr"], out int byr) && byr >= 1920 && byr <= 2002 &&
                passport["iyr"].Length == 4 && Int32.TryParse(passport["iyr"], out int iyr) && iyr >= 2010 && iyr <= 2020 &&
                passport["eyr"].Length == 4 && Int32.TryParse(passport["eyr"], out int eyr) && eyr >= 2020 && eyr <= 2030 &&
                ValidHeight(passport["hgt"]) &&
                hclRx.Matches(passport["hcl"]).Count == 1 &&
                validEyeColors.Contains(passport["ecl"]) &&
                pidRx.Matches(passport["pid"]).Count == 1;
        }
        public static bool ValidHeight(string h)
        {
            if (h.EndsWith("cm"))
            {
                var b = Int32.TryParse(h.Split("cm")[0], out int n);
                return b && n >= 150 && n <= 193;
            }
            else if (h.EndsWith("in"))
            {
                var b = Int32.TryParse(h.Split("in")[0], out int n);
                return b && n >= 59 && n <= 76;
            }
            else
            {
                return false;
            }
        }
    }
}
