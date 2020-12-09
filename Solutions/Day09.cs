using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public class Day09 : Day
    {
        public Day09(string input) : base(input)
        {
        }

        public override string Part1()
        {
            return Part1(25);
        }

        public string Part1(int preamble)
        {
            var numbers = input.Split(Environment.NewLine).Select(long.Parse).ToArray();
            var i = 0;
            foreach (var n in numbers.Skip(preamble))
            {
                if (!CheckSum(numbers.Skip(i).Take(preamble).ToArray(), n))
                    return n.ToString();
                i++;
            }

            return "";
        }

        private bool CheckSum(long[] numbers, long n)
        {
            var current = numbers;
            for (int j = 0; j < current.Length; j++)
            {
                for (int k = j; k < current.Length; k++)
                {
                    if (current[j] + current[k] == n)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override string Part2()
        {
            return Part2(144381670);
        }

        public string Part2(int target)
        {
            var numbers = input.Split(Environment.NewLine).Select(long.Parse).ToArray();
            
            var current = new List<long>() { numbers[0], numbers[1]};
            var currentIndex = 2;
            while (true)
            {
                var currentSum = current.Sum();
                if (current.Count > 1 && currentSum == target)
                    break;
                if (currentSum < target)
                {
                    current.Add(numbers[currentIndex]);
                    currentIndex++;
                }
                else
                {
                    current.RemoveAt(0);
                }
            }

            return (current.Max() + current.Min()).ToString();
        }
    }
}
