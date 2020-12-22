using Shouldly;
using Solutions;
using Tests.Utils;

namespace Tests
{
    public class Day22Tests : DayTest<Day22>
    {
        [Input(@"Player 1:
9
2
6
3
1

Player 2:
5
8
4
7
10", "306")]
        public void Part1(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part1();
            result.ShouldBe(output);
        }

        [Input(@"Player 1:
9
2
6
3
1

Player 2:
5
8
4
7
10", "291")]
        public void Part2(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part2();
            result.ShouldBe(output);
        }
    }
}
