using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace day_14
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt", Encoding.UTF8);
            string mask = "";
            ulong[] mem = new ulong[99999];
            ulong val = 0;
            Dictionary<string, string> part2 = new Dictionary<string, string>();

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Split(" = ");
                if (line[0] == "mask")
                {
                    mask = line[1];
                }
                else
                {
                    val = ulong.Parse(line[1]);
                    int address = int.Parse(line[0][4..^1]);
                    string strVal = ApplyMask(Convert.ToString((long)val, 2).PadLeft(mask.Length, '0'), mask);
                    mem[address] = Convert.ToUInt64(strVal, 2);

                    var part2res = ApplyMask2(Convert.ToString(address,2), mask);
                    foreach (var res in part2res)
                    {
                        //Console.WriteLine("part2: adding " + line[1] + " to " + res);
                        part2[res] = line[1];
                    }
                }
            }
            ulong sum = 0;
            for (var i = 0; i < mem.Length; i++)
            {
                sum += mem[i];
            }
            Console.WriteLine("Part 1: " + sum);
            sum = 0;
            foreach (var key in part2)
            {
                sum += ulong.Parse(key.Value);
            }
            Console.WriteLine("Part 2: " + sum);
        }
        private static string ApplyMask(string value, string mask)
        {
            string maskedVal = "";
            for (var i = 0; i < mask.Length; i++)
            {
                if (mask[i] == 'X')
                {
                    maskedVal += value[i];
                }
                else
                {
                    maskedVal += mask[i];
                }
            }
            return maskedVal;
        }
        private static string[] ApplyMask2(string value, string mask)
        {
            if (value.Length < mask.Length)
                value = value.PadLeft(mask.Length, '0');
            string[] result = new string[(int)Math.Pow(2, mask.Length - mask.Replace("X", "").Length)];
            var count = 0;
            for (var i = 0; i < mask.Length; i++)
            {
                char bit = '1';
                for (var j = 0; j < result.Length; j++)
                {
                    if (mask[i] == '0')
                    {
                        result[j] += value[i];
                    }
                    else if (mask[i] == '1')
                    {
                        result[j] += '1';
                    }
                    else
                    {
                        if (j % (int)Math.Pow(2,count) == 0)
                            bit = bit == '0' ? '1' : '0';
                        result[j] += bit;
                    }
                }
                if (mask[i] == 'X')
                    count++;
            }
            return result;
        }
    }
}
