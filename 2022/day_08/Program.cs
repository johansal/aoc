internal class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        int[][] woods = new int[input.Length][];
        for (int i = 0; i < woods.Length; i++)
        {
            woods[i] = new int[woods.Length];
            for (int j = 0; j < woods.Length; j++)
            {
                woods[i][j] = int.Parse(input[i][j].ToString());
            }
        }
        int visible = 0;
        int scenicScore = 0;
        for (int i = 0; i < woods.Length; i++)
        {
            for (int j = 0; j < woods.Length; j++)
            {
                int score = ScenicScore(woods, i, j);
                if(score > scenicScore)
                    scenicScore = score;
                if(IsVisible(woods, i, j))
                    visible++;
            }
        }
        Console.WriteLine(scenicScore);
    }
    private static int ScenicScore(int[][] woods, int x, int y) {
        if(x == 0 || y == 0 || x == woods.Length-1 || y == woods.Length-1)
            return 0;
        int scenicScoreLeft = 0;
        int tree = woods[x][y];
        for(int i = x-1; i >= 0; i--) {
            scenicScoreLeft++;
            if(woods[i][y] >= tree) {
                break;
            }
        }

        int scenicScoreRight = 0;
        for(int i = x+1; i < woods.Length; i++) {
            scenicScoreRight++;
            if(woods[i][y] >= tree) {
                break;
            }
        }

        int scenicScoreUp = 0;
        for(int j = y-1; j >= 0; j--) {
            scenicScoreUp++;
            if(woods[x][j] >= tree) {
                break;
            }
        }

        int scenicScoreDown = 0;
        for(int j = y+1; j < woods.Length; j++) {
            scenicScoreDown++;
            if(woods[x][j] >= tree) {
                break;
            }
        }
        return scenicScoreDown * scenicScoreLeft * scenicScoreRight * scenicScoreUp;
    }
    private static bool IsVisible(int[][] woods, int x, int y) {
        bool isVisible = true;
        int tree = woods[x][y];
        for(int i = 0; i < x; i++) {
            if(woods[i][y] >= tree) {
                isVisible = false;
                break;
            }
        }
        if(isVisible)
            return true;
        isVisible = true;
        for(int i = x+1; i < woods.Length; i++) {
            if(woods[i][y] >= tree) {
                isVisible = false;
                break;
            }
        }
        if(isVisible)
            return true;
        isVisible = true;
        for(int j = 0; j < y; j++) {
            if(woods[x][j] >= tree) {
                isVisible = false;
                break;
            }
        }
        if(isVisible)
            return true;
        isVisible = true;
        for(int j = y+1; j < woods.Length; j++) {
            if(woods[x][j] >= tree) {
                isVisible = false;
                break;
            }
        }
        return isVisible;
    }
}