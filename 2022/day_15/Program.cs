internal class Program
{
    private static void Main(string[] args)
    {
        const bool test = false;
        var input = File.ReadAllLines(test? "test" : "input");
        int min = 0;
        int max = test ? 20 : 4000001;

        List<(int x, int y, int strength)> sensors = new();
        List<(int x, int y)> beacons = new();
        foreach (var line in input)
        {
            var splitted = line.Split(new char[] { ',', ':', '=', ' ' });
            int sensorX = int.Parse(splitted[3]);
            int sensorY = int.Parse(splitted[6]);
            int beaconX = int.Parse(splitted[13]);
            int beaconY = int.Parse(splitted[16]);
            sensors.Add((sensorY, sensorX, ManhattanDistance((sensorY,sensorX),(beaconY,beaconX))));
            beacons.Add((beaconY, beaconX));
        }
        Console.WriteLine("Parsing ready...");

        //find a list of points around each sensor
        //remove points that are included in other sensor areas
        //should result in one point inside search area :)
        List<(int,int)> points = new();
        foreach (var (y, x, strength) in sensors) {
            //walk thhe diamond
            int j = y-(strength+1);
            int i = x;
            while(i <= x+strength+1 && j <= y) {
                if(j>=min && j<max && i >= min && i < max)
                    points.Add((j,i));
                i++;
                j++;
            }
            while(i >= x && j <= y+strength+1) {
                if(j>=min && j<max && i >= min && i < max)
                    points.Add((j,i));
                i--;
                j++;
            }
            while(i >= x-(strength+1) && j >= y) {
                if(j>=min && j<max && i >= min && i < max)
                    points.Add((j,i));
                i--;
                j--;
            }
            while(i <= x && j >= y-(strength+1)) {
                if(j>=min && j<max && i >= min && i < max)
                    points.Add((j,i));
                i++;
                j--;
            }
            if(j>=min && j<max && i >= min && i < max)
                    points.Add((j,i));
        }
        Console.WriteLine("Walking ready...");
        foreach(var (y, x) in points) {
            var found = false;
            foreach(var sensor in sensors) {
                if(InRange((y,x),sensor)) {
                    found = true;
                    break;
                }
            }
            if(!found) {
                Console.WriteLine(y + " " + x);
                string a = (4*x).ToString();
                a += "000000";
                string b = y.ToString();
                int diff = a.Length-b.Length;
                for(int i = 0; i < diff; i++) {
                    b = " " + b;
                }

                Console.WriteLine(a);
                Console.WriteLine(b);
                break;
            }
        }
    }
    private static bool InRange((int x, int y) point, (int x, int y, int strength) sensor) {
        return ManhattanDistance(point, (sensor.x, sensor.y)) <= sensor.strength;
    }

    private static int ManhattanDistance((int x, int y) start, (int x, int y) end) {
        int distance = start.x > end.x ? start.x - end.x : end.x - start.x;
        distance += start.y > end.y ? start.y - end.y : end.y - start.y;
        return distance;
    }
}