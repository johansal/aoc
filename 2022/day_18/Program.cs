internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        List<(int x, int y, int z)> cubes = new();
        int xMax = int.MinValue;
        int xMin = int.MaxValue;
        int yMax = int.MinValue;
        int yMin = int.MaxValue;
        int zMax = int.MinValue;
        int zMin = int.MaxValue;
        foreach (var line in input)
        {
            var tmp = line.Split(',');
            (int x, int y, int z) cube = new();
            cube.x = int.Parse(tmp[0]);
            cube.y = int.Parse(tmp[1]);
            cube.z = int.Parse(tmp[2]);
            cubes.Add(cube);
            xMax = cube.x > xMax ? cube.x : xMax;
            yMax = cube.y > yMax ? cube.y : yMax;
            zMax = cube.z > zMax ? cube.z : zMax;
            xMin = cube.x < xMin ? cube.x : xMin;
            yMin = cube.y < yMin ? cube.y : yMin;
            zMin = cube.z < zMin ? cube.z : zMin;
        }
        /*
        int[][][] obsidian = new int[max][][];
        for(int i = 0; i <= max; i++) {
            obsidian[i] = new int[max][];
            for(int j = 0; j <= max; j++) {
                obsidian[i][j] = new int[max];
                for(int u = 0; u <= max; u++) {
                    if(cubes.Contains())
                }
            }
        }
        */
        //count sides that are exposed
        int exposedSides = 0;
        List<(int x, int y , int z)> missingCube = new();
        for(int i = 0; i < cubes.Count; i++) {
            var tmp = ExposedSides(cubes[i], cubes);
            exposedSides += tmp.Count;
            missingCube = missingCube.Union(tmp).ToList();
        }
        Console.WriteLine("part 1: " + exposedSides);
        List<(int x, int y , int z)> exteriorCubes = new();
        List<(int x, int y , int z)> airPockets = new();

        for(int i = 0; i < missingCube.Count; i++) {
            //is missing cube outside obsidian?
            if(IsExteriorCube(missingCube[i], cubes))
            {
                exteriorCubes.Add(missingCube[i]);
                //Console.WriteLine("Exterior cube at " + missingCube[i].x + "," + missingCube[i].y + "," +missingCube[i].z);
            }
            else {
                airPockets.Add(missingCube[i]);
                //Console.WriteLine("Air pocket at " + missingCube[i].x + "," + missingCube[i].y + "," +missingCube[i].z);
            }

        }

        bool changes = true;
        while(changes) {
            changes = false;
            List<(int x, int y , int z)> newAirPocets = new();
            foreach(var airpocket in airPockets) {
                if(IsAdjacent(airpocket, exteriorCubes)) {
                    exteriorCubes.Add(airpocket);
                    changes = true;
                }
                else {
                    newAirPocets.Add(airpocket);
                }
            }
            airPockets = newAirPocets;
        }
        exposedSides = 0;
        foreach(var c in exteriorCubes) {
            exposedSides += 6 - ExposedSides(c, cubes).Count;
        }
        Console.WriteLine("part 2: " + exposedSides); //4126 too high, 372 too low
    }
    private static List<(int x, int y , int z)> ExposedSides((int x, int y, int z) cube, List<(int x, int y, int z)> cubes) {
        List<(int x, int y, int z)> neighbours = new();
        neighbours.Add((cube.x-1,cube.y,cube.z));
        neighbours.Add((cube.x+1,cube.y,cube.z));
        neighbours.Add((cube.x,cube.y-1,cube.z));
        neighbours.Add((cube.x,cube.y+1,cube.z));
        neighbours.Add((cube.x,cube.y,cube.z-1));
        neighbours.Add((cube.x,cube.y,cube.z+1));

       return neighbours.Except(cubes).ToList();
    }
    private static bool IsExteriorCube((int x, int y, int z) cube, List<(int x, int y, int z)> cubes) {
        if(!cubes.Where(c => c.x<cube.x && c.y == cube.y && c.z == cube.z).Any()) {
            return true;
        }
        else if(!cubes.Where(c => c.x>cube.x && c.y == cube.y && c.z == cube.z).Any()) {
            return true;
        }
        else if(!cubes.Where(c => c.x == cube.x && c.y < cube.y && c.z == cube.z).Any()) {
            return true;
        }
        else if(!cubes.Where(c => c.x == cube.x && c.y > cube.y && c.z == cube.z).Any()) {
            return true;
        }
        else if(!cubes.Where(c => c.x == cube.x && c.y == cube.y && c.z < cube.z).Any()) {
            return true;
        }
        else if(!cubes.Where(c => c.x == cube.x && c.y == cube.y && c.z > cube.z).Any()) {
            return true;
        }
        return false;
    }

    private static bool IsAdjacent((int x, int y, int z) cube, List<(int x, int y, int z)> cubes) {
        List<(int x, int y, int z)> neighbours = new();
        neighbours.Add((cube.x-1,cube.y,cube.z));
        neighbours.Add((cube.x+1,cube.y,cube.z));
        neighbours.Add((cube.x,cube.y-1,cube.z));
        neighbours.Add((cube.x,cube.y+1,cube.z));
        neighbours.Add((cube.x,cube.y,cube.z-1));
        neighbours.Add((cube.x,cube.y,cube.z+1));

        return cubes.Intersect(neighbours).ToList().Count > 0;
    }
}