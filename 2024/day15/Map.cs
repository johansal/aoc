namespace day15
{
    public class Map(int scale)
    {
        private int scale = scale;
        private (int h, int w) mapSize = (-1,-1);
        public List<(int i, int j)> Edges = [];
        public List<(int i, int j)> Boxes = [];
        public List<(int i, int j)> Spaces = [];
        public (int i, int j) Robot = (-1,-1);

        public void Add(char cur, int i, int j) {
            j *= scale;
            var limit = j + scale;
            if(cur == '#')
            {
                for(var jScale = j; jScale < limit; jScale++)
                {
                    Edges.Add((i,jScale));
                }
            }
            else if(cur == 'O')
            {
                Boxes.Add((i,j));
            }
            else if(cur == '.')
            {
                for(var jScale = j; jScale < limit; jScale++)
                {
                    Spaces.Add((i,jScale));
                }
            }
            else if(cur == '@')
            {
                Robot = (i,j);
                if(scale == 2)
                {
                    Spaces.Add((i,j+1));
                }
            }

            //Update map size
            if(i > mapSize.h)
                mapSize.h = i;
            if(j + scale > mapSize.w)
                mapSize.w = j + scale;
        }
        private (int i, int j, char c) GetNext(int dir, int i, int j)
        {
            int dx = 0;
            int dy = 0;
            if(dir == '>')
            {
                dx = 1;
            }
            else if(dir == '<')
            {
                dx = -1;
            }
            else if(dir == 'v')
            {
                dy = 1;
            }
            else if(dir == '^')
            {
                dy = -1;
            }
            else {
                throw new Exception($"Unsupported direction {dir}!");
            }
            i += dy;
            j += dx;
            char c;
            if(Spaces.Contains((i,j)))
            {
                c = '.';
            }
            else if(Edges.Contains((i,j)))
            {
                c = '#';
            }
            else if(scale == 1 && Boxes.Contains((i,j)))
            {
                c = 'O';
            }
            else if(Boxes.Contains((i,j)))
            {
                c = '[';
            }
            else if(Boxes.Contains((i,j-1)))
            {
                c = ']';
            }
            else {
                throw new Exception($"Unsupported char at {i}, {j}!");
            }     
            return (i, j, c);
        }
        public bool CanMove(char dir, (int i, int j, char c) cur) {
            if(cur.c == '.')
            {
                return true;
            }
            else if(cur.c == '#')
            {
                return false;
            }
            else if(cur.c == '@')
            {
                return CanMove(dir, GetNext(dir,cur.i,cur.j));
            }
            else 
            {
                // This is a box
                if(dir == '<' || dir == '>')
                {
                    return CanMove(dir, GetNext(dir,cur.i,cur.j));
                }
                else {
                    if(cur.c == '[')
                    {
                        return CanMove(dir, GetNext(dir,cur.i,cur.j)) && CanMove(dir, GetNext(dir,cur.i,cur.j+1));
                    }
                    else {
                        return CanMove(dir, GetNext(dir,cur.i,cur.j)) && CanMove(dir, GetNext(dir,cur.i,cur.j-1));
                    }
                }
            }
        }
        public void Move(char dir, (int i, int j, char c) cur) 
        {
            if(cur.c == '.' || cur.c == '#')
            {
                throw new Exception($"Cannot move immovable {cur.c}!");
            }
            else if(cur.c == '@')
            {
                var next = GetNext(dir, cur.i, cur.j);
                if(next.c != '.')
                {
                    Move(dir, next);
                }
                Spaces.Remove((next.i,next.j));
                Spaces.Add((cur.i,cur.j));
                Robot = (next.i,next.j);
                return;
            }
            else if(cur.c == '[')
            {
                // This is a box
                if(dir == '<')
                {
                    var next = GetNext(dir, cur.i, cur.j);
                    if(next.c != '.')
                    {
                        Move(dir, next);
                    }
                    Spaces.Remove((next.i,next.j));
                    Spaces.Add((cur.i,cur.j+1));
                    Boxes.Remove((cur.i,cur.j));
                    Boxes.Add((next.i,next.j));
                    return;
                }
                else if(dir == '>') {
                    var next = GetNext(dir, cur.i, cur.j+1);
                    if(next.c != '.')
                    {
                        Move(dir, next);
                    }
                    Spaces.Remove((next.i,next.j));
                    Spaces.Add((cur.i,cur.j));
                    Boxes.Remove((cur.i,cur.j));
                    Boxes.Add((cur.i,cur.j+1));
                    return;
                }
                else {
                    var next = GetNext(dir, cur.i, cur.j);
                    if(next.c == '[')
                    {
                        Move(dir, next);
                    }
                    else if(next.c == ']')
                    {
                        Move(dir, (next.i,next.j-1,'['));
                    }
                    var n = GetNext(dir, cur.i, cur.j+1);
                    if(n.c == '[')
                    {
                        Move(dir, n);
                    }
                    Spaces.Remove((next.i,next.j));
                    Spaces.Remove((next.i,next.j+1));
                    Spaces.Add((cur.i,cur.j));
                    Spaces.Add((cur.i,cur.j+1));
                    Boxes.Remove((cur.i,cur.j));
                    Boxes.Add((next.i,next.j));
                    return;
                }
            }
            else {
                Move(dir, (cur.i,cur.j-1,'['));
                return;
            }
        }
        public void Print()
        {
            for(int i = 0; i <= mapSize.h; i++)
            {
                for(int j = 0; j < mapSize.w; j++)
                {
                    if(Edges.Contains((i,j)))
                    {
                        Console.Write('#');
                    }
                    else if(Boxes.Contains((i,j)))
                    {
                        if(scale == 1)
                            Console.Write('O');
                        else if(scale == 2)
                            Console.Write('[');
                    }
                    else if(Spaces.Contains((i,j)))
                    {
                        Console.Write('.');
                    }
                    else if (Robot.i == i && Robot.j == j) {
                        Console.Write('@');
                    }
                    else {
                        Console.Write(']');
                    }
                }
                Console.Write("\n");
            }
            Console.WriteLine();
        }
    }
}