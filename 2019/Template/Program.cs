using System;
using System.IO;
using System.Text;

namespace Template
{
    class Program
    {
        static void Main(string[] args)
        {
            string year;
            string day;
            string puzzle;
            string input;
            string inputLocation;
            string output = "";
            var watch = new System.Diagnostics.Stopwatch();
            string testInputLocation = "./inputs/test.txt";

            Console.WriteLine("Loading input...");

            //Allowed arguments: year(int) day(int) puzzle(1/2)

            if (args.Length < 2 || args.Length > 3 || !Int32.TryParse(args[0], out int r1) || !Int32.TryParse(args[1], out int r2))
            {
                year = DateTime.Now.Year.ToString();
                day = DateTime.Now.Day.ToString();
                if (args.Length == 1)
                    puzzle = args[0];
                else
                    puzzle = "1";
            }
            else
            {
                year = args[0];
                day = args[1];
                if (args.Length == 3 && (args[2].Equals("1") || args[2].Equals("2")))
                    puzzle = args[2];
                else
                    puzzle = "1";
            }
            inputLocation = GetInputLocation(year, day);
            input = GetInputString(year, day);
            //Console.WriteLine("debug :: " + input);

            Console.Write("Running puzzle " + puzzle + "...  ");

            watch.Start();

            if (year.Equals("2019") && day.Equals("1"))
            {
                if (puzzle.Equals("1"))
                    output = Day_2019_1.firstPuzzle(inputLocation);
                else if (puzzle.Equals("2"))
                    output = Day_2019_1.secondPuzzle(inputLocation);
            }
            else if (year.Equals("2019") && day.Equals("2"))
            {
                if (puzzle.Equals("1"))
                    output = Day_2019_2.firstPuzzle(input);
                else if (puzzle.Equals("2"))
                    output = Day_2019_2.secondPuzzle(input);
            }
            else if (year.Equals("2019") && day.Equals("3"))
            {
                if (puzzle.Equals("1"))
                    output = Day_2019_3.firstPuzzle(inputLocation);
                else if (puzzle.Equals("2"))
                    output = Day_2019_3.secondPuzzle(inputLocation);
            }
            else if (year.Equals("2019") && day.Equals("4"))
            {
                if (puzzle.Equals("1"))
                    output = Day_2019_4.firstPuzzle(input);
                else if (puzzle.Equals("2"))
                    output = Day_2019_4.secondPuzzle(input);
            }
            else if (year.Equals("2019") && day.Equals("5"))
            {
                if (puzzle.Equals("1"))
                    output = Day_2019_5.firstPuzzle(input);
                else if (puzzle.Equals("2"))
                    output = Day_2019_5.secondPuzzle(input);
            }
            else if (year.Equals("2019") && day.Equals("6"))
            {
                if (puzzle.Equals("1"))
                    output = Day_2019_6.firstPuzzle(inputLocation);
                else if (puzzle.Equals("2"))
                    output = Day_2019_6.secondPuzzle(inputLocation);
            }
            else if (year.Equals("2019") && day.Equals("7"))
            {
                if (puzzle.Equals("1"))
                    output = Day_2019_7.firstPuzzle(input);
                else if (puzzle.Equals("2"))
                    output = Day_2019_7.secondPuzzle(input);
            }
            else if (year.Equals("2019") && day.Equals("8"))
            {
                if (puzzle.Equals("1"))
                    output = Day_2019_8.firstPuzzle(input);
                else if (puzzle.Equals("2"))
                    output = Day_2019_8.secondPuzzle(input);
            }
            else if (year.Equals("2019") && day.Equals("9"))
            {
                if (puzzle.Equals("1"))
                    output = Day_2019_9.firstPuzzle(input);
                else if (puzzle.Equals("2"))
                    output = Day_2019_9.secondPuzzle(input);
            }
            else if (year.Equals("2019") && day.Equals("10"))
            {
                if (puzzle.Equals("1"))
                    output = Day_2019_10.firstPuzzle(inputLocation);
                else if (puzzle.Equals("2"))
                    output = Day_2019_10.secondPuzzle(inputLocation);
            }
            else if (year.Equals("2019") && day.Equals("11"))
            {
                if (puzzle.Equals("1"))
                    output = Day_2019_11.firstPuzzle(input);
                else if (puzzle.Equals("2"))
                    output = Day_2019_11.secondPuzzle(input);
            }
            else if (year.Equals("2019") && day.Equals("12"))
            {
                if (puzzle.Equals("1"))
                    output = Day_2019_12.firstPuzzle(inputLocation);
                else if (puzzle.Equals("2"))
                    output = Day_2019_12.secondPuzzle(inputLocation);
            }
            else if (year.Equals("2019") && day.Equals("13"))
            {
                if (puzzle.Equals("1"))
                    output = Day_2019_13.firstPuzzle(input);
                else if (puzzle.Equals("2"))
                    output = Day_2019_13.secondPuzzle(input);
            }
            else if (year.Equals("2019") && day.Equals("14"))
            {
                if (puzzle.Equals("1"))
                    output = Day_2019_14.firstPuzzle(inputLocation);
                else if (puzzle.Equals("2"))
                    output = Day_2019_14.secondPuzzle(inputLocation);
            }
            else if (year.Equals("2019") && day.Equals("15"))
            {
                if (puzzle.Equals("1"))
                    output = new Day_2019_15().firstPuzzle(input);
                else if (puzzle.Equals("2"))
                    output = new Day_2019_15().secondPuzzle(input);
            }
            else if (year.Equals("2019") && day.Equals("16"))
            {
                if (puzzle.Equals("1"))
                    output = Day_2019_16.firstPuzzle(input);
                else if (puzzle.Equals("2"))
                    output = Day_2019_16.secondPuzzle(input);
            }
            else if (year.Equals("2019") && day.Equals("17"))
            {
                if (puzzle.Equals("1"))
                    output = Day_2019_17.firstPuzzle(input);
                else if (puzzle.Equals("2"))
                    output = Day_2019_17.secondPuzzle(input);
            }
            else
            {
                output = year + "/" + day + " not implemented, try " + testInputLocation;
            }

            watch.Stop();

            Console.WriteLine(watch.ElapsedMilliseconds + "ms");
            Console.WriteLine("Output:");
            Console.WriteLine(output);
        }

        #region Helper methods
        private static string GetInputString(string year, string day)
        {
            string s;
            string location = "./inputs/" + year + "_" + day + ".txt";
            try
            {
                s = File.ReadAllText(@location, Encoding.UTF8);
                Console.WriteLine("Got input for " + year + "/" + day + "!");
            }
            catch (FileNotFoundException)
            {
                s = null;
                Console.WriteLine("Missing input for " + year + "/" + day + "!");
            }
            return s;
        }
        private static string GetTestInputString()
        {
            string s;
            string location = "./inputs/test.txt";
            try
            {
                s = File.ReadAllText(@location, Encoding.UTF8);
                Console.WriteLine("Got test input!");
            }
            catch (FileNotFoundException)
            {
                s = null;
                Console.WriteLine("Missing test input!");
            }
            return s;
        }
        private static string GetInputLocation(string year, string day)
        {
            string location = "./inputs/" + year + "_" + day + ".txt";

            if (File.Exists(@location))
            {
                Console.WriteLine("Got location for " + year + "/" + day + "!");
                return location;
            }
            else
            {
                Console.WriteLine("Missing input for " + year + "/" + day + "!");
                return null;
            }
        }
        #endregion
    }
}
