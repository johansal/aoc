internal class Program
{
    /// <summary>
    /// Yeah it works but not really very nice...
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
        var input = File.ReadAllLines("input");
        var find = "XMAS";
        var part1 = 0;
        
        var reverse = "SAMX";
        for (int i = 0; i < input.Length; i++)
        {       
            var row = input[i];
            //Console.WriteLine(row);
            var index = row.IndexOf(find);
            while (index > -1)
            {
                //Console.WriteLine(index);
                part1++;
                index = row.IndexOf(find, index + 1);
            }
            index = row.IndexOf(reverse);
            while (index > -1)
            {
                //Console.WriteLine(index);
                part1++;
                index = row.IndexOf(reverse, index + 1);
            }
        }
        // Check columns
        //Console.WriteLine("Columns:");
        for (int j = 0; j < input[0].Length; j++)
        {
            string row = "";
            for (int i = 0; i < input.Length; i++)
            {
                row += input[i][j];
            }
            //Console.WriteLine(row);
            var index = row.IndexOf(find);
            while (index > -1)
            {
                //Console.WriteLine(index);
                part1++;
                index = row.IndexOf(find, index + 1);
            }
            index = row.IndexOf(reverse);
            while (index > -1)
            {
                //Console.WriteLine(index);
                part1++;
                index = row.IndexOf(reverse, index + 1);
            }
        }
        // Check \  first half
        //Console.WriteLine("\\ top|");
        for (int jStart = input[0].Length - 1; jStart >= 0; jStart--)
        {
            string row = "";
            int i = 0;
            int j = jStart;
            while ( j < input[0].Length && i < input.Length)
            {
                row += input[i][j];
                i++;
                j++;
            }
            //Console.WriteLine(row);
            var index = row.IndexOf(find);
            while (index > -1)
            {
                //Console.WriteLine(index);
                part1++;
                index = row.IndexOf(find, index + 1);
            }
            index = row.IndexOf(reverse);
            while (index > -1)
            {
                //Console.WriteLine(index);
                part1++;
                index = row.IndexOf(reverse, index + 1);
            }
        }
        // Check \  2nd half
        //Console.WriteLine("\\ |_bottom");
        for (int iStart = 1; iStart < input.Length; iStart++)
        {
            string row = "";
            int i = iStart;
            int j = 0;
            while ( j < input[0].Length && i < input.Length)
            {
                row += input[i][j];
                i++;
                j++;
            }
            //Console.WriteLine(row);
            var index = row.IndexOf(find);
            while (index > -1)
            {
                //Console.WriteLine(index);
                part1++;
                index = row.IndexOf(find, index + 1);
            }
            index = row.IndexOf(reverse);
            while (index > -1)
            {
                //Console.WriteLine(index);
                part1++;
                index = row.IndexOf(reverse, index + 1);
            }
        }
        // Check / first half
        //Console.WriteLine("/ bottom_|");
        for (int jStart = 1; jStart < input[0].Length; jStart++)
        {
            string row = "";
            int i = input.Length - 1;
            int j = jStart;
            while ( j < input[0].Length && i >= 0)
            {
                row += input[i][j];
                i--;
                j++;
            }
            //Console.WriteLine(row);
            var index = row.IndexOf(find);
            while (index > -1)
            {
                //Console.WriteLine(index);
                part1++;
                index = row.IndexOf(find, index + 1);
            }
            index = row.IndexOf(reverse);
            while (index > -1)
            {
                //Console.WriteLine(index);
                part1++;
                index = row.IndexOf(reverse, index + 1);
            }
        }
        // Check / 2nd half
        //Console.WriteLine("/ top|");
        for (int iStart = 0; iStart < input.Length; iStart++)
        {
            string row = "";
            int i = iStart;
            int j = 0;
            while ( j < input[0].Length && i >= 0)
            {
                row += input[i][j];
                i--;
                j++;
            }
            //Console.WriteLine(row);
            var index = row.IndexOf(find);
            while (index > -1)
            {
                //Console.WriteLine(index);
                part1++;
                index = row.IndexOf(find, index + 1);
            }
            index = row.IndexOf(reverse);
            while (index > -1)
            {
                //Console.WriteLine(index);
                part1++;
                index = row.IndexOf(reverse, index + 1);
            }
        }

        Console.WriteLine(part1);//2569

        // Find X-MAS in shape 
        var part2 = 0;

        for (int i = 0; i < input.Length; i++)
        {
            for(int j = 0; j < input[0].Length; j++)
            {
                if('A' == input[i][j])
                {
                    // Chek corners for M&S
                    try {
                    if (('M' == input[i-1][j-1] && 'S' == input[i+1][j+1])||('S' == input[i-1][j-1] && 'M' == input[i+1][j+1]))
                    {
                        if(('M' == input[i+1][j-1] && 'S' == input[i-1][j+1])||('S' == input[i+1][j-1] && 'M' == input[i-1][j+1]))
                        {
                            part2++;
                        }
                    }
                    }
                    catch (Exception)
                    {
                        //This is fine...
                    }
                }
            }
        }
        Console.WriteLine(part2);
    }
}