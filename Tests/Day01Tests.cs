using Shouldly;
using Solutions;
using Tests.Utils;

namespace Tests
{
    public class Day01Tests : DayTest<Day01>
    {
        [Input(@"1721
            979
            366
            299
            675
            1456", "514579")]
        public void Part1(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part1();
            result.ShouldBe(output);
        }

        [Input(@"1721
979
366
299
675
1456", "241861950")]
        public void Part2(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part2();
            result.ShouldBe(output);
        }
    }
}
