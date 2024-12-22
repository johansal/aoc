using System.Data;
using System.Text.RegularExpressions;

namespace day21;
internal class Program
{
    private static void Main()
    {
        var input = File.ReadAllLines("input");

        var numpad = FloodFill(["789", "456", "123", "#0A"]);
        var keypad = FloodFill(["#^A", "<v>"]);

        Dictionary<(string keys, int depth), long> cache = [];
        Console.WriteLine(PushButtons(input, 2, cache, numpad, keypad));
        Console.WriteLine(PushButtons(input, 25, cache, numpad, keypad));
    }
    private static long PushButtons(
        string[] input, int robots, Dictionary<(string keys, int depth), long> cache,
        Dictionary<(char, char), List<string>> numKeys, Dictionary<(char, char), List<string>> arrowKeys) 
    {
        long count = 0;
        foreach(var line in input)
        {
            List<string> numOptions = [];
            BuildSequence(line, 0, numKeys, 'A', numOptions, "");
            long min = long.MaxValue;
            foreach(var option in numOptions)
            {
                var len = SequenceLen(option, robots, arrowKeys, cache);
                if(len < min)
                {
                    min = len;
                }
            }
            var num = int.Parse(line[..^1]);
            count += min * num;
        }
        return count;
    }
    private static long SequenceLen(
        string keys, int depth, Dictionary<(char, char), List<string>> map, 
        Dictionary<(string, int), long> memo)
    {
        if (depth == 0)
            return keys.Length;
        if (memo.TryGetValue((keys, depth), out var v))
            return v;
        long totalLen = 0;
        string pattern = $@"(?<=[{Regex.Escape("A")}])";
        var parts = Regex.Split(keys, pattern);
        foreach(var key in parts) 
        {
            List<string> sequences = [];
            BuildSequence(key, 0, map, 'A', sequences, "");
            long min = long.MaxValue;
            foreach(var seq in sequences)
            {
                var len = SequenceLen(seq, depth-1, map, memo);
                if(len < min)
                    min = len;
            }
            totalLen += min;
        }
        memo[(keys, depth)] = totalLen;
        return totalLen;
    }
    private static void BuildSequence(
        string keys, int cur, Dictionary<(char, char), List<string>> map, 
        char previousKey, List<string> result, string currentPath)
    {
        if(keys.Length == cur)
        {
            result.Add(currentPath);
            return;
        }
        var key = keys[cur];
        foreach(var path in map[(previousKey, key)])
        {
            BuildSequence(keys, cur + 1, map, key, result, currentPath + path + 'A');
        }
    }

    private static Dictionary<(char, char), List<string>> FloodFill(string[] map)
    {
        Dictionary<(char, char), List<string>> ret = [];
        for(var i = 0; i < map.Length; i++) {
            for(var j = 0; j < map[0].Length; j++)
            {
                for(var n = 0; n < map.Length; n++) {
                    for(var m = 0; m < map[0].Length; m++)
                    {
                        if(map[i][j] == '#' || map[n][m] == '#')
                        {
                            continue;
                        }
                        Pos start = new(i,j);
                        var (end, moves) = Djikstra(start, map[n][m], map);
                        ret[(map[i][j],map[n][m])] = moves;
                    }
                }
            }
        }
        return ret;
    }
    private static (Pos end, List<string> moves) Djikstra(Pos start, char end, string[] map)
    {
        // Add start position to queue
        var q = new PriorityQueue<(Pos position, string), int>();
        q.Enqueue((start, string.Empty), 0);
        
        // Track positions visited and shortest path to it from start
        var visited = new Dictionary<Pos, int>();
        
        Pos e = new(0,0);
        List<string> shortestPaths = [];

        // Dequeue node with lowest pathLength, if any
        while (q.TryDequeue(out var c, out var pathLength))
        {   
            var (current, moves) = c;

            //check if end
            if (map[current.Row][current.Col] == end) {
                e = current;
                shortestPaths.Add(moves);
            }

            // check if we have visited this node already with smaller pathLength
            if (visited.GetValueOrDefault(current, int.MaxValue) < pathLength)
            {
                continue;
            }

            // add this node to visited
            visited[current] = pathLength;
 
            // queue next node ahead if not boulder or out of bounds
            foreach(var dir in Compas.Directions())
            {
                var next = current.Move(dir);
                var nextMove = Compas.GetDirectionalInput(dir);
                if (
                    InBounds(next, map.Length, map[0].Length) && 
                    map[next.Row][next.Col] != '#' && 
                    visited.ContainsKey(next) == false)
                {
                    
                    var nextPathLength = pathLength + 1;
                    string nextMoves = moves + nextMove;
                    q.Enqueue((next, nextMoves), nextPathLength);
                }
            }
        }

        var zigzagPattern = new Regex(@"(<[^<]+<)|(>[^>]+>)|(\^[^\^]+\^)|(v[^v]+v)");

        // Filter out movements with zigzag patterns
        var prunedMovements = shortestPaths.Where(movement => 
            !zigzagPattern.IsMatch(movement)
        ).ToList();

        return (e,prunedMovements); 
    }
    private static bool InBounds(Pos c, int h, int l)
    {
        return c.Col >= 0 && c.Row >= 0 && c.Col < l && c.Row < h;
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
    public static char GetDirectionalInput(Compas dir)
    {
        if (dir == N)
            return '^';
        else if(dir == S)
            return 'v';
        else if (dir == E)
            return '>';
        else
            return '<';        
    }
}
internal record Pos(int Row, int Col)
{
    public Pos Move(Compas dir)
    {
        return new Pos(Row + dir.dx, Col + dir.dy);
    }
}