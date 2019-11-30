using Shouldly;
using Solutions;
using Tests.Utils;

namespace Tests
{
    public class Day22Tests : DayTest<Day22>
    {
        [Input(@"input", "expected")]
        public void Part1(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part1();
            result.ShouldBe(output);
        }

        [Input(@"input", "expected")]
        public void Part2(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part2();
            result.ShouldBe(output);
        }
    }
}
