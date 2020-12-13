using Shouldly;
using Solutions;
using Tests.Utils;

namespace Tests
{
    public class Day13Tests : DayTest<Day13>
    {
        [Input(@"939
7,13,x,x,59,x,31,19", "295")]
        public void Part1(string input, string output)
        {
            var result = Sut(input).Part1();
            result.ShouldBe(output);
        }

        [Input(@"939
17,x,13,19", "3417")]
        [Input(@"939
67,7,59,61", "754018")]
        [Input(@"939
67,x,7,59,61", "779210")]
        [Input(@"939
1789,37,47,1889", "1202161486")]
        public void Part2(string input, string output)
        {
            var result = Sut(input).Part2();
            result.ShouldBe(output);
        }
    }
}
