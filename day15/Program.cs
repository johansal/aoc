namespace Day15;

public class Program
{
    private static void Main(string[] args)
    {
        StreamReader sr = new(File.OpenRead("inputs/input"));
        int currentHashValue = 0;
        string label = "";
        Box[] boxes = new Box[256];
        for(int i = 0; i < boxes.Length; i++) {
            boxes[i] = new();
        }
        int c = sr.Read();
        while (c != -1)
        {
            if(c == ',')
            {
                /*
                for(int i = 0; i < boxes.Length; i++)
                {
                    if( boxes[i].Lenses.Count > 0)
                        Console.Write($"box {i}: ");
                    for(int j = 0; j < boxes[i].Lenses.Count; j++) 
                    { 
                        Console.Write($"[{boxes[i].Lenses[j].label} {boxes[i].Lenses[j].flength}] ");
                    }
                    if( boxes[i].Lenses.Count > 0)
                        Console.WriteLine();
                }
                Console.WriteLine("");
                */
                label = "";
                currentHashValue = 0;
            }
            else if(c == '-')
            {
                //Console.WriteLine($"Remove {label} from box {currentHashValue}");
                boxes[currentHashValue].Remove(label);
            }
            else if(c == '=')
            {
                int fl = sr.Read() - '0';
                //Console.WriteLine($"Add {label} with focal length {fl} to box {currentHashValue}");
                boxes[currentHashValue].Add(label, fl);
            }
            else {
                label += (char)c;
                currentHashValue = Hash(c, currentHashValue);
            }
            c = sr.Read();
        }
        sr.Close();
        int power = 0;
        for(int i = 0; i < boxes.Length; i++)
        {
            if( boxes[i].Lenses.Count > 0)
                Console.Write($"box {i}: ");
            for(int j = 0; j < boxes[i].Lenses.Count; j++) 
            { 
                Console.Write($"[{boxes[i].Lenses[j].label} {boxes[i].Lenses[j].flength}] ");                
                power += (i+1) * (j+1) * boxes[i].Lenses[j].flength;
            }
            if( boxes[i].Lenses.Count > 0)
                Console.WriteLine();
        }
        Console.WriteLine(power); // < 535787
    }
    private static int Hash(int c, int currentValue) {
        currentValue += c;
        currentValue *= 17;
        currentValue %= 256;
        return currentValue;
    }
    private static void Solve1()
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
}
public class Box {
    public List<(string label, int flength)> Lenses {get; set;}

    public Box()
    {
        Lenses = [];
    }
    public void Remove(string label) {
        int i = Lenses.Count - 1;
        while(i >= 0)
        {
            if(Lenses[i].label.Equals(label))
            {
                Lenses.RemoveAt(i);
                return;
            }
            i--;
        }
    }
    public void Add(string lab, int flength) {
        int i = Lenses.FindIndex(l => l.label.Equals(lab));
        if(i != -1) {
            Lenses[i] = (lab, flength);
        }
        else {
            Lenses.Add((lab, flength));
        }
    }
}