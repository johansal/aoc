internal class Program
{
    private static void Main(string[] args)
    {
        char[][] tunnel = new char[1000][];//this could be max value in vector (X,Y)
        //init rows
        for(int i = 0; i < tunnel.Length; i++) {
            tunnel[i] = new char[1000]; //this could be max value in vector (X,Y)
            for(int j = 0; j < tunnel[i].Length; j++) {
                tunnel[i][j] = '.';
            }
        }

        //parse input and mark walls in tunnel
        int highestX = 0;
        foreach(var line in File.ReadAllLines("input")) {
            var points = line.Split(" -> ");
            for(int i = 0; i < points.Length - 1; i++) {
                var point1 = Parse(points[i]);
                var point2 = Parse(points[i+1]);
                var start = (point1.Item1<point2.Item1 ? point1.Item1 : point2.Item1, point1.Item2<point2.Item2 ? point1.Item2 : point2.Item2);
                var end = (point1.Item1>point2.Item1 ? point1.Item1 : point2.Item1, point1.Item2>point2.Item2 ? point1.Item2 : point2.Item2);
                for(int x = start.Item1; x <= end.Item1; x++) {
                    for(int y = start.Item2; y <= end.Item2; y++) {
                        tunnel[x][y] = '#';
                        highestX = x > highestX ? x : highestX;
                    }
                }
            }
        }
        highestX += 2;
        for(int i = 0; i < tunnel[highestX].Length; i++) {
            tunnel[highestX][i] = '#';
        }

        //simulate
        int sandCount = 0;
        bool overflow = false;
        while(!overflow) {
            var sand = (0,500);
            sand = Collision(sand, tunnel);
            if(sand == (0,500)) {
                overflow = true;
            }
            tunnel[sand.Item1][sand.Item2] = 'O';
            sandCount++;
        }
        Console.WriteLine("part 1: " + sandCount);
    }
    private static (int,int) Parse(string point) {
        var p = point.Split(',');
        var x = int.Parse(p[1]);
        var y = int.Parse(p[0]);
        return (x,y);
    }
    private static bool OutOfBounds((int x, int y) p) {
        return 1000 <= p.x || 1000 <= p.y || p.y < 0 || p.x < 0;
    }
    private static (int, int) Collision((int x, int y) p, char[][] tunnel) {
        p.x++;
        if(OutOfBounds(p)) {
            return p;
        }
        else if (tunnel[p.x][p.y] == '.') {
            return Collision((p.x,p.y), tunnel);
        }
        else if (OutOfBounds((p.x,p.y-1))) {
            return (p.x,p.y-1);
        }
        else if(tunnel[p.x][p.y-1] == '.') {
            return Collision((p.x,p.y-1), tunnel);
        }   
        else if (OutOfBounds((p.x,p.y+1))) {
            return (p.x,p.y+1);
        }
        else if(tunnel[p.x][p.y+1] == '.') {
            return Collision((p.x,p.y+1), tunnel);
        }
        else {
            return (p.x-1,p.y);
        }
    }
}