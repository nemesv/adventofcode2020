using Shouldly;
using Solutions;
using Tests.Utils;

namespace Tests
{
    public class Day06Tests : DayTest<Day06>
    {
        [Input(@"abc

a
b
c

ab
ac

a
a
a
a

b", "11")]
        public void Part1(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part1();
            result.ShouldBe(output);
        }

        [Input(@"abc

a
b
c

ab
ac

a
a
a
a

b", "6")]
        public void Part2(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part2();
            result.ShouldBe(output);
        }
    }
}
