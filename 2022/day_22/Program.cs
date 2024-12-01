internal class Program
{
    private static void Main(string[] args)
    {
        //parse and init
        bool test = false;
        var input = File.ReadAllLines(test ? "test" : "input");
        int side = test ? 4 : 50;
        string[] board = new string[input.Length-2];
        int max = 0;
        for(int i = 0; i < board.Length; i++) {
            board[i] = input[i];
            if (board[i].Length > max) max = board.Length;
        }
        for(int i = 0; i < board.Length; i++) {
            for(int u = board[i].Length; u < max; u++)
            {
                board[i] += ' ';
            }
        }
        string path = input[^1];
        var steps = path.Split('R','L');
        (int row, int column) position = (0,0);
        int direction = 0;
        for(int i = 0; i < input[0].Length; i++) {
            if(input[0][i] == '.') {
                position.column = i;
                break;
            }
        }

        int j = 0;
        //Console.WriteLine("Initial position " + position.row + ", " + position.column + " and direction " + direction);
        for(int i = 0; i < path.Length; i++) {
            if(path[i] == 'R' || path[i] == 'L') {
                direction = Turn(direction, path[i]);
                Console.Write("Turning " + path[i] + ", new direction " + direction);
            }
            else {
                var step = int.Parse(steps[j]);
                (position, direction) = Move(position,direction,step,board, side);
                Console.WriteLine(", Moving to " + position.row + ", " + position.column);
                i += steps[j].Length - 1;
                j++;
            }
        }
        Console.WriteLine((1000 * (position.row + 1)) + (4 * (position.column + 1)) + direction); //117086 too low
    }

    private static int Turn(int direction, char turnDirection) {
        if(turnDirection == 'R') {
            direction++;
        }
        else {
            direction--;
        }
        if (direction < 0)
        {
            return 3;
        }
        else if (direction > 3)
        {
            return 0;
        }
        return direction;
    }
    private static ((int, int),int) Move((int row, int column) p, int direction, int steps, string[] board, int side) {
        for(int i = 0; i < steps; i++) {
            var next = StepToNext(p, direction, board, side);
            if(next.n == (-1,-1)) break; //wall
            p = next.n;
            direction = next.d;
        }
        return (p,direction);
    }

    private static ((int, int)n,int d) StepToNext((int row, int column) p, int direction, string[] board, int side) {
        (int row, int column) next;
        if(direction == 0) {
            next = (p.row,p.column + 1);
        }
        else if(direction == 1) {
            next = (p.row + 1,p.column);
        }
        else if(direction == 2) {
            next = (p.row,p.column - 1);
        }
        else if(direction == 3) {
            next = (p.row - 1,p.column);
        }
        else {
            throw new Exception("Unknown direction " + direction);
        }

        //Boundary checks, step to next only if its possible
        //make sure we are inside the beard
        if(next.row < 0) {
            //next.row = board.Length-1;
            if(next.column < side) { //G
                next.row = next.column + side;
                next.column = side;
                direction = 0;
            }
            else if(next.column < side * 2) { //E
                next.row = next.column + (side * 2);
                next.column = 0;
                direction = 0;
            }
            else { //C
                next.column -= side * 2;
                next.row = board.Length - 1;
                direction = 3;
            }
        }
        else if(next.row > board.Length-1) {
            //next.row = 0;
            if(next.column < side) { //C
                next.column += 2 * side;
                next.row = 0;
                direction = 1;
            }
            else if(next.column < side * 2) { //D
                next.row = next.column + (2 * side);
                next.column = side - 1;
                direction = 2;
            }
            else { //B
                next.row = next.column - side;
                next.column = (side * 2) - 1;
                direction = 2;
            }
        }
        else if(next.column >= board[next.row].Length) {
            //next.column = 0;
            if(next.row < side) { //A
                next.column = (side * 2) - 1;
                next.row = board.Length - 1 - side - next.row;
                direction = 2;
            }
            else if(next.row < side * 2) { //B
                next.column = next.row + side;
                next.row = side - 1;
                direction = 3;
            }
            else if(next.row < side * 3) { //A
                next.row = board.Length - 1 - next.row - side;
                next.column = (side * 3) - 1;
                direction = 2;
            }
            else { //D
                next.column = next.row - (2 * side);
                next.row = (side * 3) - 1;
                direction = 3; //tarkistettu tähän asti
            }
        }
        else if(next.column < 0) {
            //next.column = board[next.row].Length-1;
            if(next.row < side) { //F
                next.row = board.Length - 1 - side - next.row;
                next.column = 0;
                direction = 0;
            }
            else if(next.row < side * 2) { //G
                next.column = next.row - side;
                next.row = side * 2;
                direction = 1;
            }
            else if(next.row < side * 3) { //F
                next.row = board.Length - 1 - side - next.row; //?
                next.column = side;
                direction = 0;
            }
            else { //E
                next.column = next.row - (2 * side);
                next.row = 0;
                direction = 1;
            }
        }

        //sanity check
        if(next.row < 0 || next.row >= board.Length || next.column < 0 || next.column >= board[next.row].Length) Console.WriteLine("invalid next for ("+p.row + ","+ p.column+"): " + next.row + ", " + next.column);

        //if we hit the wall return invalid position
        if(board[next.row][next.column] == '#') {
            return ((-1,-1),direction);
        }
        //else if we are outside, continue stepping to next until we are back inside or hit the wall
        else if(board[next.row][next.column] == ' ') {
            return StepToNext(next, direction, board, side);
        }
        else {
            //Console.WriteLine("next: " + next.row + ", " + next.column + " dir: " + direction);
            return (next,direction);
        }
    }
}