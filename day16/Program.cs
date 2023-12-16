namespace day16;
public class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("inputs/input");
        List<((int x, int y) position ,int direction)> energized = [];
        Dictionary<(int x, int y), bool> e = [];
        (int x, int y) position = (0,0);
        var direction = ChangeDirection(1, input[0][0]); // -> 1, v 2, <- 3, ^ 0
        List<((int x, int y) position, int direction)> beams = [];
        beams.Add((position,direction[0]));
        energized.Add((position, direction[0]));
        e[position] = true;
        while(beams.Count > 0)
        {
            List<((int x, int y) position, int direction)> newBeams = [];
            foreach(var beam in beams) {
                var newPosi = Move(beam.position, beam.direction);
                if(newPosi.x >= 0 && newPosi.x < input.Length && newPosi.y >= 0 && newPosi.y < input[0].Length)
                {
                    var newDirection = ChangeDirection(beam.direction, input[newPosi.x][newPosi.y]);
                    foreach(var d in newDirection)
                    {
                        if(energized.FindIndex(x => x.position == newPosi && x.direction == d) == -1)
                        {
                            newBeams.Add((newPosi, d));
                            energized.Add((newPosi, d));
                            e[newPosi] = true;
                        }
                    }
                }
            }
            beams = newBeams;
        }
        Console.WriteLine(e.Count);
    }
    private static (int x, int y) Move((int x, int y) position, int direction)
    {
        if(direction == 1)
        {
            return (position.x, position.y + 1);
        }
        else if(direction == 2)
        {
            return (position.x + 1, position.y);
        }
        else if(direction == 3)
        {
            return (position.x, position.y - 1);
        }
        else if(direction == 0)
        {
            return (position.x - 1, position.y);
        }
        else 
        {
            throw new NotImplementedException();
        }
    }
    private static List<int> ChangeDirection(int direction, char tile)
    {
        List<int> ret = [];
        if(direction == 1)
        {
            if(tile == '.' || tile == '-')
                ret.Add(direction);
            else if(tile == '/')
                ret.Add(direction - 1);
            else if(tile == '\\')
                ret.Add(direction + 1);
            else if(tile == '|')
            {
                ret.Add(direction - 1);
                ret.Add(direction + 1);
            }
            else 
            {
                throw new NotImplementedException();
            } 
        }
        else if(direction == 2)
        {
            if(tile == '.' || tile == '|')
                ret.Add(direction);
            else if(tile == '/')
                ret.Add(direction + 1);
            else if(tile == '\\')
                ret.Add(direction - 1);
            else if(tile == '-')
            {
                ret.Add(direction - 1);
                ret.Add(direction + 1);
            }
            else 
            {
                throw new NotImplementedException();
            }
        }
        else if(direction == 3)
        {
            if(tile == '.' || tile == '-')
                ret.Add(direction);
            else if(tile == '/')
                ret.Add(direction - 1);
            else if(tile == '\\')
                ret.Add(0);
            else if(tile == '|')
            {
                ret.Add(direction - 1);
                ret.Add(0);
            }
            else 
            {
                throw new NotImplementedException();
            }
        }
        else if(direction == 0)
        {
            if(tile == '.' || tile == '|')
                ret.Add(direction);
            else if(tile == '/')
                ret.Add(direction + 1);
            else if(tile == '\\')
                ret.Add(3);
            else if(tile == '-')
            {
                ret.Add(3);
                ret.Add(direction + 1);
            }
            else 
            {
                throw new NotImplementedException();
            }
        }
        else 
        {
            throw new NotImplementedException();
        } 
        return ret;
    }
}