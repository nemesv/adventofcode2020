using Shouldly;
using Solutions;
using Tests.Utils;

namespace Tests
{
    public class Day15Tests : DayTest<Day15>
    {
        [Input(@"0,3,6", "436")]
        public void Part1(string input, string output)
        {
            var result = Sut(input).Part1();
            result.ShouldBe(output);
        }

        [Input(@"0,3,6", "175594")]
        public void Part2(string input, string output)
        {
            var result = Sut(input).Part2();
            result.ShouldBe(output);
        }
    }
}
