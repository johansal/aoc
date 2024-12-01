using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace day_4
{
    class Program
    {
        static void Main(string[] args)
        {
            List<BingoBoard> boards = new();
            List<(BingoBoard,string)> winners = new();
            var input = File.ReadAllLines("input.txt");
            var bingoNumbers = input[0].Split(',');
            string newBoard = "";
            for (int i = 2; i < input.Length; i++)
            {
                var row = Regex.Replace(input[i], @"\s+", " ");
                row = Regex.Replace(row, @"^\s+","");
                //Console.WriteLine(row);
                if (!string.IsNullOrEmpty(input[i]))
                {
                    if (newBoard == "") {
                        newBoard = row;
                    }
                    else
                    {
                        newBoard += " " + row;
                    }
                }
                else
                {
                    boards.Add(new BingoBoard
                    {
                        Board = newBoard.Split(" ")
                    });
                    newBoard = "";
                }
            }
            boards.Add(new BingoBoard
            {
                Board = newBoard.Split(" ")
            });

            for (int i = 0; i < bingoNumbers.Length; i++) {
                //bool bingo = false;

                //Console.WriteLine("number of boards: " + boards.Count);
                List<BingoBoard> bingos = new();
                foreach(var board in boards) {
                    board.MarkNewNumber(bingoNumbers[i]);
                    //Console.WriteLine("Added number:" + bingoNumbers[i]);
                    //board.Print();
                    if(board.IsBingo()) {
                        //bingo = true;
                        //board.Print();
                        winners.Add(new (new BingoBoard {Board = board.Board}, bingoNumbers[i]));  
                        bingos.Add(board);
                        //Console.WriteLine("Bingo: " + board.SumOfUnmarked() + " " + int.Parse(bingoNumbers[i]));
                        //break;
                    }
                }
                foreach(var board in bingos) {
                    boards.Remove(board);
                }
            }
            Console.WriteLine("Last Bingo: " + winners.Last().Item1.SumOfUnmarked() * int.Parse(winners.Last().Item2));
        }
    }
    public class BingoBoard
    {
        public string[] Board { get; set; }
        public void Print() {
            for(int i = 0; i < 5; i++) {
                for (int j = 0; j < 5; j++) {
                    Console.Write(this.Board[(i*5)+j] + " ");
                }
                Console.WriteLine();
            }
        }
        public void MarkNewNumber(string number)
        {
            for (int i = 0; i < this.Board.Length; i++)
            {
                if (this.Board[i].Equals(number))
                {
                    this.Board[i] = "x";
                }
            }
        }
        public bool IsBingo()
        {
            //vertical lines
            for (int i = 0; i < 5; i++)
            {
                bool bingo = true;
                for (int j = 0; j < 5; j++)
                {
                    if (!this.Board[(i * 5) + j].Equals("x"))
                    {
                        bingo = false;
                        break;
                    }
                }
                if (bingo)
                    return true;
            }
            //columns
            for (int i = 0; i < 5; i++)
            {
                bool bingo = true;
                for (int j = 0; j < 5; j++)
                {
                    if (!this.Board[i + (j * 5)].Equals("x"))
                    {
                        bingo = false;
                        break;
                    }
                }
                if (bingo)
                    return true;
            }
            return false;
        }

        public int SumOfUnmarked() {
            int sum = 0;
            for(int i = 0; i < Board.Length; i++) {
                if(!Board[i].Equals("x")) {
                    sum += int.Parse(Board[i]);
                }
            }
            return sum;
        }

    }
}
