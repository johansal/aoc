public class Crt {
    public Crt() {
        this.pixels = new char[6][];
        for(var i = 0; i < 6; i++) {
            this.pixels[i] = new char[40];
        }
    }
    public char[][] pixels;
    public void Draw(int cycle, int middle) {
        int row = cycle / 40;
        int column = cycle % 40;
        if(!(column > (middle + 1) || column < (middle - 1)))
            pixels[row][column] = '*';
        else {
            pixels[row][column] = ' ';
        }
    }
    public void Print() {
        for(var i = 0; i < 6; i++) {
            Console.WriteLine();
            for(int j = 0; j < 40; j++)
            {
                Console.Write(pixels[i][j]);
            }
        }
        Console.WriteLine();
    }
}