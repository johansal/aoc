internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        (int h, int w) mapSize = (-1,input[0].Length);
        List<(int i, int j)> edges = [];
        List<(int i, int j)> boxes = [];
        List<(int i, int j)> spaces = [];
        (int i, int j) robot = (-1,-1);

        // Parse input: map & movement instructions separated by empty line
        bool isMap = true;
        for(int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            if(string.IsNullOrEmpty(line))
            {
                mapSize.h = i;
                isMap = false;
            }
            else if(isMap)
            {
                for(int j = 0; j < input[0].Length; j++)
                {
                    var cur = input[i][j];
                    if(cur == '#')
                    {
                        edges.Add((i,j));
                    }
                    else if(cur == 'O')
                    {
                        boxes.Add((i,j));
                    }
                    else if(cur == '.')
                    {
                        spaces.Add((i,j));
                    }
                    else if(cur == '@')
                    {
                        robot = (i,j);
                    }
                }
            }
            else {
                //Parse instructions on this line
                foreach(var c in line)
                {
                    if(c == '>')
                    {
                        var edge = edges.Where(e => e.i == robot.i && e.j > robot.j).OrderBy(e => e.j).First();
                        try {
                            var space = spaces.Where(s => s.i == robot.i && s.j > robot.j && s.j < edge.j).OrderBy(s => s.j).First();
                            spaces.Remove(space);
                            spaces.Add(robot);
                            if(robot.j + 1 == space.j)
                            {              
                                robot = space;
                            }
                            else {
                                //Move box
                                var box = (robot.i, robot.j + 1);
                                boxes.Remove(box);
                                boxes.Add(space);
                                robot = box;
                            }
                        }
                        catch(InvalidOperationException)
                        {
                            //No space, do nothing
                        }
                    }
                    else if(c == '<')
                    {
                        var edge = edges.Where(e => e.i == robot.i && e.j < robot.j).OrderByDescending(e => e.j).First();
                        try {
                            var space = spaces.Where(s => s.i == robot.i && s.j < robot.j && s.j > edge.j).OrderByDescending(s => s.j).First();
                            spaces.Remove(space);
                            spaces.Add(robot);
                            if(robot.j - 1 == space.j)
                            {              
                                robot = space;
                            }
                            else {
                                //Move box
                                var box = (robot.i, robot.j - 1);
                                boxes.Remove(box);
                                boxes.Add(space);
                                robot = box;
                            }
                        }
                        catch(InvalidOperationException)
                        {
                            //No space, do nothing
                        }
                    }
                    else if(c == 'v')
                    {
                        var edge = edges.Where(e => e.j == robot.j && e.i > robot.i).OrderBy(e => e.j).First();
                        try {
                            var space = spaces.Where(s => s.j == robot.j && s.i > robot.i && s.i < edge.i).OrderBy(s => s.i).First();
                            spaces.Remove(space);
                            spaces.Add(robot);
                            if(robot.i + 1 == space.i)
                            {              
                                robot = space;
                            }
                            else {
                                //Move box
                                var box = (robot.i + 1, robot.j);
                                boxes.Remove(box);
                                boxes.Add(space);
                                robot = box;
                            }
                        }
                        catch(InvalidOperationException)
                        {
                            //No space, do nothing
                        }
                    }
                    else if(c == '^')
                    {
                        var edge = edges.Where(e => e.j == robot.j && e.i < robot.i).OrderByDescending(e => e.i).First();
                        try {
                            var space = spaces.Where(s => s.j == robot.j && s.i < robot.i && s.i > edge.i).OrderByDescending(s => s.i).First();
                            spaces.Remove(space);
                            spaces.Add(robot);
                            if(robot.i - 1 == space.i)
                            {              
                                robot = space;
                            }
                            else {
                                //Move box
                                var box = (robot.i - 1, robot.j);
                                boxes.Remove(box);
                                boxes.Add(space);
                                robot = box;
                            }
                        }
                        catch(InvalidOperationException)
                        {
                            //No space, do nothing
                        }
                    }
                    //Console.WriteLine($"Move {c}:");
                    //Print(mapSize, edges, boxes, spaces, robot);
                }
            }
        }
        //Print map for test
        //Print(mapSize, edges, boxes, spaces, robot);
        var part1 = 0;
        foreach(var box in boxes)
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