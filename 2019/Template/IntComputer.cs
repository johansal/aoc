using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Template
{
    public class IntComputer
    {
        const long ADD = 1;
        const long MULTIPLY = 2;
        const long INPUT = 3;
        const long OUTPUT = 4;
        const long JUMP_IF_T = 5;
        const long JUMP_IF_F = 6;
        const long LESS_THAN = 7;
        const long EQUALS = 8;
        const long RELATIVA_BASE_OFFSET = 9;
        const long STOP = 99;
        long position { get; set; }
        long relativeBase { get; set; }
        long[] memory { get; set; }

        public bool isHalted { get; set; }
        public bool isWaiting { get; set; }
        public List<long> inputs { get; set; }
        public List<long> outputs { get; set; }

        public IntComputer(long[] m)
        {
            position = 0;
            relativeBase = 0;
            memory = m;
            isHalted = false;
            isWaiting = false;
            inputs = new List<long>();
            outputs = new List<long>();
        }
        public bool compute()
        {
            while (position < memory.Length)
            {

                //Parse int code and parameter mode (0 position, 1 immediate, 2 relative)
                long[] digits = memory[position].ToString().Select(t => long.Parse(t.ToString())).ToArray();
                long intCode = digits.Length >= 2 ? digits[digits.Length - 2] * 10 + digits[digits.Length - 1] : digits[0];
                try
                {
                    if (intCode == ADD)
                    {
                        long param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? memory[position + 1] :
                            digits.Length > 2 && digits[digits.Length - 3] == 2 ? memory[memory[position + 1] + relativeBase] : memory[memory[position + 1]];
                        long param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? memory[position + 2] :
                            digits.Length > 3 && digits[digits.Length - 4] == 2 ? memory[memory[position + 2] + relativeBase] : memory[memory[position + 2]];
                        if (digits.Length > 4 && digits[digits.Length - 5] == 2)
                            memory[memory[position + 3] + relativeBase] = param1 + param2;
                        else
                            memory[memory[position + 3]] = param1 + param2;
                        position += 4;
                    }
                    else if (intCode == MULTIPLY)
                    {
                        long param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? memory[position + 1] :
                            digits.Length > 2 && digits[digits.Length - 3] == 2 ? memory[memory[position + 1] + relativeBase] : memory[memory[position + 1]];
                        long param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? memory[position + 2] :
                            digits.Length > 3 && digits[digits.Length - 4] == 2 ? memory[memory[position + 2] + relativeBase] : memory[memory[position + 2]];
                        if (digits.Length > 4 && digits[digits.Length - 5] == 2)
                            memory[memory[position + 3] + relativeBase] = param1 * param2;
                        else
                            memory[memory[position + 3]] = param1 * param2;
                        position += 4;
                    }
                    else if (intCode == INPUT)
                    {
                        if (inputs.Count > 0)
                        {
                            if (isWaiting)
                                isWaiting = false;
                            if (digits.Length > 2 && digits[digits.Length - 3] == 2)
                                memory[memory[position + 1] + relativeBase] = inputs.First();
                            else
                                memory[memory[position + 1]] = inputs.First();
                            inputs.RemoveAt(0);
                            position += 2;
                        }
                        else
                        {
                            isWaiting = true;
                            return true;
                        }
                    }
                    else if (intCode == OUTPUT)
                    {
                        if (digits.Length > 2 && digits[digits.Length - 3] == 1)
                            outputs.Add(memory[position + 1]);
                        else if (digits.Length > 2 && digits[digits.Length - 3] == 2)
                            outputs.Add(memory[memory[position + 1] + relativeBase]);
                        else
                            outputs.Add(memory[memory[position + 1]]);
                        position += 2;
                    }
                    else if (intCode == JUMP_IF_T)
                    {
                        long param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? memory[position + 1] :
                            digits.Length > 2 && digits[digits.Length - 3] == 2 ? memory[memory[position + 1] + relativeBase] : memory[memory[position + 1]];
                        long param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? memory[position + 2] :
                            digits.Length > 3 && digits[digits.Length - 4] == 2 ? memory[memory[position + 2] + relativeBase] : memory[memory[position + 2]];
                        if (param1 != 0)
                            position = param2;
                        else
                            position += 3;
                    }
                    else if (intCode == JUMP_IF_F)
                    {
                        long param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? memory[position + 1] :
                            digits.Length > 2 && digits[digits.Length - 3] == 2 ? memory[memory[position + 1] + relativeBase] : memory[memory[position + 1]];
                        long param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? memory[position + 2] :
                            digits.Length > 3 && digits[digits.Length - 4] == 2 ? memory[memory[position + 2] + relativeBase] : memory[memory[position + 2]];
                        if (param1 == 0)
                            position = param2;
                        else
                            position += 3;
                    }
                    else if (intCode == LESS_THAN)
                    {
                        long param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? memory[position + 1] :
                            digits.Length > 2 && digits[digits.Length - 3] == 2 ? memory[memory[position + 1] + relativeBase] : memory[memory[position + 1]];
                        long param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? memory[position + 2] :
                            digits.Length > 3 && digits[digits.Length - 4] == 2 ? memory[memory[position + 2] + relativeBase] : memory[memory[position + 2]];
                        if (digits.Length > 4 && digits[digits.Length - 5] == 2)
                            memory[memory[position + 3] + relativeBase] = param1 < param2 ? 1 : 0;
                        else
                            memory[memory[position + 3]] = param1 < param2 ? 1 : 0;
                        position += 4;
                    }
                    else if (intCode == EQUALS)
                    {
                        long param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? memory[position + 1] :
                            digits.Length > 2 && digits[digits.Length - 3] == 2 ? memory[memory[position + 1] + relativeBase] : memory[memory[position + 1]];
                        long param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? memory[position + 2] :
                            digits.Length > 3 && digits[digits.Length - 4] == 2 ? memory[memory[position + 2] + relativeBase] : memory[memory[position + 2]];
                        if (digits.Length > 4 && digits[digits.Length - 5] == 2)
                            memory[memory[position + 3] + relativeBase] = param1 == param2 ? 1 : 0;
                        else
                            memory[memory[position + 3]] = param1 == param2 ? 1 : 0;
                        position += 4;
                    }
                    else if (intCode == RELATIVA_BASE_OFFSET)
                    {
                        long param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? memory[position + 1] :
                            digits.Length > 2 && digits[digits.Length - 3] == 2 ? memory[memory[position + 1] + relativeBase] : memory[memory[position + 1]];
                        relativeBase += param1;
                        position += 2;
                    }
                    else if (intCode == STOP)
                    {
                        isHalted = true;
                        return true;
                    }
                    else
                    {
                        throw new Exception("Unsupported opcode " + memory[position].ToString() + " at " + position);
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    Console.WriteLine("[" + string.Join(' ', digits) + "] Index out of bounds at " + position + ", double memory!");
                    if (position == 477)
                        throw new Exception("STOP");
                    long[] additionalMemory = new long[memory.Length * 2];
                    memory.CopyTo(additionalMemory, 0);
                    for (long i = memory.Length; i < additionalMemory.Length; i++) { additionalMemory[i] = 0; }
                    memory = additionalMemory;
                }
            }
            Console.WriteLine("We shouldn't be here!");
            return false;
        }
    }
}