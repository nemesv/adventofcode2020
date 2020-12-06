using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public class Day06 : Day
    {
        public Day06(string input) : base(input)
        {
        }

        public override string Part1()
        {
            var groups = new List<string>();
            var current = "";
            foreach (var answer in input.Split(Environment.NewLine))
            {
                if (string.IsNullOrEmpty(answer))
                {
                    groups.Add(current);
                    current = "";
                    continue;
                }
                current += answer;
            }
            groups.Add(current);
            var counts = groups.Select(g => g.Distinct().Count());
            return counts.Sum().ToString();
        }

        public override string Part2()
        {
            var groups = new List<List<string>>();
            var current = new List<string>();
            foreach (var answer in input.Split(Environment.NewLine))
            {
                if (string.IsNullOrEmpty(answer))
                {
                    groups.Add(current);
                    current = new List<string>();
                    continue;
                }
                current.Add(answer);
            }
            groups.Add(current);
            var counts = groups
                .Select(g =>
                    string.Join("", g)
                        .GroupBy(x => x)
                        .Count(a => a.Count() == g.Count));
            return counts.Sum().ToString();
        }
    }
}
