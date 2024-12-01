using System;
using System.Linq;

namespace Template
{
    public class Day_2019_4
    {
        public static string firstPuzzle(string input)
        {
            //Calculate possible passwords
            int lowerBound = Convert.ToInt32(input.Split('-')[0]);
            int hiBound = Convert.ToInt32(input.Split('-')[1]);
            int output = 0;
            for (int i = lowerBound + 1; i < hiBound; i++)
            {
                if (meetsCriteria(i))
                    output++;
            }

            return output.ToString();
        }

        public static string secondPuzzle(string input)
        {
            //Calculate possible passwords
            int lowerBound = Convert.ToInt32(input.Split('-')[0]);
            int hiBound = Convert.ToInt32(input.Split('-')[1]);
            int output = 0;
            for (int i = lowerBound + 1; i < hiBound; i++)
            {
                var digits = i.ToString().Select(t => int.Parse(t.ToString())).ToArray();
                if (strictDoubleCriteria(digits) && increasingCriteria(digits))
                    output++;
            }

            return output.ToString();
        }

        private static bool meetsCriteria(int a)
        {
            var digits = a.ToString().Select(t => int.Parse(t.ToString())).ToArray();
            if (doubleCriteria(digits) && increasingCriteria(digits))
                return true;
            return false;
        }
        private static bool doubleCriteria(int[] digits)
        {
            for (int i = 0; i < digits.Length - 1; i++)
            {
                if (digits[i] == digits[i + 1])
                    return true;
            }
            return false;
        }
        private static bool strictDoubleCriteria(int[] digits)
        {
            for (int i = 0; i < digits.Length - 1; i++)
            {
                if (digits[i] == digits[i + 1]) {
                    //make sure they are not part of larger group
                    if (i - 1 < 0 || digits[i-1] != digits[i])
                    {
                        if (i + 2 > digits.Length - 1 || digits[i+2] != digits[i]) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private static bool increasingCriteria(int[] digits)
        {
            for (int i = 0; i < digits.Length - 1; i++)
            {
                if (digits[i] > digits[i + 1])
                    return false;
            }
            return true;
        }
    }
}
