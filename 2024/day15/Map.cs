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
        public bool Move(char dir, (int i, int j, char c) cur) {
            int dx;
            int dy;
            if(dir == '>')
            {
                dx = 1;
                dy = 0;
            }
            else if(dir == '<')
            {
                dx = -1;
                dy = 0;
            }
            else if(dir == 'v')
            {
                dy = 1;
                dx = 0;
            }
            else if(dir == '^')
            {
                dy = -1;
                dx = 0;
            }
            else {
                throw new Exception($"Unsupported direction {dir}!");
            }
            (int i, int j) next = (cur.i+dy, cur.j+dx);
            if(Edges.Contains(next))
            {
                return false;
            }
            else if(Spaces.Contains(next))
            {
                Spaces.Remove(next);
                Spaces.Add((cur.i,cur.j));
                if(cur.c == '@')
                {
                    Robot = next;
                }
                else {
                    Boxes.Remove((cur.i,cur.j));
                    Boxes.Add(next);
                }
                return true;
            }
            else if (Boxes.Contains(next) && Move(dir, (next.i,next.j, 'O'))){
                Spaces.Remove(next);
                Spaces.Add((cur.i,cur.j));
                if(cur.c == '@')
                {
                    Robot = next;
                }
                else {
                    Boxes.Remove((cur.i,cur.j));
                    Boxes.Add(next);
                }
                return true;
            }
            else {
                return false;
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
                        Console.Write('O');
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