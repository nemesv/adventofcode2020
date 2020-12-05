using Shouldly;
using Solutions;
using Tests.Utils;

namespace Tests
{
    public class Day05Tests : DayTest<Day05>
    {
        [Input(@"BBFFBBFRLL
BFFFBBFRRR
FFFBBBFRRR", "820")]
        public void Part1(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part1();
            result.ShouldBe(output);
        }
    }
}
