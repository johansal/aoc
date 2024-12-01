internal class Program
{
    private static void Main(string[] args)
    {
        //test - input values
        string input = "input"; // test || input

        //parse input to 2d int array
        var originalMap = File.ReadAllLines(input);
        (int x,int y) start = (0,0);
        (int x,int y) end = (0,0);
        int[][] heightMap = new int[originalMap.Length][];
        int h = originalMap.Length;
        int l = originalMap[0].Length;
        int count = h * l;
        for(var i = 0; i < heightMap.Length; i++) {
            heightMap[i] = new int[originalMap[i].Length];
            for(var j = 0; j < originalMap[i].Length; j++) {
                if(originalMap[i][j] == 'S') {
                    start = (i,j);
                    heightMap[i][j] = 'a';
                }
                else if(originalMap[i][j] == 'E') {
                    end = (i,j);
                    heightMap[i][j] = 'z';
                }
                else {
                    heightMap[i][j] = originalMap[i][j];
                }
            }
        }
        //Console.WriteLine("Part 1: " + Dijkstra(heightMap, start)[(end.x * l) + end.y]); //get distance to end location
        var part2distances = Dijkstra(heightMap, end); //get distances from end location to all other locations
        int distanceEndToClosestA = int.MaxValue;
        for(int i = 0; i < count; i++) {
            if(heightMap[i/l][i%l] == 'a') {
                distanceEndToClosestA = part2distances[i] < distanceEndToClosestA ? part2distances[i] : distanceEndToClosestA;
            }
        }
        Console.WriteLine("Part 2: " + distanceEndToClosestA);
    }

    private static int[] Dijkstra(int[][] input, (int x,int y) start) {
        int h = input.Length;
        int l = input[0].Length;
        int count = h * l;
        int[] distance = new int[count];
        bool[] sptSet = new bool[count];
        for (var i = 0; i < count; i++)
        {
            distance[i] = int.MaxValue; //init distance with int.Max so first path will always be less than this
            sptSet[i] = false; //init each node as not checked
        }
        //set distance of start to zero since we are there already
        distance[(start.x * l) + start.y] = 0;

        //find minimum distance for all nodes - start
        for (var c = 0; c < count-1; c++)
        {
            //get node u which has minimum distance but adjacent nodes hasn't been checked yet
            var u = MinimumDistance(distance, sptSet, count);
            sptSet[u] = true; //u is checked after this round, no need to check it anymore

            //check adjacent nodes (v) of u that have not been checked yet and calculate their distance
            for (var v = 0; v < count; v++)
            {
                if (!sptSet[v] && IsAdjacent(u, v, l))
                {
                    //we can access adjacent only if its hight is same, lower or max +1 to current height
                    //this check makes this function not reusable :(
                    var heightDiff = input[v/l][v%l] - input[u/l][u%l];
                    //if(heightDiff < 2) { //Part1
                    if(heightDiff > -2) { //Part2
                        var tmp = distance[u] + 1; //update distance only if it was less than some previous distance too this node
                        distance[v] = tmp < distance[v] ? tmp : distance[v];
                    }
                }
            }
        }
        return distance;
    }

    private static bool IsAdjacent(int current, int other, int columns)
    {
        return (other - 1 == current && other % columns != 0) ||
        (other + 1 == current && current % columns != 0) ||
        other - columns == current ||
        other + columns == current;
    }
    private static int MinimumDistance(int[] dist, bool[] sptSet, int count)
    {
        int min = int.MaxValue;
        int minIndex = 0;

        for (int v = 0; v < count; ++v)
        {
            if (!sptSet[v] && dist[v] < min)
            {
                min = dist[v];
                minIndex = v;
            }
        }
        return minIndex;
    }
}