public class Decrypt {
    public string[] Input {get;set;}
    private int[] buffer;

    public Decrypt(string[] input) {
        Input = input;
        buffer = Enumerable.Range(0,input.Length).ToArray();
    }
    public void Print() {
        Console.WriteLine();
        for(int i = 0; i < Input.Length; i++) {
            Console.Write((long.Parse(Input[FindValue(i)]) * 811589153) + " ");
        }
    }

    public long GetCoordinates(bool test) {
        //Find index of 0
        int zero = test ? 5 : 4333;
        int zeroIndex = buffer[zero];
        Console.WriteLine($"0 at {zeroIndex}");
        var part1Index =FindValue((zeroIndex+1000)%Input.Length);
        long part1 = long.Parse(Input[part1Index]) * 811589153;
        Console.WriteLine($"{part1} at {(zeroIndex+1000)%Input.Length}");
        var part2Index = FindValue((zeroIndex+2000)%Input.Length);
        long part2 = long.Parse(Input[part2Index]) * 811589153;
        Console.WriteLine($"{part2} at {(zeroIndex+2000)%Input.Length}");
        var part3Index = FindValue((zeroIndex+3000)%Input.Length);
        long part3 = long.Parse(Input[part3Index]) * 811589153;
        Console.WriteLine($"{part3} at {(zeroIndex+3000)%Input.Length}");
        return part1 + part2 + part3;
    }

    public void Loop(bool t) {
        if(t)Print();
        for(int c = 0; c < 10; c++)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                long moves = long.Parse(Input[i]) * 811589153;
                Mix(i, moves);
            }
            if(t)Print();
        }
    }

    private void Mix(int bufferIndex, long moves) {
        var oldPosition = (long)buffer[bufferIndex];
        long newPosition;
        if(moves < 0) {
            moves = buffer.Length - (Math.Abs(moves) % (buffer.Length - 1)) - 1;
        }
        if (moves == 0)
        {
            return;
        }
        if (moves > 0) {
            var skip = moves % (buffer.Length - 1);
            newPosition = (oldPosition+skip) % (buffer.Length - 1); //<-this was %byffer.length for part one but needed to be -1 for part 2 and I have no idea why ¨\_(^_^)_/¨

            //if new > old -> move all between --, else ++
            if(newPosition > oldPosition) {
                for(int i = 0; i < buffer.Length; i++) {
                    if(buffer[i] > oldPosition && buffer[i] <= newPosition)
                        buffer[i]--;
                }
                buffer[bufferIndex] = (int)newPosition;
            }
            else if (oldPosition > newPosition){
                for(int i = 0; i < buffer.Length; i++) {
                    if(buffer[i] < oldPosition && buffer[i] >= newPosition)
                        buffer[i]++;
                }
                buffer[bufferIndex] = (int)newPosition;
            }
            else {
                throw new Exception("awkward...you should not be here");
            }
        }
    }
    private int FindValue(int v) {
        for(int i = 0; i < buffer.Length; i++) {
            if(buffer[i] == v)
                return i;
        }
        throw new Exception("did not find " + v);
    }
}