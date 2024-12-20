namespace day20;
public class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        Pos start = new(0,0), end = new(0,0);
        List<Pos> boulders = [];
        int h = input.Length, l = input[0].Length;
        var map = new bool[h, l];
        for(int i = 0; i < h; i++)
        {
            for(int j = 0; j < l; j++)
            {
                map[i, j] = true;
                if(input[i][j] == 'S')
                {
                    start = new Pos(i,j);
                }
                else if(input[i][j] == 'E')
                {
                    end = new Pos(i,j);
                }
                else if(input[i][j] == '#')
                {
                    // set boulder
                    map[i, j] = false;
                    boulders.Add(new Pos(j,i));
                }
            }
        }
        // Find shortest route
        int baseline = Solve(start, end, map, h-1);
        Console.WriteLine($"Baseline: {baseline}");
        Dictionary<int, int> timeSaved = [];
        // Remove one boulder and see how much time is saved
        foreach(Pos b in boulders) {
            map[b.Col, b.Row] = true;
            int time = Solve(start, end, map, h-1);
            var save = baseline - time;
            //Console.WriteLine($"Removing {b.Col},{b.Row} got {time}, saving {save}");
            if (time < 0 || save <= 0)
            {
                // no time was saved
            }
            else if(timeSaved.TryGetValue(save, out var value)) 
            {
                timeSaved[save] = value + 1;
            }
            else {
                timeSaved[save] = 1;
            }
            map[b.Col, b.Row] = false;
        }
        var part1 = 0;
        foreach(int i in timeSaved.Keys.Where(x => x >= 100))
        {
            Console.WriteLine($"{timeSaved[i]} cheats that save {i} picoseconds.");
            part1 += timeSaved[i];
        }
        Console.WriteLine(part1);

    }
    private static int Solve(Pos start, Pos end, bool[,] map, int h)
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
            if (InBounds(next, h) && map[next.Row, next.Col] && visited.ContainsKey(next) == false)
            {
                var nextPriority = nextPathLength + Heuristic(next, end);
                q.Enqueue((next, nextPathLength), nextPriority);
            }

            next = current.Move(Compas.S);
            if (InBounds(next, h) && map[next.Row, next.Col] && visited.ContainsKey(next) == false)
            {
                var nextPriority = nextPathLength + Heuristic(next, end);
                q.Enqueue((next, nextPathLength), nextPriority);
            }
        
            next = current.Move(Compas.E);
            if (InBounds(next, h) && map[next.Row, next.Col] && visited.ContainsKey(next) == false)
            {
                var nextPriority = nextPathLength + Heuristic(next, end);
                q.Enqueue((next, nextPathLength), nextPriority);
            }

            next = current.Move(Compas.W);
            if (InBounds(next, h) && map[next.Row, next.Col] && visited.ContainsKey(next) == false)
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