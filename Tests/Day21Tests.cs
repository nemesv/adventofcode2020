using Shouldly;
using Solutions;
using Tests.Utils;

namespace Tests
{
    public class Day21Tests : DayTest<Day21>
    {
        [Input(@"mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)", "5")]
        public void Part1(string input, string output)
        {
            var result = Sut(input).Part1();
            result.ShouldBe(output);
        }

        [Input(@"mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)", "mxmxvkd,sqjhc,fvjkl")]
        public void Part2(string input, string output)
        {
            var result = Sut(input).Part2();
            result.ShouldBe(output);
        }
    }
}
