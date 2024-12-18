namespace day18;
public class Program
{
    private static void Main(string[] args)
    {
        //(string[] arr, int gS, int sS) param = (File.ReadAllLines("test"), 6, 12); //test
        (string[] arr, int gS, int sS) param = (File.ReadAllLines("input"), 70, 1024); //prod

        var input = param.arr;
        int gridSize = param.gS;
        int simulationSteps = param.sS;
        
        Pos start = new(0,0), end = new(gridSize,gridSize);
        var map = new bool[gridSize+1, gridSize+1];
        //Parse boulder coordinates to map
        for(int i = 0; i < gridSize+1; i++)
        {
            for(int j = 0; j < gridSize+1; j++)
            {
                map[i,j] = true;
            }
        }
        foreach (var line in input.Take(simulationSteps))
        {
            // list has x,y coordinates (col, line)
            var strB = line.Split(",");
            map[int.Parse(strB[1]),int.Parse(strB[0])] = false;
        }
        PrintMap(map, gridSize + 1);
        Console.WriteLine($"Part 1: {Solve(start, end, map, gridSize)}");
    }
    private static int Solve(Pos start, Pos end, bool[,] map, int gridSize)
    {
        // Dijkstra from d16, without the direction component
        // Changed to A* with manhattan distance heuristic
        // Add start position to queue
        var q = new PriorityQueue<(Pos position, int pathLength), int>();
        var startH = Heuristic(start,end);
        q.Enqueue((start, 0), startH);
        
        // Track positions visited and shortest path to it from start
        var visited = new Dictionary<Pos, int>();

        // Dequeue node with lowest pathLength, if any
        while (q.TryDequeue(out var c, out var priority))
        {   
            var (current, pathLength) = c;
            Console.WriteLine($"Checking {current.Col},{current.Row} with prio {priority}");


            if (current == end) {
                return pathLength;
            }

            // check if we have visited this node already with smaller pathLength
            if (visited.GetValueOrDefault(current, int.MaxValue) < pathLength)
            {
                continue;
            }

            // add this node to visited
            visited[current] = pathLength;
 
            // queue next node ahead, on left and on right side, if they are not boulders or out of bounds
            var next = current.Move(Compas.N);
            var nextPathLength = pathLength + 1;
            if (InBounds(next, gridSize) && map[next.Row, next.Col] && visited.ContainsKey(next) == false)
            {
                var nextPriority = nextPathLength + Heuristic(next, end);
                q.Enqueue((next, nextPathLength), nextPriority);
            }

            next = current.Move(Compas.S);
            if (InBounds(next, gridSize) && map[next.Row, next.Col] && visited.ContainsKey(next) == false)
            {
                var nextPriority = nextPathLength + Heuristic(next, end);
                q.Enqueue((next, nextPathLength), nextPriority);
            }
        
            next = current.Move(Compas.E);
            if (InBounds(next, gridSize) && map[next.Row, next.Col] && visited.ContainsKey(next) == false)
            {
                var nextPriority = nextPathLength + Heuristic(next, end);
                q.Enqueue((next, nextPathLength), nextPriority);
            }

            next = current.Move(Compas.W);
            if (InBounds(next, gridSize) && map[next.Row, next.Col] && visited.ContainsKey(next) == false)
            {
                var nextPriority = nextPathLength + Heuristic(next, end);
                q.Enqueue((next, nextPathLength), nextPriority);
            }      
        }
        return -1;
    }
    private static bool InBounds(Pos c, int gridSize)
    {
        return c.Col >= 0 && c.Row >= 0 && c.Col <= gridSize && c.Row <= gridSize;
    }
    private static int Heuristic(Pos c, Pos end)
    {
        //use manhattan distance as heuristic
        return end.Col - c.Col + (end.Row - c.Row);
    }
    private static void PrintMap(bool[,] map, int len)
    {
        Console.WriteLine();
        for(int i = 0; i < len; i++)
        {
            for (int j = 0; j < len; j++)
            {
                if(map[i,j] == false)
                {
                    Console.Write("#");
                }
                else {
                    Console.Write(".");
                }
            }
            Console.Write("\n");
        }
    }
}

internal record Compas(int dx, int dy)
{
    public Compas TurnL()
    {
        return new Compas(-dy, dx);
    }
 
    public Compas TurnR()
    {
        return new Compas(dy, -dx);
    }
    public static readonly Compas E = new(0, 1);
    public static readonly Compas W = new(0, -1);
    public static readonly Compas N = new(-1, 0);
    public static readonly Compas S = new(1, 0);
}
internal record Pos(int Row, int Col)
{
    public Pos Move(Compas dir)
    {
        return new Pos(Row + dir.dx, Col + dir.dy);
    }
}