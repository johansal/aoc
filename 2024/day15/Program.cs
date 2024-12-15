using day15;

internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        (int height, int width) = (-1,input[0].Length);
        Map map = new();

        // Parse input: map & movement instructions separated by empty line
        bool isMap = true;
        for(int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            if(string.IsNullOrEmpty(line))
            {
                height = i;
                isMap = false;
            }
            else if(isMap)
            {
                for(int j = 0; j < input[0].Length; j++)
                {
                    map.Add(input[i][j], i, j, 1);
                }
            }
            else {
                //Parse instructions on this line
                foreach(var c in line)
                {
                    map.Move(c);
                    //Console.WriteLine($"Move {c}:");
                    //Print(mapSize, edges, boxes, spaces, robot);
                }
            }
        }
        //Print map for test
        //Print(mapSize, edges, boxes, spaces, robot);
        var part1 = 0;
        foreach(var box in map.Boxes)
        {
            part1 += Gps(box);
        }
        Console.WriteLine(part1);
    }
    private static int Gps((int i, int j) box)
    {
        return 100 * box.i + box.j;
    }

    private static void Print((int h, int w) mapSize, List<(int i, int j)> edges, List<(int i, int j)> boxes, List<(int i, int j)> spaces, (int i, int j) robot)
    {
        for(int i = 0; i < mapSize.h; i++)
        {
            for(int j = 0; j < mapSize.w; j++)
            {
                if(edges.Contains((i,j)))
                {
                    Console.Write('#');
                }
                else if(boxes.Contains((i,j)))
                {
                    Console.Write('O');
                }
                else if(spaces.Contains((i,j)))
                {
                    Console.Write('.');
                }
                else if (robot.i == i && robot.j == j) {
                    Console.Write('@');
                }
                else {
                    Console.Write('X');
                }
            }
            Console.Write("\n");
        }
        Console.WriteLine();
    }
}