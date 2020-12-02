using Shouldly;
using Solutions;
using Tests.Utils;

namespace Tests
{
    public class Day02Tests : DayTest<Day02>
    {
        [Input(@"1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc", "2")]
        public void Part1(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part1();
            result.ShouldBe(output);
        }

        [Input(@"1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc", "1")]
        public void Part2(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part2();
            result.ShouldBe(output);
        }
    }
}
