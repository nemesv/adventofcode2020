﻿using Shouldly;
using Solutions;
using Tests.Utils;

namespace Tests
{
    public class Day17Tests : DayTest<Day17>
    {
        [Input(@".#.
..#
###", "112")]
        public void Part1(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part1();
            result.ShouldBe(output);
        }

        [Input(@".#.
..#
###", "848")]
        public void Part2(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part2();
            result.ShouldBe(output);
        }
    }
}
