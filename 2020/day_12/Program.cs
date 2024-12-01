using System;
using System.IO;
using System.Text;

namespace day_12
{
    class Program
    {
        static char[] Compas = { 'N', 'E', 'S', 'W' };
        static (int x, int y) Ship = (0, 0);
        static (int x, int y) Waypoint = (-1, 10);

        static void Main(string[] args)
        {
            string[] instructions = File.ReadAllLines("INPUT.txt", Encoding.UTF8);
            (int x, int y, int compasDirection) = (0, 0, 1);
            foreach (var i in instructions)
            {
                (x, y, compasDirection) = CountDelta(x, y, compasDirection, i);
                move(i);
                //Console.WriteLine("Current position: " + x + "," + y + ", dir:" + compasDirection);
            }
            Console.WriteLine("Part 1: " + (x + y));
            if (Ship.x < 0)
                Ship.x *= -1;
            if (Ship.y < 0)
                Ship.y *= -1;
            Console.WriteLine("Part 2: " + (Ship.x + Ship.y));
        }

        static (int xD, int yD, int newDirection) CountDelta(int x, int y, int compasDirection, string instruction)
        {
            char moveTo = instruction[0] != 'F' ? instruction[0] : Compas[compasDirection];
            int units = int.Parse(instruction.Substring(1));
            int turnAngle;
            switch (moveTo)
            {
                case 'N':
                    return (x - units, y, compasDirection);
                case 'S':
                    return (x + units, y, compasDirection);
                case 'E':
                    return (x, y + units, compasDirection);
                case 'W':
                    return (x, y - units, compasDirection);
                case 'L':
                    turnAngle = compasDirection - ((units / 90) % 4);
                    if (turnAngle < 0)
                        turnAngle += 4;
                    else if (turnAngle >= 4)
                        turnAngle -= 4;
                    //Console.WriteLine("Compas direction was " + compasDirection + ", change" + units + ", new direction is " + turnAngle);
                    return (x, y, turnAngle);
                case 'R':
                    turnAngle = compasDirection + ((units / 90) % 4);
                    if (turnAngle < 0)
                        turnAngle += 4;
                    else if (turnAngle >= 4)
                        turnAngle -= 4;
                    //Console.WriteLine("Compas direction was " + compasDirection + ", change " + units + ", new direction is " + turnAngle);
                    return (x, y, turnAngle);
                default:
                    throw new Exception("Illegal move to instruction " + moveTo);
            }
        }
        static void move(string instruction)
        {
            char moveTo = instruction[0];
            int units = int.Parse(instruction.Substring(1));
            int turnAngle;
            switch (moveTo)
            {
                case 'N':
                    Waypoint.x -= units;
                    break;
                case 'S':
                    Waypoint.x += units;
                    break;
                case 'E':
                    Waypoint.y += units;
                    break;
                case 'W':
                    Waypoint.y -= units;
                    break;
                case 'L':

                    turnAngle = ((units / 90) % 4);
                    //Console.Write("Turning L " + units + ", waypoint was " + Waypoint.x + " " + Waypoint.y);
                    if (turnAngle == 1)
                    {
                        int tmp = Waypoint.x;
                        Waypoint.x = Waypoint.y * -1;
                        Waypoint.y = tmp;
                    }
                    else if (turnAngle == 2)
                    {
                        Waypoint.x *= -1;
                        Waypoint.y *= -1;
                    }
                    else if (turnAngle == 3)
                    {
                        int tmp = Waypoint.x;
                        Waypoint.x = Waypoint.y;
                        Waypoint.y = tmp * -1;
                    }
                    //Console.Write(", new waypoint at " + Waypoint.x + " " + Waypoint.y + "\n");
                    break;
                case 'R':
                    turnAngle = ((units / 90) % 4);
                    //Console.Write("Turning R " + units + ", waypoint was " + Waypoint.x + " " + Waypoint.y);
                    if (turnAngle == 1)
                    {
                        int tmp = Waypoint.x;
                        Waypoint.x = Waypoint.y;
                        Waypoint.y = tmp * -1;
                    }
                    else if (turnAngle == 2)
                    {
                        Waypoint.x *= -1;
                        Waypoint.y *= -1;
                    }
                    else if (turnAngle == 3)
                    {
                        int tmp = Waypoint.x;
                        Waypoint.x = Waypoint.y * -1;
                        Waypoint.y = tmp;
                    }
                    //Console.Write(", new waypoint at " + Waypoint.x + " " + Waypoint.y + "\n");
                    break;
                case 'F':
                    Ship.x += Waypoint.x * units;
                    Ship.y += Waypoint.y * units;
                    break;
                default:
                    throw new Exception("Illegal move to instruction " + moveTo);
            }
        }
    }
}
