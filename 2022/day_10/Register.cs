public class Register
{
    public Register() => this.Value = 1;
    public Register(int x) => this.Value = x;
    public int Value {get; set;}
    public void addx(int V) => Value += V;
}