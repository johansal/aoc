internal class Program
{
    private static void Main(string[] args)
    {
        StreamReader sr = new(File.OpenRead("inputs/input"));
        int sum = 0;
        int currentValue = 0;
        int c = sr.Read();
        while (c != -1)
        {
            if(c == ',')
            {
                //Console.WriteLine(currentValue);
                sum += currentValue;
                currentValue = 0;
            }
            else {
                currentValue = Hash(c, currentValue);
            }
            c = sr.Read();
        }
        sum += currentValue;
        Console.WriteLine(sum);
        sr.Close();
    }
    private static int Hash(int c, int currentValue) {
        currentValue += c;
        currentValue *= 17;
        currentValue %= 256;
        return currentValue;
    }
}