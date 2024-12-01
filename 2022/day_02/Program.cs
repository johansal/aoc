Dictionary<string, int> Hands = new()
{
    { "A", 1 },
    { "B", 2 },
    { "C", 3 },
    { "X", 0 },
    { "Y", 3 },
    { "Z", 6 }
};

var input = File.ReadAllLines("input");
var score = 0;
foreach(var line in input) {
    var currentHand = line.Split(' ');
    var result = Hands[currentHand[1]];
    var myHand = Hands[currentHand[0]];
    score += result;
    if(result == 0) {
        myHand--;
        if(myHand < 1)
            myHand = 3;
    }
    else if(result == 6)
    {
        myHand++;
        if(myHand > 3)
            myHand = 1;
    }
    score += myHand;
}

Console.WriteLine(score);