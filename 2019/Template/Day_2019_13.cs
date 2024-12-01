using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Template
{
    public class Day_2019_13
    {
        public static string firstPuzzle(string input)
        {
            long[] intcode = input.Split(',').Select(long.Parse).ToArray();
            IntComputer ic = new IntComputer(intcode);

            int blocks = 0;

            ic.compute();
            Console.WriteLine(ic.isWaiting.ToString());
            for (int i = 2; i < ic.outputs.Count; i += 3)
            {
                if (ic.outputs[i] == 2)
                    blocks++;
            }

            return blocks.ToString();
        }

        public static string secondPuzzle(string input)
        {
            long[] intcode = input.Split(',').Select(long.Parse).ToArray();
            IntComputer ic = new IntComputer(intcode);

            long score = 0;
            long blocks = 363;
            Tuple<long, long> paddle = new Tuple<long, long>(0, 0);
            Tuple<long, long> oldbBall = new Tuple<long, long>(0, 0);
            Tuple<long, long> ball = new Tuple<long, long>(0, 0);

            ic.compute();
            while (ic.isWaiting)
            {
                blocks = 0;
                Console.WriteLine(ic.outputs.Count);
                while (ic.outputs.Count >= 3)
                {
                    if (ic.outputs[2] == 4)
                    {
                        oldbBall = ball;
                        ball = new Tuple<long, long>(ic.outputs[0], ic.outputs[1]);
                    }
                    else if (ic.outputs[2] == 3)
                    {
                        paddle = new Tuple<long, long>(ic.outputs[0], ic.outputs[1]);
                    }
                    else if (ic.outputs[2] == 2)
                    {
                        blocks++;
                    }
                    else if (ic.outputs[0] == -1 && ic.outputs[1] == 0)
                    {
                        score = ic.outputs[2];
                    }
                    ic.outputs.RemoveRange(0, 3);
                }
                Console.WriteLine(blocks + " blocks left!");
                //Move paddle according to ball movement
                if (oldbBall.Item1 < ball.Item1)
                {
                    //ball is moving right
                    if (paddle.Item1 > ball.Item1 + 1)
                    {
                        ic.inputs.Add(-1);
                    }
                    else if (paddle.Item1 < ball.Item1 + 1)
                    {
                        ic.inputs.Add(1);
                    }
                    else
                    {
                        ic.inputs.Add(0);
                    }
                }
                else if (oldbBall.Item1 > ball.Item1)
                {
                    //ball is moving right
                    if (paddle.Item1 > ball.Item1 - 1)
                    {
                        ic.inputs.Add(-1);
                    }
                    else if (paddle.Item1 < ball.Item1 - 1)
                    {
                        ic.inputs.Add(1);
                    }
                    else
                    {
                        ic.inputs.Add(0);
                    }
                }
                else
                {
                    //ball is moving straight up or down
                    if (paddle.Item1 > ball.Item1)
                    {
                        ic.inputs.Add(-1);
                    }
                    else if (paddle.Item1 < ball.Item1)
                    {
                        ic.inputs.Add(1);
                    }
                    else
                    {
                        ic.inputs.Add(0);
                    }
                }
            }

            return score.ToString();
        }
        public static int rotateCompass(int compass, int direction)
        {
            if (direction == 0)
                compass--;
            else
                compass++;
            if (compass < 1)
                compass = 4;
            else if (compass > 4)
                compass = 1;
            return compass;
        }
    }
}
