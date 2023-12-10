namespace day10;

public class Day10 {
    public static void Main()
    {
        //find Bobers start position
        var input = File.ReadAllLines("inputs/input");
        (int x, int y) Bober = (0,0);
        for(int i = 0; i < input.Length; i++)
        {
            int j = input[i].IndexOf('S');
            if(j >= 0)
            {
                Bober = (i,j);
                break;
            }
        }
        //check adjacent tiles you can move to
        // | - L J 7 F .
        List<(int x, int y)> visited = [];
        visited.Add(Bober);
        var valids = LoopNeighbours(Bober, input);
        var newNeighbours = valids.Where(x => visited.Contains(x) == false).ToList();
        var fartestPoint = 0;
        while(newNeighbours.Count > 0)
        {
            fartestPoint++;
            visited.AddRange(newNeighbours);
            valids = [];
            foreach(var n in newNeighbours) {
                valids.AddRange(LoopNeighbours(n, input));
            }
            newNeighbours = valids.Where(x => visited.Contains(x) == false).ToList();
        }
        Console.WriteLine($"Part1: {fartestPoint}");

        //foreach tile that is not in the loop, check if you can get to the edge
        //if not, it could be Bobers nest
        //if yes, add to visited so we don't check those again
        List<(int x, int y)> nest = [];
        for(int i = 1; i < input.Length - 1; i++) 
        {
            for(int j = 1; j < input[0].Length; j++) 
            {
                if(visited.Contains((i,j)) == false)
                {
                    List<(int x, int y)> notVisited = [(i,j)];
                    //add neighbours that are not visited

                }
            }
        }
    }
    public static List<(int x, int y)> GetNeighBours((int x, int y) current) {
        List<(int x, int y)> ret = [];
        ret.Add((current.x+1,current.y));
        ret.Add((current.x-1,current.y));
        ret.Add((current.x,current.y+1));
        ret.Add((current.x,current.y-1));
    }

    public static List<(int x, int y)> LoopNeighbours((int x, int y) current, string[] map) {
        List<(int x, int y)> ret = [];
        char tmp;
        string valids = "";
        if(current.x > 0) {
            tmp = map[current.x-1][current.y];
            valids = "|7F";
            if(valids.Contains(tmp)) {
                ret.Add((current.x-1, current.y));
            } 
        }
        if(current.y < map[0].Length - 1) {
            tmp = map[current.x][current.y+1];
            valids = "-J7";
            if(valids.Contains(tmp)) {
                ret.Add((current.x, current.y+1));
            } 
        }
        if(current.y > 0) {
            tmp = map[current.x][current.y-1];
            valids = "-LF";
            if(valids.Contains(tmp)) {
                ret.Add((current.x, current.y-1));
            } 
        }
        if(current.x < map.Length - 1) {
            tmp = map[current.x+1][current.y];
            valids = "|LJ";
            if(valids.Contains(tmp)) {
                ret.Add((current.x+1, current.y));
            } 
        }
        return ret;
    }
}