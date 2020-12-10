using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public class Day10 : Day
    {
        public Day10(string input) : base(input)
        {
        }

        public override string Part1()
        {
            var adapters = input.Split(Environment.NewLine).Select(int.Parse)
                .OrderBy(a => a).ToList();
            adapters.Insert(0, 0);
            adapters.Add(adapters.Max() + 3);
            var ones = 0;
            var threes = 0;
            var twos = 0;
            for (int i = 0; i < adapters.Count - 1; i++)
            {
                var difference = adapters[i + 1] - adapters[i];
                if (difference == 1)
                    ones++;
                if (difference == 3)
                    threes++;
                if (difference == 2)
                    twos++;
            }
            return (ones * threes).ToString();
        }

        public override string Part2()
        {
            var adapters = input.Split(Environment.NewLine).Select(int.Parse)
                .OrderBy(a => a).ToList();
            adapters.Insert(0, 0);
            adapters.Add(adapters.Max() + 3);
            var oneLengths = new List<int>();
            var c = 0;
            for (int i = 0; i < adapters.Count - 1; i++)
            {
                var difference = adapters[i + 1] - adapters[i];
                if (difference == 1)
                {
                    c++;
                }
                else
                {
                    oneLengths.Add(c);
                    c = 0;
                }
            }

            var result = 1L;
            foreach (var oneLength in oneLengths)
            {
                switch (oneLength)
                {
                    case 2:
                        result *= 2;
                        break;
                    case 3:
                        result *= 4;
                        break;
                    case 4:
                        result *= 7;
                        break;
                }
            }

            return result.ToString();
        }
    }
}
