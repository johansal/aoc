namespace day20;
public class Program
{
    private static void Main()
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
        var path = Djikstra(start, end, map);
        //Calculate cheats
        var part1 = Cheats(path, map.GetLength(0), 2, 100);
        Console.WriteLine($"Part1: {part1}");
        var part2 = Cheats(path, map.GetLength(0), 20, 100);
        Console.WriteLine($"Part2: {part2}");
    }
    private static int Cheats(Dictionary<Pos, int> path, int h, int maxLen, int minSave)
    {
        List<int> saves = [];
        foreach(var p in path)
        {
            saves.AddRange(FindShortCuts(p.Key, path, h, p.Value + maxLen).Values.Where(x => x >= minSave).ToList());
        }
        /*foreach(var g in saves.GroupBy(x=> x).OrderBy(x => x.Key))
        {
            Console.WriteLine($"{g.Count()} cheats that save {g.Key}");
        }*/
        return saves.Count;
    }
    private static Dictionary<Pos, int> FindShortCuts(Pos start, Dictionary<Pos, int> path, int h, int maxLen)
    {
        Dictionary<Pos, int> shortcuts = [];
        Dictionary<Pos, int> distances = [];

        // BFS queue
        Queue<Pos> q = new();
        q.Enqueue(start);
        distances[start] = path[start];

        while(q.Count > 0) {
            var cur = q.Dequeue();

            //Check if we are back on path
            if(cur != start && path.ContainsKey(cur))
            {
                //if distance to path node is smaller than the existing distance on path,
                //we found a shortcut
                if(distances[cur] < path[cur])
                {
                    // return how much our shortcut saves
                    int saves = path[cur] - distances[cur];
                    if(shortcuts.ContainsKey(cur) == false || shortcuts[cur] < saves)
                    {
                        shortcuts[cur] = saves;
                    }
                }
            }

            var nextDistance = distances[cur] + 1;
            foreach(var dir in Compas.Directions())
            {
                var next = cur.Move(dir);
                if (nextDistance <= maxLen && InBounds(next, h) && distances.ContainsKey(next) == false)
                {
                    distances[next] = nextDistance;
                    q.Enqueue(next); 
                }
            }
        }
        return shortcuts;
    }
    private static Dictionary<Pos, int> Djikstra(Pos start, Pos end, bool[,] map)
    {
        // Add start position to queue
        var q = new PriorityQueue<(Pos position, Dictionary<Pos, int>), int>();
        var shortestPaths = new Dictionary<Pos, int>
        {
            [start] = 0
        };
        q.Enqueue((start, shortestPaths), 0);
        
        // Track positions visited and shortest path to it from start
        var visited = new Dictionary<Pos, int>();

        // Dequeue node with lowest pathLength, if any
        while (q.TryDequeue(out var c, out var pathLength))
        {   
            var (current, moves) = c;

            //check if end
            if (current == end) {
                return moves;
            }

            // check if we have visited this node already with smaller pathLength
            if (visited.GetValueOrDefault(current, int.MaxValue) < pathLength)
            {
                continue;
            }

            // add this node to visited
            visited[current] = pathLength;
 
            // queue next node ahead if not boulder or out of bounds
            var nextPathLength = pathLength + 1;
            foreach(var dir in Compas.Directions())
            {
                var next = current.Move(dir);
                if (InBounds(next, map.GetLength(0)) && map[next.Row, next.Col] && visited.ContainsKey(next) == false)
                {
                    var nextMoves = new Dictionary<Pos, int>(moves)
                    {
                        [next] = nextPathLength
                    };
                    q.Enqueue((next, nextMoves), nextPathLength);
                }
            }
        }
        return [];
    }
    private static bool InBounds(Pos c, int gridSize)
    {
        return c.Col >= 0 && c.Row >= 0 && c.Col < gridSize && c.Row < gridSize;
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
    public static Compas[] Directions()
    {
        return [N, W, E, S];
    }
}
internal record Pos(int Row, int Col)
{
    public Pos Move(Compas dir)
    {
        return new Pos(Row + dir.dx, Col + dir.dy);
    }
}