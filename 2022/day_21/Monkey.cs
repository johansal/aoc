namespace day_21 {
    public class Monkey {
        public string Name {get;set;}
        public string[] Operation {get;set;}

        public Monkey(string line) {
            var tmp = line.Split(": ");
            Name = tmp[0];
            Operation = tmp[1].Split(' ');
        }
    }
}