namespace day7;

public class Day7 {
    public static void Main() {
        var input = File.ReadAllLines("inputs/input");
        List<PokerHand> hands = [];
        foreach(var line in input) 
        {
            var tmp = line.Split(" ");
            var hand = tmp[0];
            int bet = int.Parse(tmp[1]);
            hands.Add(new(hand,bet));
        }
        var orderedHands = hands.Order().ToList();
        int result = 0;
        for(int i = 1; i <= orderedHands.Count; i++) 
        {
            result += orderedHands[i-1].Bet * i;
        }
        Console.WriteLine(result);
    }
}
public class PokerHand : IComparable
{
    public readonly string Hand;
    public readonly int Bet;
    public readonly int Type;
    private readonly Dictionary<char,int> _hand;
    public PokerHand(string hand, int bet) {
        Hand = hand;
        Bet = bet;
        _hand = new();
        int jokers = 0; //part2
        foreach(var card in hand) {
            if(card == 'J') //part2
                jokers++;
            else if(_hand.TryGetValue(card, out int value)) {
                _hand[card] = ++value;
            }
            else {
                _hand.Add(card,1);
            }
        }
        //five of kind
        if(_hand.Count == 1 && _hand.ContainsValue(5))
            Type = 6;
        //four of kind
        else if(_hand.ContainsValue(4))
            Type = 5;
        //fullhouse or three of kind
        else if(_hand.ContainsValue(3))
        {
            if(_hand.ContainsValue(2))
                Type = 4;
            else
                Type = 3;
        }
        //two pair or pair
        else if(_hand.ContainsValue(2))
        {
            Type = _hand.Count(x => x.Value == 2);
        }
        //shark
        else {
            Type = 0;
        }

        //part2:count jokers to rank
        if(jokers >= 4)
            Type = 6;
        else if(jokers == 3) {
            Type += 5;
        }
        else if(jokers == 2) {
            Type = Type == 1 ? 5 : Type + 3;
        }
        else if(jokers == 1) {
            Type = Type == 0 || Type == 5 ? Type + 1 : Type + 2;
        }
    }
    public int CompareTo(object? obj)
    {
        if(obj == null) {
            throw new ArgumentException("Cannot compare to null");
        }
        PokerHand h2 = (PokerHand) obj;
        if(Type == h2.Type) {
            //var rank = "23456789TJQKA".AsSpan(); part1
            var rank = "J23456789TQKA".AsSpan();
            /* REAL POKER (_hand should be public for this and jokers wont work)
            var sortedHand = from card in _hand orderby card.Value ascending select card;
            var sortedHand2 = from card in h2._hand orderby card.Value ascending select card;
            for(int i = 0; i < sortedHand.Count(); i++)
            {
                var card1 = sortedHand.ElementAt(i).Key;
                var card2 = sortedHand2.ElementAt(i).Key;
                if(card1 != card2)
                    return rank.IndexOf(card1) - rank.IndexOf(card2);
            }*/
            // CAMEL POKER
            for(int i = 0; i < Hand.Length; i++)
            {
                var card1 = Hand.ElementAt(i);
                var card2 = h2.Hand.ElementAt(i);
                if(card1 != card2)
                    return rank.IndexOf(card1) - rank.IndexOf(card2);
            }
            return 0;
        }
        else {
            return Type - h2.Type;
        }
    }
}