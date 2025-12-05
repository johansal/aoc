namespace day18_snailfish;
public class SnailfishNumber
{
    public SnailfishNumber? Left { get; private set; }
    public SnailfishNumber? Right { get; private set; }
    public int? RegularValue { get; private set; }
    public bool IsRegular => RegularValue.HasValue;

    public SnailfishNumber(int value)
    {
        RegularValue = value;
    }

    public SnailfishNumber(SnailfishNumber left, SnailfishNumber right)
    {
        Left = left;
        Right = right;
    }
    public override string ToString()
    {
        if(IsRegular)
        {
            return RegularValue.ToString()!;
        }
        return $"[{Left},{Right}]";
    }
    public int Magnitude()
    {
        if (IsRegular)
        {
            return RegularValue!.Value;
        }
        return 3 * Left!.Magnitude() + 2 * Right!.Magnitude();
    }
    private SnailfishNumber Reduce()
    {
        bool wasReduced = true;
        while (wasReduced)
        {
            wasReduced = TryExplode() || TrySplit();
        }
        return this;
    }

    private bool TrySplit()
    {
        // If this is a regular number and >= 10, split it
        if (IsRegular && RegularValue! >= 10)
        {
            int leftValue = RegularValue.Value / 2;
            int rightValue = (RegularValue.Value + 1) / 2; // ceiling division
            Left = new SnailfishNumber(leftValue);
            Right = new SnailfishNumber(rightValue);
            RegularValue = null;
            return true;
        }
        if (Left != null && Left.TrySplit())
        {
            return true;
        }
        if (Right != null && Right.TrySplit())
        {
            return true;
        }
        return false;
    }

    private bool TryExplode()
    {
        return TryExplode(this, 0, out _, out _);
    }

    private static bool TryExplode(SnailfishNumber node, int depth, out int? leftCarry, out int? rightCarry)
    {
        leftCarry = null;
        rightCarry = null;

        if (node.IsRegular)
        {
            return false;
        }

        if (depth == 4)
        {
            leftCarry = node.Left!.RegularValue;
            rightCarry = node.Right!.RegularValue;
            node.Left = null;
            node.Right = null;
            node.RegularValue = 0;
            return true;
        }

        // Try to explode left child
        if (TryExplode(node.Left!, depth + 1, out var leftFromLeft, out var rightFromLeft))
        {
            if (rightFromLeft.HasValue)
            {
                // Add to the leftmost regular number in the right child
                AddToLeftmost(node.Right!, rightFromLeft.Value);
            }
            leftCarry = leftFromLeft;
            return true;
        }

        // Try to explode right child
        if (TryExplode(node.Right!, depth + 1, out var leftFromRight, out var rightFromRight))
        {
            if (leftFromRight.HasValue)
            {
                // Add to the rightmost regular number in the left child
                AddToRightmost(node.Left!, leftFromRight.Value);
            }
            rightCarry = rightFromRight;
            return true;
        }

        return false;
    }

    private static void AddToRightmost(SnailfishNumber snailfishNumber, int value)
    {
        if (snailfishNumber.IsRegular)
        {
            snailfishNumber.RegularValue += value;
        }
        else
        {
            AddToRightmost(snailfishNumber.Right!, value);
        }
    }

    private static void AddToLeftmost(SnailfishNumber snailfishNumber, int value)
    {
        if (snailfishNumber.IsRegular)
        {
            snailfishNumber.RegularValue += value;
        }
        else
        {
            AddToLeftmost(snailfishNumber.Left!, value);
        }
    }

    public static SnailfishNumber operator +(SnailfishNumber a, SnailfishNumber b)
    {
        var result = new SnailfishNumber(a, b);
        return result.Reduce();
    }
}
