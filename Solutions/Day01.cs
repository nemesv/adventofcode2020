using System;
using System.Linq;

namespace Solutions
{
    public class Day01 : Day
    {
        public Day01(string input) : base(input)
        {
        }

        public override string Part1()
        {
            var numbers = input.Split(Environment.NewLine).Select(int.Parse).ToList();
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[i] + numbers[j] == 2020)
                        return (numbers[i] * numbers[j]).ToString();
                }
            }

            return "";
        }

        public override string Part2()
        {
            var numbers = input.Split(Environment.NewLine).Select(int.Parse).ToList();
            for (int i = 0; i < numbers.Count - 2; i++)
            {
                for (int j = i + 1; j < numbers.Count - 1; j++)
                {
                    if (numbers[i] + numbers[j] > 2020)
                        continue;

                    for (int k = j + 1; k < numbers.Count; k++)
                    {
                        if (numbers[i] + numbers[j] + numbers[k] == 2020)
                            return (numbers[i] * numbers[j] * numbers[k]).ToString();
                    }
                }
            }

            return "";
        }
    }
}
