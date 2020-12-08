using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public class Day08 : Day
    {
        public Day08(string input) : base(input)
        {
        }

        public override string Part1()
        {
            var accumulator = 0;
            var instructions = input
                .Split(Environment.NewLine)
                .Select(i =>
                {
                    var parts = i.Split(" ");
                    return (Op: parts[0], Arg: int.Parse(parts[1]));
                }).ToList();
            var currentIndex = 0;
            var visited = new HashSet<int>();

            while (!visited.Contains(currentIndex))
            {
                var currentOp = instructions[currentIndex];
                visited.Add(currentIndex);
                switch (currentOp.Op)
                {
                    case "nop":
                        currentIndex += 1;
                        break;
                    case "acc":
                        accumulator += currentOp.Arg;
                        currentIndex += 1;
                        break;
                    case "jmp":
                        currentIndex += currentOp.Arg;
                        break;
                }
            }
            return accumulator.ToString();
        }

        public override string Part2()
        {
            var instructions = input
                .Split(Environment.NewLine)
                .Select(i =>
                {
                    var parts = i.Split(" ");
                    return (Op: parts[0], Arg: int.Parse(parts[1]));
                }).ToList();

            var changeAt = new HashSet<int>();

            while (true)
            {
                var accumulator = 0;
                var currentIndex = 0;
                var visited = new HashSet<int>();
                var changed = false;
                while (!visited.Contains(currentIndex))
                {
                    var currentOp = instructions[currentIndex];

                    if (!changed && !changeAt.Contains(currentIndex))
                    {
                        if (currentOp.Op == "nop")
                        {
                            currentOp = ("jmp", currentOp.Arg);
                            changeAt.Add(currentIndex);
                            changed = true;
                        }
                        else if (currentOp.Op == "jmp")
                        {
                            currentOp = ("nop", currentOp.Arg);
                            changeAt.Add(currentIndex);
                            changed = true;
                        }
                    }

                    visited.Add(currentIndex);
                    switch (currentOp.Op)
                    {
                        case "nop":
                            currentIndex += 1;
                            break;
                        case "acc":
                            accumulator += currentOp.Arg;
                            currentIndex += 1;
                            break;
                        case "jmp":
                            currentIndex += currentOp.Arg;
                            break;
                    }

                    if (currentIndex >= instructions.Count)
                        return accumulator.ToString();
                }
            }

        }
    }
}
