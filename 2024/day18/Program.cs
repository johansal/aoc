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
        //Parse boulder coordinates to map 
        var boulders = ParseBoulders(input, simulationSteps);
        PrintMap(gridSize, boulders, []);
        Console.WriteLine($"Part 1: {Solve(start, end, boulders, gridSize)}");
    }
    private static int Solve(Pos start, Pos end, HashSet<Pos> boulders, int gridSize)
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
            var current = c.position;
            var pathLength = c.pathLength;

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
            if (BoundaryCheck(next, gridSize) && boulders.Contains(next) == false)
            {
                var nextPriority = nextPathLength + Heuristic(next, end);
                q.Enqueue((next, nextPathLength), nextPriority);
            }

            next = current.Move(Compas.S);
            if (BoundaryCheck(next, gridSize) && boulders.Contains(next) == false)
            {
                var nextPriority = nextPathLength + Heuristic(next, end);
                q.Enqueue((next, nextPathLength), nextPriority);
            }
        
            next = current.Move(Compas.E);
            if (BoundaryCheck(next, gridSize) && boulders.Contains(next) == false)
            {
                var nextPriority = nextPathLength + Heuristic(next, end);
                q.Enqueue((next, nextPathLength), nextPriority);
            }

            next = current.Move(Compas.W);
            if (BoundaryCheck(next, gridSize) && boulders.Contains(next) == false)
            {
                var nextPriority = nextPathLength + Heuristic(next, end);
                q.Enqueue((next, nextPathLength), nextPriority);
            }      
        }
        return -1;
    }

    private static HashSet<Pos> ParseBoulders(string[] input, int simulationSteps) {
        HashSet<Pos> boulders = [];
        foreach (var line in input.Take(simulationSteps))
        {
            var strB = line.Split(',');
            // list has x,y coordinates (col, line)
            boulders.Add(new(int.Parse(strB[1]),int.Parse(strB[0])));
        }
        return boulders;
    }
    private static int Heuristic(Pos c, Pos end)
    {
        //use manhattan distance as heuristic
        return end.Col - c.Col + (end.Row - c.Row);
    }
    private static bool BoundaryCheck(Pos p, int gridLen)
    {
        return p.Col >= 0 && p.Row >= 0 && p.Col <= gridLen && p.Row <= gridLen;
    }
    private static void PrintMap(int gridSize, HashSet<Pos> boulders, HashSet<Pos> path)
    {
        Console.WriteLine();
        for(int i = 0; i <= gridSize; i++)
        {
            for (int j = 0; j <= gridSize; j++)
            {
                Pos p = new(i,j);
                if(path.Contains(p))
                {
                    Console.Write("o");
                }
                else if(boulders.Contains(p))
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