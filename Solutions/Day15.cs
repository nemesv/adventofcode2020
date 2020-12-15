using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public class Day15 : Day
    {
        public Day15(string input) : base(input)
        {
        }

        public override string Part1()
        {
            var numbers = input.Split(",").Select(int.Parse).ToList();
            while (numbers.Count < 2020)
            {
                var last = numbers.Last();
                var lastTime = numbers.LastIndexOf(last, numbers.Count - 2);
                if (lastTime == -1)
                {
                    numbers.Add(0);
                }
                else
                {
                    numbers.Add(numbers.Count - 1 - lastTime);
                }
            }

            return numbers[2019].ToString();
        }

        public override string Part2()
        {
            var numbers = input.Split(",").Select(int.Parse).ToList();
            var lastIndex = numbers.SkipLast(1).Select((i,ind) => new {i,ind})
                .ToDictionary(i => i.i, i => i.ind + 1);
            var turn = numbers.Count + 1;
            var last = numbers.Last();
            while (turn <= 30000000)
            {
                if (!lastIndex.ContainsKey(last))
                {
                    lastIndex[last] = turn - 1;
                    last = 0;
                }
                else
                {
                    var prevIndex = lastIndex[last];
                    lastIndex[last] = turn - 1;
                    last = turn - prevIndex - 1;
                }
                turn++;
            }

            return last.ToString();
        }
    }
}
