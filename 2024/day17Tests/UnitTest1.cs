using static day17.Program;

namespace day17Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        long[] reg = [0,0,9];
        var program = "2,6";
        _ = Run(program, reg);

        Assert.Equal(1, reg[1]);
    }
    [Fact]
    public void Test2()
    {
        long[] reg = [10,0,0];
        var program = "5,0,5,1,5,4";

        var result = Run(program, reg);
        
        Assert.Equal("0,1,2", result);
    }
    [Fact]
    public void Test3()
    {
        long[] reg = [2024,0,0];
        var program = "0,1,5,4,3,0";

        var result = Run(program, reg);
        
        Assert.Equal("4,2,5,6,7,7,7,7,3,1,0", result);
        Assert.Equal(0, reg[0]);
    }
    [Fact]
    public void Test4()
    {
        long[] reg = [0,29,0];
        var program = "1,7";

        _ = Run(program, reg);
        
        Assert.Equal(26, reg[1]);
    }
    [Fact]
    public void Test5()
    {
        long[] reg = [0,2024,43690];
        var program = "4,0";

        _ = Run(program, reg);
        
        Assert.Equal(44354, reg[1]);
    }
    [Fact]
    public void Test6()
    {
        string[] testInput = ["Register A: 729","Register B: 0","Register C: 0",string.Empty,"Program: 0,1,5,4,3,0"];
        var (reg, program) = Parse(testInput);
        var result = Run(program, reg);

        Assert.Equal("4,6,3,5,6,3,5,2,1,0", result);
    }
    [Fact]
    public void TestAdv()
    {
        long[] reg = [1337,0,0];
        var program = "0,0";

        _ = Run(program, reg);
        
        Assert.Equal(1337, reg[0]);
    }
    [Fact]
    public void TestBdv()
    {
        long[] reg = [1337,0,0];
        var program = "6,0";

        _ = Run(program, reg);
        
        Assert.Equal(1337, reg[1]);
    }
    [Fact]
    public void TestCdv()
    {
        long[] reg = [1337,0,0];
        var program = "7,0";

        _ = Run(program, reg);
        
        Assert.Equal(1337, reg[2]);
    }
    [Fact]
    public void TestAdv2()
    {
        long[] reg = [4,2,0];
        var program = "0,5";

        _ = Run(program, reg);
        
        Assert.Equal(1, reg[0]);
    }
}