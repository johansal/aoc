internal class Program
{
    private static void Main()
    {
        var input = File.ReadAllLines("input");
        Pos start = new(0,0), end = new(0,0);
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
                }
            }
        }

        // Dijkstra - with a priorityqueue this year.
        // Add start position and direction to queue
        var q = new PriorityQueue<(Pos Position, Compas Direction, HashSet<Pos> Moves), int>();
        var s = start;
        q.Enqueue((s, Compas.E, []), 0);
        
        // Track positions visited and shortest path to it from start
        var visited = new Dictionary<(Pos Position, Compas direction), int>();
        // Track lenght of shortest path from start to end and all nodes that are part of some shortest path
        var shortestPaths = new HashSet<Pos> {s};
        var shortestPathLength = 0;
        
        // Dequeue node with lowest pathLength, if any
        while (q.TryDequeue(out var current, out var pathLength))
        {
            var (position, direction, moves) = current;
            
            // Found path to end, store pathlength and moves
            if (position == end)
            {
                shortestPathLength = pathLength;
                shortestPaths.UnionWith(moves);
            }

            // check if we have visited this node already with smaller pathLength
            if (visited.GetValueOrDefault((position, direction), int.MaxValue) < pathLength)
            {
                continue;
            }

            // add this node to visited
            visited[(position, direction)] = pathLength;
 
            // queue next node ahead, on left and on right side, if they are not boulders
            var next = position.Move(direction);
            if (map[next.Row, next.Col])
            {
                var nextMoves = new HashSet<Pos>(moves) { next };
                q.Enqueue((next, direction, nextMoves), pathLength + 1);
            }

            var left = direction.TurnL();
            next = position.Move(direction.TurnL());
            if (map[next.Row, next.Col])
            {
                var nextMoves = new HashSet<Pos>(moves) { next };
                q.Enqueue((next, left, nextMoves), pathLength + 1001);
            }
        
            var right = direction.TurnR();
            next = position.Move(right);
            if (map[next.Row, next.Col])
            {
                var nextMoves = new HashSet<Pos>(moves) { next };
                q.Enqueue((next, right, nextMoves), pathLength + 1001);
            }
        }
        Console.WriteLine($"Part 1: {shortestPathLength}");
        Console.WriteLine($"Part 2: {shortestPaths.Count}");
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