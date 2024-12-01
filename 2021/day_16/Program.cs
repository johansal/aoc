using System;
using System.Collections;
using System.Text;

namespace day_16
{
    public static class Program
    {
        static void Main(string[] args)
        {
            // DONE WITH LAPTOP!
            bool[] bools = new bool[2] { true, true };
            BitArray bitsA = new(bools);
            bools = new bool[2] { true, true };
            BitArray bitsB = new(bools);
            Console.WriteLine(" "+bitsA.ToBitString());
            Console.WriteLine("+"+bitsB.ToBitString());
            bitsA = Sum(bitsA,bitsB);
            Console.WriteLine(bitsA.ToBitString());
        }
        public static BitArray Sum(BitArray a, BitArray b)
        {
            if (a.Length != b.Length)
                throw new Exception("different lengths!");
            BitArray ret = new(a.Length+1);
            bool carry = false;

            bool partial = false;
            for (int i = a.Length - 1; i >= 0; i--)
            {
                partial = carry ^ a[i];
                carry = carry & a[i];
                ret[i+1] = partial ^ b[i];
                if (!carry) carry = partial & b[i];
            }
            ret[0] = carry;
            return ret;
        }
        public static BitArray Product(BitArray a, BitArray b) {
            return a;
        }
        public static string ToBitString(this BitArray bits)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < bits.Count; i++)
            {
                char c = bits[i] ? '1' : '0';
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
