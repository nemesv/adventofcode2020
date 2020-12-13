using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public class Day13 : Day
    {
        public Day13(string input) : base(input)
        {
        }

        public override string Part1()
        {
            var lines = input.Split(Environment.NewLine);
            var departTime = int.Parse(lines[0]);
            var buses = lines[1].Split(",").Where(b => b != "x").Select(int.Parse).ToArray();

            var waitTimes = new List<(int Bus, int Wait)>();

            foreach (var bus in buses)
            {
                var d = departTime / bus;
                waitTimes.Add((bus, (d + 1) * bus - departTime));
            }

            var result = waitTimes.OrderBy(w => w.Wait).First();
            return (result.Wait * result.Bus).ToString();
        }

        public override string Part2()
        {
            var lines = input.Split(Environment.NewLine);
            var departTime = int.Parse(lines[0]);
            var buses = lines[1].Split(",")
                .Select((b,i) =>
                {
                    if (b == "x")
                        return 0;

                    return int.Parse(b);
                })
                .ToArray();


            return ChineseRemainderTheorem
                .Solve(
                    buses.Where(b => b !=0).ToArray(),
                    buses.Where(b => b != 0)
                        .Select(b => Array.IndexOf(buses, b) - b).ToArray()
                )
                .ToString();
        }

        //https://rosettacode.org/wiki/Chinese_remainder_theorem#C.23
        public static class ChineseRemainderTheorem
        {
            public static long Solve(int[] n, int[] a)
            {
                long prod = n.Aggregate(1L, (i, j) => i * j);
                long p;
                long sm = 0;
                for (long i = 0; i < n.Length; i++)
                {
                    p = prod / n[i];
                    sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
                }
                return Math.Abs(sm % prod);
            }

            private static long ModularMultiplicativeInverse(long a, long mod)
            {
                long b = a % mod;
                for (long x = 1; x < mod; x++)
                {
                    if ((b * x) % mod == 1)
                    {
                        return x;
                    }
                }
                return 1;
            }
        }
    }
}
