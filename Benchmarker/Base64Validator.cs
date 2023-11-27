using System.Buffers;

namespace Benchmarker;

public class Base64Validator {
    const string validCharsStr = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+/=";

    private readonly char[] validChars = [
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
        '0','1','2','3','4','5','6','7','8','9','+','/','='
        ];

    //SearchValues.Create is the magic here, you want to call it just once!
    static readonly SearchValues<char> base64SearchVals = SearchValues.Create(validCharsStr);

    public bool Validate_Naive(string value) {
        foreach(char c in value) {
            if (validChars.Contains(c) == false)
                return false;
        }
        return true;
    }

    public bool Validate_Span(string value)
    {
        return value.AsSpan().IndexOfAnyExcept(validChars) == -1;
    }

    public static bool Validate(string value)
    {
        return value.AsSpan().IndexOfAnyExcept(base64SearchVals) == -1;
    }
}