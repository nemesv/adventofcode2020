using Shouldly;
using Solutions;
using Tests.Utils;

namespace Tests
{
    public class Day14Tests : DayTest<Day14>
    {
        [Input(@"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0", "165")]
        public void Part1(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part1();
            result.ShouldBe(output);
        }

        [Input(@"mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1", "208")]
        public void Part2(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part2();
            result.ShouldBe(output);
        }
    }
}
