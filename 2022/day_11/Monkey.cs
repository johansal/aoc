public class Monkey {
    public Monkey()
    {
        Items = new();
        OperationValue = "";
        InspectionCount = 0;
    }
    public List<long> Items {get;set;}
    public string OperationValue {get;set;}
    public long TestValue {get;set;}
    public int TMonkey {get; set;}
    public int FMonkey {get; set;}
    public long InspectionCount {get; set;}
    public delegate long OperationCallback(long item, string x);
    public void Operation(OperationCallback operation, long N) {
        for(int i = 0; i < Items.Count; i++) {
            InspectionCount++;
            Items[i] = operation(Items[i], OperationValue) % N;
            // N is product of all monkey test values
            // this works because: N = a * b & x % a == y => (x % N) % a == y
        }
    }
    public int Test(int i) {
        return Items[i] % TestValue == 0 ? TMonkey : FMonkey;
    }
}


