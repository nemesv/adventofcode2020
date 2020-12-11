using Shouldly;
using Solutions;
using Tests.Utils;

namespace Tests
{
    public class Day11Tests : DayTest<Day11>
    {
        [Input(@"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL", "37")]
        public void Part1(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part1();
            result.ShouldBe(output);
        }

        [Input(@"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL", "26")]
        public void Part2(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part2();
            result.ShouldBe(output);
        }
    }
}
