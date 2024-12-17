using static day17.Program;

namespace day17Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        int[] reg = [0,0,9];
        var program = "2,6";
        var result = Run(program, reg);

        Assert.Equal(1, reg[1]);
    }
    [Fact]
    public void Test2()
    {
        int[] reg = [10,0,0];
        var program = "5,0,5,1,5,4";

        var result = Run(program, reg);
        
        Assert.Equal("0,1,2", result);
    }
    [Fact]
    public void Test3()
    {
        int[] reg = [2024,0,0];
        var program = "0,1,5,4,3,0";

        var result = Run(program, reg);
        
        Assert.Equal("4,2,5,6,7,7,7,7,3,1,0", result);
        Assert.Equal(0, reg[0]);
    }
    [Fact]
    public void Test4()
    {
        int[] reg = [0,29,0];
        var program = "1,7";

        var result = Run(program, reg);
        
        Assert.Equal(26, reg[1]);
    }
    [Fact]
    public void Test5()
    {
        int[] reg = [0,2024,43690];
        var program = "4,0";

        var result = Run(program, reg);
        
        Assert.Equal(44354, reg[1]);
    }
    [Fact]
    public void Test6()
    {
        var testInput = File.ReadAllLines("C:/git/aoc/2024/day17/test");
        var p = Parse(testInput);
        var result = Run(p.program, p.reg);

        Assert.Equal("4,6,3,5,6,3,5,2,1,0", result);
    }
    [Fact]
    public void TestAdv()
    {
        int[] reg = [1337,0,0];
        var program = "0,0";

        var result = Run(program, reg);
        
        Assert.Equal(1337, reg[0]);
    }
    [Fact]
    public void TestBdv()
    {
        int[] reg = [1337,0,0];
        var program = "6,0";

        var result = Run(program, reg);
        
        Assert.Equal(1337, reg[1]);
    }
    [Fact]
    public void TestCdv()
    {
        int[] reg = [1337,0,0];
        var program = "7,0";

        var result = Run(program, reg);
        
        Assert.Equal(1337, reg[2]);
    }
    [Fact]
    public void TestAdv2()
    {
        int[] reg = [4,2,0];
        var program = "0,5";

        _ = Run(program, reg);
        
        Assert.Equal(1, reg[0]);
    }
    [Fact]
    public void TestAdv3()
    {
        int[] numerators = [0,0,1,1,2,4,2,4,2,3];
        int[] operands =   [0,1,0,1,0,0,1,2,3,1];
        int[] expected =   [0,0,1,0,2,4,1,1,0,1];
        for(int i = 0; i < expected.Length; i++)
        {
            Assert.True(expected[i] == Adv(numerators[i], operands[i]), $"i was {i}");
        }
    }
}