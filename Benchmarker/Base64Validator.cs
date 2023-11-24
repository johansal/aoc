namespace Benchmarker;

public class Base64Validator {
    private readonly char[] validChars = ['a','b','c','d','1','2','3','4'];
    public bool Validate_Naive(string value) {
        foreach(char c in value) {
            if (validChars.Contains(c) == false)
                return false;
        }
        return true;
    }
}