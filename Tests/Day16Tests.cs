using Shouldly;
using Solutions;
using Tests.Utils;

namespace Tests
{
    public class Day16Tests : DayTest<Day16>
    {
        [Input(@"class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12", "71")]
        public void Part1(string input, string output)
        {
            var result = Sut(input).Part1();
            result.ShouldBe(output);
        }
    }
}
