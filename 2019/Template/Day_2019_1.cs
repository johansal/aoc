using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Template
{
    public class Day_2019_1
    {
        public static string firstPuzzle(string location) {
            //Fuel counter-upper
            string[] fuels = File.ReadAllLines(@location, Encoding.UTF8);
            int output = 0;
            foreach(string fuelS in fuels) {
                int fuel = Convert.ToInt32(fuelS);
                decimal x = fuel / 3;
                fuel = (int)Math.Floor(x) - 2;
                output += fuel;
            }
            return output.ToString();
        }

        public static string secondPuzzle(string location) {
            //Fuel counter-upper
            string[] fuels = File.ReadAllLines(@location, Encoding.UTF8);
            int output = 0;
            foreach(string fuelS in fuels) {
                int fuel = Convert.ToInt32(fuelS);
                fuel = calculateFuel(fuel);
                output += fuel;
            }
            return output.ToString();
        }

        private static int calculateFuel(int f) {
            decimal x = f / 3;
            f = f / 3 - 2;
            if(f / 3 - 2 > 0) {
                f += calculateFuel(f);
            }
            return f;

        }
    }
}
