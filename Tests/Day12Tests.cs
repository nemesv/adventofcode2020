using Shouldly;
using Solutions;
using Tests.Utils;

namespace Tests
{
    public class Day12Tests : DayTest<Day12>
    {
        [Input(@"F10
N3
F7
R90
F11", "25")]
        public void Part1(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part1();
            result.ShouldBe(output);
        }

        [Input(@"F10
N3
F7
R90
F11", "286")]
        public void Part2(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part2();
            result.ShouldBe(output);
        }
    }
}
