using Shouldly;
using Solutions;
using Tests.Utils;

namespace Tests
{
    public class Day09Tests : DayTest<Day09>
    {
        [Input(@"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576", "127")]
        public void Part1(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part1(5);
            result.ShouldBe(output);
        }

        [Input(@"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576", "62")]
        public void Part2(string input, string output)
        {
            var result = Sut(input.Replace(",", "\r\n")).Part2(127);
            result.ShouldBe(output);
        }
    }
}
