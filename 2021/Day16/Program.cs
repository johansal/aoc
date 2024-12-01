using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Day16
{
    public static class Program
    {
        public static void Main()
        {
            var i = 0;
            var input = File.ReadAllText("input.txt");
            BitArray bytes = HexaDecode(input[i]);
            for (i = 1; i < input.Length; i++)
            {
                bytes = Append(bytes, HexaDecode(input[i]));
            }
            i = 0;
            (int, BitArray) ret = ParsePacket(i, bytes);
            Console.WriteLine(GetIntFromBitArray(ret.Item2));
        }
        public static (int, BitArray) ParsePacket(int packageStart, BitArray bytes)
        {
            // Get header from beginning of package (packageStart)
            // 1st 3-bits: Version
            // 2nd 3-bits: Type ID
            int version = 0;
            int typeId = 0;
            int i = 0;
            while (i < 6)
            {
                i++;
                if (i - 1 < 3 && bytes[packageStart + i - 1]) version += (int)Math.Pow(2, 3 - i);
                if (i - 1 >= 3 && bytes[packageStart + i - 1]) typeId += (int)Math.Pow(2, 6 - i);
            }

            //Console.WriteLine("v " + version);
            //Console.WriteLine("t " + typeId);

            if (typeId == 4)
            {
                // Package has literal value
                BitArray literalValue = new(0);
                do
                {
                    // take next 5 bits, append last 4 to literalValue
                    i++;
                    BitArray nextBits = new(4);
                    for (var j = 0; j < 4; j++)
                    {
                        nextBits[j] = bytes[packageStart + i + j];
                    }
                    literalValue = Append(literalValue, nextBits);
                    i += 4;
                    // if 1st bit was 1 repeat
                } while (bytes[packageStart + i - 5]);
                //Console.WriteLine("literalvalue " + GetIntFromBitArray(literalValue));
                return (packageStart + i, literalValue);
            }
            else
            {
                // Package is operator
                long subLength;
                //i++;
                //Console.WriteLine("i " + bytes[originalI + i]);
                if (!bytes[packageStart + i])
                {
                    i++;
                    //next 15 bits are total length of sub packets
                    BitArray len = new(15);
                    for (var j = 0; j < 15; j++)
                    {
                        len[j] = bytes[packageStart + i + j];
                        //Console.Write(len[j]);
                    }
                    //Console.WriteLine();
                    i += 15;
                    subLength = GetIntFromBitArray(len);
                    //Console.WriteLine("l " +  subLength);
                    var subEnd = packageStart + i + subLength;
                    BitArray subRet = null;
                    var currentI = i + packageStart;
                    while (currentI < subEnd)
                    {
                        var subPacket = ParsePacket(currentI, bytes);
                        currentI = subPacket.Item1;
                        subRet = AgregatePacketValue(subRet, subPacket.Item2, typeId);
                    }
                    return (currentI, subRet);
                }
                else
                {
                    i++;
                    //next 11 bits are number of sub packages
                    BitArray len = new(11);
                    for (var j = 0; j < 11; j++)
                    {
                        len[j] = bytes[packageStart + i + j];
                    }
                    i += 11;
                    subLength = GetIntFromBitArray(len);
                    //Console.WriteLine("c " +  subLength);
                    BitArray subRet = null;
                    var currentI = i + packageStart;
                    for (var x = 0; x < subLength; x++)
                    {
                        var subPacket = ParsePacket(currentI, bytes);
                        currentI = subPacket.Item1;
                        subRet = AgregatePacketValue(subRet, subPacket.Item2, typeId);
                    }
                    return (currentI, subRet);
                }
            }
        }
        public static BitArray AgregatePacketValue(BitArray original, BitArray value, int typeId)
        {
            if (original == null) return value;
            String[] chars = { " + ", " * ", " min ", " max ", "lol", " greater than ", " smaller than ", " equal to " };
            long v = 0;
            if (typeId == 0) { v = GetIntFromBitArray(original) + GetIntFromBitArray(value); }
            else if (typeId == 1) { v = GetIntFromBitArray(original) * GetIntFromBitArray(value); }
            else if (typeId == 2) { v = Math.Min(GetIntFromBitArray(original), GetIntFromBitArray(value)); }
            else if (typeId == 3) { v = Math.Max(GetIntFromBitArray(original), GetIntFromBitArray(value)); }
            else if (typeId == 5) { v = GetIntFromBitArray(original) > GetIntFromBitArray(value) ? 1 : 0; }
            else if (typeId == 6) { v = GetIntFromBitArray(original) < GetIntFromBitArray(value) ? 1 : 0; }
            else if (typeId == 7) { v = GetIntFromBitArray(original) == GetIntFromBitArray(value) ? 1 : 0; }
            else
            {
                throw new Exception("WTF?");
            }
            Console.WriteLine(original + chars[typeId] + value + " = " + v);
            return GetBitArrayFromInt(v);
        }
        public static BitArray HexaDecode(char hex)
        {
            int value = hex - '0';
            if (value > 15) value -= 7;
            var hexB = BitConverter.GetBytes(value).Reverse().ToArray();
            var str = Convert.ToString(hexB[^1], 2).PadLeft(4, '0');
            return new BitArray(str.Select(c => c == '1').ToArray());
        }

        public static BitArray Append(this BitArray current, BitArray after)
        {
            var bools = new bool[current.Count + after.Count];
            current.CopyTo(bools, 0);
            after.CopyTo(bools, current.Count);
            return new BitArray(bools);
        }
        public static long GetIntFromBitArray(BitArray bitArray)
        {
            long value = 0;
            for (var i = 0; i < bitArray.Length; i++)
            {
                if (bitArray[^(i + 1)]) value += (long)Math.Pow(2, i);
            }
            return value;
        }
        public static BitArray GetBitArrayFromInt(long value)
        {
            BitArray b = new BitArray(new int[] { (int)value });

        }
        public static BitArray Sum(BitArray a, BitArray b)
        {
            bool carry = false;
            bool partial = false;
            BitArray ret = new(Math.Max(a.Length, b.Length) + 1);
            for (var i = 0; i < ret.Length; i++)
            {
                if ()
            }
        }
    }
}
