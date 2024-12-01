using System;
using System.IO;

namespace Day2
{
    public class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            Console.WriteLine(AimNavi(lines));
        }
        // Part1
        private static int Navigate(string[] lines) {
            // x, y
            var position = (0,0);
            foreach(var line in lines) {
                var command = line.Split(' ');
                var value = int.Parse(command[1]);
                if(command[0] == "forward") {
                    position.Item1 += value;
                }
                else if(command[0] == "down") {
                    position.Item2 += value;
                }
                else if(command[0] == "up") {
                    position.Item2 -= value;
                }
            }
            return position.Item1 * position.Item2;
        }

        //Part2
        public static int AimNavi(string[] lines) {
            // x, y , aim
            var position = (0,0,0);
            foreach(var line in lines) {
                var command = line.Split(' ');
                var value = int.Parse(command[1]);
                if(command[0] == "forward") {
                    position.Item1 += value;
                    position.Item2 +=  position.Item3 * value;
                }
                else if(command[0] == "down") {
                    position.Item3 += value;
                }
                else if(command[0] == "up") {
                    position.Item3 -= value;
                }
            }
            return position.Item1 * position.Item2;
        }
    }
}
