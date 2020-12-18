using Shouldly;
using Solutions;
using Tests.Utils;

namespace Tests
{
    public class Day18Tests : DayTest<Day18>
    {
        [Input(@"2 * 3 + (4 * 5)
((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", "13658")]
        public void Part1(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part1();
            result.ShouldBe(output);
        }

        [Input(@"((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", "23340")]
        public void Part2(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part2();
            result.ShouldBe(output);
        }
    }
}
