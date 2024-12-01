public class Cpu {
    public Crt Crt = new();
    public Register X = new();
    public int Cycle {get; set;}
    public int signalStrengthSum = 0;
    private int debugCycle = 20;

    public void addx(int x) {
        tick();
        tick();
        X.addx(x);
    }
    public void noop() {
        tick();
    }
    private void tick() {
        Crt.Draw(Cycle, X.Value);
        Cycle++;
        //Console.WriteLine("During cycle " + Cycle + " register is " + Register);
        //if(Cycle == debugCycle)
        //    Debug();
    }
    private void Debug() {
        var signalStr = debugCycle * X.Value;
        signalStrengthSum += signalStr;
        debugCycle += 40;
        //Console.WriteLine(Cycle + ": " + Register);
        //Console.WriteLine(signalStr);
    }
    
}