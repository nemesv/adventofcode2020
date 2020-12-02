using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Solutions
{
    public class Day02 : Day
    {
        public Day02(string input) : base(input)
        {
        }

        public override string Part1()
        {
            var lines = input.Split(Environment.NewLine);
            var result = 0;
            foreach (var line in lines)
            {
                var parts = Regex.Match(line, @"(\d+)-(\d+) (\w): (.*)");
                var min = int.Parse(parts.Groups[1].Value);
                var max = int.Parse(parts.Groups[2].Value);
                var letter = parts.Groups[3].Value;
                var password = parts.Groups[4].Value;

                var letterCount = password.Count(l => l.ToString() == letter);

                if (min <= letterCount && letterCount <= max)
                    result++;
            }

            return result.ToString();
        }

        public override string Part2()
        {
            var lines = input.Split(Environment.NewLine);
            var result = 0;
            foreach (var line in lines)
            {
                var parts = Regex.Match(line, @"(\d+)-(\d+) (\w): (.*)");
                var min = int.Parse(parts.Groups[1].Value);
                var max = int.Parse(parts.Groups[2].Value);
                var letter = parts.Groups[3].Value;
                var password = parts.Groups[4].Value;

                var matchCount = 0;
                if (password[min - 1].ToString() == letter)
                    matchCount++;
                if (password[max - 1].ToString() == letter)
                    matchCount++;

                if (matchCount == 1)
                    result++;
            }

            return result.ToString();
        }
    }
}
