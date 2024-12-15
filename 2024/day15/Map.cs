namespace day15
{
    public class Map
    {
        public List<(int i, int j)> Edges = [];
        public List<(int i, int j)> Boxes = [];
        public List<(int i, int j)> Spaces = [];
        public (int i, int j) Robot = (-1,-1);

        public void Add(char cur, int i, int j, int scale) {
            if(cur == '#')
            {
                Edges.Add((i,j));
            }
            else if(cur == 'O')
            {
                Boxes.Add((i,j));
            }
            else if(cur == '.')
            {
                Spaces.Add((i,j));
            }
            else if(cur == '@')
            {
                Robot = (i,j);
            }
        }
        public void Move(char c) {
            if(c == '>')
            {
                var edge = Edges.Where(e => e.i == Robot.i && e.j > Robot.j).OrderBy(e => e.j).First();
                try {
                    var space = Spaces.Where(s => s.i == Robot.i && s.j > Robot.j && s.j < edge.j).OrderBy(s => s.j).First();
                    Spaces.Remove(space);
                    Spaces.Add(Robot);
                    if(Robot.j + 1 == space.j)
                    {              
                        Robot = space;
                    }
                    else {
                        //Move box
                        var box = (Robot.i, Robot.j + 1);
                        Boxes.Remove(box);
                        Boxes.Add(space);
                        Robot = box;
                    }
                }
                catch(InvalidOperationException)
                {
                    //No space, do nothing
                }
            }
            else if(c == '<')
            {
                var edge = Edges.Where(e => e.i == Robot.i && e.j < Robot.j).OrderByDescending(e => e.j).First();
                try {
                    var space = Spaces.Where(s => s.i == Robot.i && s.j < Robot.j && s.j > edge.j).OrderByDescending(s => s.j).First();
                    Spaces.Remove(space);
                    Spaces.Add(Robot);
                    if(Robot.j - 1 == space.j)
                    {              
                        Robot = space;
                    }
                    else {
                        //Move box
                        var box = (Robot.i, Robot.j - 1);
                        Boxes.Remove(box);
                        Boxes.Add(space);
                        Robot = box;
                    }
                }
                catch(InvalidOperationException)
                {
                    //No space, do nothing
                }
            }
            else if(c == 'v')
            {
                var edge = Edges.Where(e => e.j == Robot.j && e.i > Robot.i).OrderBy(e => e.j).First();
                try {
                    var space = Spaces.Where(s => s.j == Robot.j && s.i > Robot.i && s.i < edge.i).OrderBy(s => s.i).First();
                    Spaces.Remove(space);
                    Spaces.Add(Robot);
                    if(Robot.i + 1 == space.i)
                    {              
                        Robot = space;
                    }
                    else {
                        //Move box
                        var box = (Robot.i + 1, Robot.j);
                        Boxes.Remove(box);
                        Boxes.Add(space);
                        Robot = box;
                    }
                }
                catch(InvalidOperationException)
                {
                    //No space, do nothing
                }
            }
            else if(c == '^')
            {
                var edge = Edges.Where(e => e.j == Robot.j && e.i < Robot.i).OrderByDescending(e => e.i).First();
                try {
                    var space = Spaces.Where(s => s.j == Robot.j && s.i < Robot.i && s.i > edge.i).OrderByDescending(s => s.i).First();
                    Spaces.Remove(space);
                    Spaces.Add(Robot);
                    if(Robot.i - 1 == space.i)
                    {              
                        Robot = space;
                    }
                    else {
                        //Move box
                        var box = (Robot.i - 1, Robot.j);
                        Boxes.Remove(box);
                        Boxes.Add(space);
                        Robot = box;
                    }
                }
                catch(InvalidOperationException)
                {
                    //No space, do nothing
                }
            }
        }
    }
}