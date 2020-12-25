using System;
using System.Linq;

namespace Solutions
{
    public class Day25 : Day
    {
        public Day25(string input) : base(input)
        {
        }

        public override string Part1()
        {
            var keys = input.Split(Environment.NewLine).Select(long.Parse).ToArray();
            var cardPublicKey = keys[0];
            var doorPublicKey = keys[1];

            var subject = 7L;
            var cardCycle = 0L;
            var value = 1L;
            while (value != cardPublicKey)
            {
                value *= subject;
                value = value % 20201227;
                cardCycle++;
            }

            var doorCycle = 0L;
            value = 1L;
            while (value != doorPublicKey)
            {
                value *= subject;
                value = value % 20201227;
                doorCycle++;
            }

            subject = cardPublicKey;
            value = 1;
            for (int i = 0; i < doorCycle; i++)
            {

                value *= subject;
                value = value % 20201227;
            }

            return value.ToString();
        }

        public override string Part2()
        {
            return "";
        }
    }
}
