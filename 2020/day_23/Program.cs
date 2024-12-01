using System;
using System.Linq;
using System.Collections.Generic;

namespace day_23
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "318946572";
            //string input = "389125467";//test

            LinkedList<long> circle = new(input.Select(c => long.Parse(c.ToString())));
            var pickedUp = new LinkedListNode<long>[3];
            long destination = -1;
            var current = circle.First;
            for (var i = 10; i <= 1000000; i++)
            {
                circle.AddLast(i);
            }
            Console.WriteLine("circle ready..");
            var cupsDict = new Dictionary<long, (bool active, LinkedListNode<long> node)>(circle.Count);
            for (var node = circle.First; !(node is null); node = node.Next)
            {
                cupsDict.Add(node.Value, (true, node));
            }
            Console.WriteLine("cupsDict ready...");
            for (var i = 0; i < 10000000; i++)
            {
                var next = current.Next ?? circle.First;
                for (var j = 0; j < 3; j++)
                {
                    pickedUp[j] = next;
                    next = next.Next ?? circle.First;
                    circle.Remove(pickedUp[j]);
                    cupsDict[pickedUp[j].Value] = (false, pickedUp[j]);
                }
                destination = current.Value == 1 ? circle.Count + pickedUp.Length : current.Value - 1;
                while (!cupsDict[destination].active)
                {
                    destination = destination == 1 ? circle.Count + pickedUp.Length : destination - 1;
                }
                var index = cupsDict[destination].node;
                for (var j = 0; j < pickedUp.Length; j++)
                {
                    circle.AddAfter(index, pickedUp[j]);
                    cupsDict[pickedUp[j].Value] = (true, pickedUp[j]);
                    index = pickedUp[j];
                }
                current = current.Next ?? circle.First;
            }
            Console.WriteLine("Part 2: " + circle.SkipWhile(l => l != 1).Skip(1).Take(2).Aggregate((a, l) => a * l)); 
        }
    }
}
