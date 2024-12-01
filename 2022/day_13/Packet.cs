public class Packet : IComparable<Packet> {
    public string Value {get;set;}
    public Packet(string s) {
        Value = s;
    }
    private static int CompareLists(string left, string right) {
        //Parse next value
        //Console.WriteLine($"Compare {left} - {right}");
        var lValue = ParseNextValue(left);
        var rValue = ParseNextValue(right);
        //Console.WriteLine($"Parsed value {lValue} - {rValue}");
        //if only left missing
        //packet ok
        if(string.IsNullOrEmpty(lValue) && !string.IsNullOrEmpty(rValue)) {
            return -1;
        }
        //if only right missing
        //packet not ok
        else if(!string.IsNullOrEmpty(lValue) && string.IsNullOrEmpty(rValue)) {
            return 1;
        }
        //if both are empty -> move to next value
        else if(string.IsNullOrEmpty(lValue) && string.IsNullOrEmpty(rValue)) {
            return 0;
        }
        int compareResult;
        //if both are integers -> compare values
        if(int.TryParse(lValue, out int l) && int.TryParse(rValue, out int r)) {
            compareResult = l - r;
        }
        //if both are lists -> compare sub lists
        else if(lValue[0] == '[' && rValue[0] == '[') {
            compareResult = CompareLists(lValue, rValue);
        }
        //if only one is list, create new list for integer value and compare sub lists
        else {
            if(lValue[0] != '[') {
                compareResult = CompareLists($"[{lValue}]", rValue);
            }
            else if (rValue[0] != '[') {
                compareResult = CompareLists(lValue, $"[{rValue}]");
            }
            else {
                throw new NotImplementedException();
            }
        }

        //comparison result:
        //if same remove this value from lists and compare sub newlist
        //if left < right -> packet is ok
        //else packet not ok
        if(compareResult != 0) {
            return compareResult;
        }
        else {
            left = RemoveValue(left);
            right = RemoveValue(right);
            return CompareLists(left, right);
        }
    }

    private static string RemoveValue(string packet)
    {
        var value = ParseNextValue(packet);
        //Console.WriteLine($"removing value {value} from packet {packet}...");
        packet = packet[(1 + value.Length)..];
        if(packet[0] == ',')
            packet = packet[1..];
        //Console.WriteLine("[" + packet);
        return "[" + packet;
    }

    private static string ParseNextValue(string packet) {
        if(string.IsNullOrEmpty(packet))
            return packet;

        int depth = 0;
        int i = 0;
        while(i < packet.Length && packet[i] != ',') {
            if(packet[i] == '[') {
                depth++;
            }
            else if (packet[i] == ']') {
                depth--;
            }
            i++;
        }
        if(i == packet.Length) {
            return packet[1..^1];
        }
        else if(depth == 1) {
            return packet[1..i];
        }
        else {
            while(i < packet.Length && depth > 1) {
                if(packet[i] == '[') {
                    depth++;
                }
                else if (packet[i] == ']') {
                    depth--;
                }
                i++;
            }
            return packet[1..i];
        }
    }

    public int CompareTo(Packet? obj)
    {
        if(obj == null)
            return 1;
        return CompareLists(this.Value, obj.Value);
    }
}