using System;
using System.IO;
using System.Text;

namespace day_25
{
    class Program
    {
        static void Main(string[] args)
        {
            //test
            //string[] lines = new string[] {"5764801","17807724"};
            string[] lines = File.ReadAllLines("input.txt", Encoding.UTF8);

            long cardPublicKey = long.Parse(lines[0]);
            long cardLoopSize = ReverseTransform(7, cardPublicKey);
            long doorPublicKey = long.Parse(lines[1]);
            long doorLoopSize = ReverseTransform(7, doorPublicKey);
            long encryptionKey = Transform(cardPublicKey, doorLoopSize);
            Console.WriteLine("Part 1: " + encryptionKey);
        }
        static long Transform(long subjectNumber, long loopSize)
        {
            long value = 1;
            for (var i = 0; i < loopSize; i++)
            {
                value *= subjectNumber;
                value %= 20201227;
            }
            return value;
        }

        static long ReverseTransform(long subjectNumber, long value)
        {
            long v = 1;
            long loopSize = 0;
            while (v != value)
            {
                loopSize++;
                v *= subjectNumber;
                v %= 20201227;
            }
            return loopSize;
        }
    }
}
