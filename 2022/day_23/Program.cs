internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        List<(int x, int y)> elfs = new();
        for(int i = 0; i < input.Length; i++) {
            for(int j = 0; j < input[i].Length; j++) {
                if(input[i][j] == '#') elfs.Add((i,j));
            }
        }
        List<(int xDiff, int yDiff)> directions = new()
        {
            (-1, 0),
            (1, 0),
            (0, -1),
            (0, 1)
        };

        //Simulate
        bool elfsMoved = true;
        int round = 0;
        while (elfsMoved) {
            round++;
            var ret = MoveElfs(elfs, directions);
            elfs = ret.Item1;
            elfsMoved = ret.Item2;
            directions = PreferredDirections(directions);
        }
        Console.WriteLine(round);
        //FOR PART 1 with 10 rounds
        /*
        //get area
        var xMin = elfs[0].x;
        var xMax = elfs[0].x;
        var yMin = elfs[0].y;
        var yMax = elfs[0].y;
        for(int i = 0; i < elfs.Count; i++) {
            if(elfs[i].x < xMin) {
                xMin = elfs[i].x;
            }
            else if(elfs[i].x > xMax) {
                xMax = elfs[i].x;
            }
            if(elfs[i].y < yMin) {
                yMin = elfs[i].y;
            }
            else if(elfs[i].y > yMax) {
                yMax = elfs[i].y;
            }
        }

        //Print
        
        Console.WriteLine();
        for(int i = xMin; i <= xMax; i++) {
            for(int j = yMin; j <= yMax; j++) {
                if(elfs.Contains((i,j))) {
                    Console.Write("(" + i + " " + j + ")");
                }
                else {
                    Console.Write('.');
                }
            }
            Console.WriteLine();
        }

        var area = (xMax - xMin + 1) * (yMax - yMin + 1);
        Console.WriteLine("Part 1: " + (area - elfs.Count));
        */
    }

    private static List<(int xDiff,int yDiff)> PreferredDirections(List<(int xDiff,int yDiff)> dir) {
        var f = dir[0];
        dir.RemoveAt(0);
        dir.Add(f);
        return dir;
    }
    private static (List<(int, int)>,bool) MoveElfs(List<(int x, int y)> elfs, List<(int xDiff,int yDiff)> dir) {
        bool elfsMoved = false;
        //find positions that elfs propose
        List<(int x, int y)> proposals = new();
        foreach(var elf in elfs) {
            proposals.Add(ProposeNew(elf,elfs,dir));
        }
        //move elfs, if there is collisions they stay put
        for(int i = 0; i < elfs.Count; i++) {
            if(proposals.FindAll(x => x == proposals[i]).Count == 1) {
                elfsMoved = elfsMoved || elfs[i] != proposals[i];
                elfs[i] = proposals[i];
            }
        }
        return (elfs,elfsMoved);
    }
    private static (int, int) ProposeNew((int x, int y) elf, List<(int x, int y)> elfs, List<(int xDiff,int yDiff)> dir) {
        var neighbours = elfs.Where(e => (e.x >= elf.x-1) && (e.x <= elf.x+1) && (e.y >= elf.y-1) && (e.y <= elf.y+1) && e != elf).ToList();
        if(!neighbours.Any()) {
            return elf;
        }
        foreach(var d in dir) {
            if(CanMove(elf, neighbours, d)) {
                //Console.WriteLine(elf.x + "," + elf.y + "proposes to move " + d.xDiff + ", " + d.yDiff);
                return(elf.x + d.xDiff, elf.y + d.yDiff);
            }
        }
        return elf;
    }
    private static bool CanMove((int x, int y) elf, List<(int x, int y)> n, (int xDiff,int yDiff) direction) {
        (int x, int y) tempElf = (elf.x + direction.xDiff, elf.y + direction.yDiff);
        if(n.Contains(tempElf)) {
            return false;
        }
        else {
            if(direction.xDiff == 0) {
                tempElf.x--;
                if(n.Contains(tempElf)) {
                    return false;
                }
                tempElf.x += 2;
                return !n.Contains(tempElf);
            }
            else if(direction.yDiff == 0) {
                tempElf.y--;
                if(n.Contains(tempElf)) {
                    return false;
                }
                tempElf.y += 2;
                return !n.Contains(tempElf);
            }
            else {
                throw new Exception("wtf?");
            }
        }
    }
}