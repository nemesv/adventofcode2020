using Shouldly;
using Solutions;
using Tests.Utils;

namespace Tests
{
    public class Day23Tests : DayTest<Day23>
    {
        [Input(@"389125467", "67384529")]
        public void Part1(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part1();
            result.ShouldBe(output);
        }

        [Input(@"389125467", "149245887792")]
        public void Part2(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part2();
            result.ShouldBe(output);
        }
    }
}
