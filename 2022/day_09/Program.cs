internal static class Program
{
    private static void Main()
    {
        var input = File.ReadAllLines("input");
        var knots = new List<(int,int)>();
        for(int i = 0; i < 10; i++) {
            knots.Add((0,0));
        }
        var tailVisited = new List<(int, int)>
        {
            (0,0)
        };

        foreach (var line in input)
        {
            var cmd = line.Split(" ");
            for(int i = 0; i < int.Parse(cmd[1]); i++) {
                knots[0] = MoveHead(knots[0], cmd[0]);
                for(int k = 0; k < 9; k++) {
                    knots[k+1] = MoveTail(knots[k],knots[k+1]);
                }
                if(!tailVisited.Contains(knots[9]))
                    tailVisited.Add(knots[9]);
            }
        }
        Console.WriteLine(tailVisited.Count);
    }
    private static (int,int) MoveTail((int,int) head, (int,int) tail) {
        var xDiff = head.Item1 - tail.Item1;
        var yDiff = head.Item2 - tail.Item2;
        if(xDiff > 1) {
            tail.Item1++;
            if(yDiff > 0) {
                tail.Item2++;
            }
            else if(yDiff < 0) {
                tail.Item2--;
            }
        }
        else if(xDiff < -1) {
            tail.Item1--;
            if(yDiff > 0) {
                tail.Item2++;
            }
            else if(yDiff < 0) {
                tail.Item2--;
            }
        }
        else if(yDiff > 1) {
            tail.Item2++;
            if(xDiff > 0) {
                tail.Item1++;
            }
            else if(xDiff < 0) {
                tail.Item1--;
            }
        }
        else if(yDiff < -1) {
            tail.Item2--;
            if(xDiff > 0) {
                tail.Item1++;
            }
            else if(xDiff < 0) {
                tail.Item1--;
            }
        }
        return tail;
    }
    private static (int,int) MoveHead((int,int) position, string direction) {
        switch(direction) {
            case "U" :
                position.Item1--;
                break;
            case "D" :
                position.Item1++;
                break;
            case "L" :
                position.Item2--;
                break;
            case "R" :
                position.Item2++;
                break;
        }
        return position;
    }
}