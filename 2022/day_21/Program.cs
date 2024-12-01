namespace day_21 {
    internal class Program
    {
        private static void Main()
        {
            var input = File.ReadAllLines("input");
            List<Monkey> monkeys = new();
            foreach (var line in input)
            {
                monkeys.Add(new(line));
            }
            Console.WriteLine("Part1: " + Shout("root", monkeys));
            Console.WriteLine("Part12 " + Solve("root", "humn", monkeys, null));
        }
        /*
        One of the monkeys is actually unknown and top operation (root) is equality check,
        find wihch side of equation the unknown monkey is, use shout to solve the side we know all monkeys
        and make recursive caal to solve the unknown side
        */
        public static long Solve(string root, string unknown, List<Monkey> monkeys, long? result) {
            /*
            if(root = unknown return result)
            else check if unknown is on 1st operands side
                if true solve 2nd operand with shout
                else solve 1st operands side with shout
                make käänteistemppu to result and other side and then solve not known side
            */
            if(root == unknown) {
                if (result == null)
                    throw new Exception("Result should not be null here!");
                return result ?? 0;
            }

            Monkey? r = monkeys.Find(x=>x.Name.Equals(root));
            if(r == null)
                throw new Exception("Did not find monkey " + root);
            if(r.Operation.Length == 1) {
                return long.Parse(r.Operation[0]);
            }
            else {
                if(FindMonkey(r.Operation[0],unknown,monkeys))
                {
                    //unknown is left side
                    var otherSide = Shout(r.Operation[2], monkeys);
                    //make käänteistemppu
                    long newResult;
                    if(r.Operation[1] == "+") {
                        newResult = result - otherSide ?? otherSide; //does this work?
                    }
                    else if(r.Operation[1] == "-") {
                        newResult = result + otherSide ?? otherSide; //does this work?
                    }
                    else if(r.Operation[1] == "*") {
                        newResult = result / otherSide ?? otherSide; //does this work?
                    }
                    else if(r.Operation[1] == "/") {
                        newResult = result * otherSide ?? otherSide; //does this work?
                    }
                    else {
                        var error =  r.Operation.Length > 1 ? r.Operation[1] : "";
                        throw new Exception("Unknown operation " + error);
                    }
                    return Solve(r.Operation[0], unknown, monkeys, newResult);
                }
                else {
                    //unknown is right side
                    var otherSide = Shout(r.Operation[0], monkeys);
                    //make käänteistemppu
                    long newResult;
                    if(r.Operation[1] == "+") {
                        newResult = result - otherSide ?? otherSide; //does this work?
                    }
                    else if(r.Operation[1] == "-") {
                        newResult = -1 * (result - otherSide) ?? otherSide; //does this work?
                    }
                    else if(r.Operation[1] == "*") {
                        newResult = result / otherSide ?? otherSide; //does this work?
                    }
                    else if(r.Operation[1] == "/") {
                        newResult = otherSide / result ?? otherSide; //does this work?
                    }
                    else {
                        var error =  r.Operation.Length > 1 ? r.Operation[1] : "";
                        throw new Exception("Unknown operation " + error);
                    }
                    return Solve(r.Operation[2], unknown, monkeys, newResult);
                }
            }

        }
        public static bool FindMonkey(string root, string name, List<Monkey> monkeys) {
            if(root != name) {
                Monkey? m = monkeys.Find(x=>x.Name.Equals(root));
                if(m.Operation.Length == 3) {
                    return FindMonkey(m.Operation[0], name, monkeys) || FindMonkey(m.Operation[2], name, monkeys);
                }
                else {
                    return false;
                }
            }
            else {return true;}
        }
        /*
        split line with ': '
        [0] is monkey name
        [1] is operation
        split operation with ' '
        (if operation length is 1 -> number
        else some math operation)

        make recurcive function to
        find monkey by name 
        check operation length, if 1 return it
        else find monkeys by by name and return the number according to operation
        */
        public static long Shout(string name, List<Monkey> monkeys) {
            Monkey? m = monkeys.Find(x=>x.Name.Equals(name));

            if(m == null)
                throw new Exception("Monkey not find!");
            if(m.Operation.Length == 1) {
                return long.Parse(m.Operation[0]);
            }
            else {
                var operand1 = Shout(m.Operation[0], monkeys);
                var operand2 = Shout(m.Operation[2], monkeys);
                if(m.Operation[1].Equals("+")) {
                    return operand1 + operand2;
                }
                else if(m.Operation[1].Equals("-")) {
                    return operand1 - operand2;
                }
                else if(m.Operation[1].Equals("*")) {
                    return operand1 * operand2;
                }
                else if(m.Operation[1].Equals("/")) {
                    return operand1 / operand2;
                }
                else {
                    var error =  m.Operation.Length > 1 ? m.Operation[1] : "";
                    throw new Exception("Unknown operation " + error);
                }
            }
        }
    }
}