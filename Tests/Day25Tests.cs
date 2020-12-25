using Shouldly;
using Solutions;
using Tests.Utils;

namespace Tests
{
    public class Day25Tests : DayTest<Day25>
    {
        [Input(@"5764801
17807724", "14897079")]
        public void Part1(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part1();
            result.ShouldBe(output);
        }
    }
}
