using System;
using System.Linq;

namespace Solutions
{
    public class Day03 : Day
    {
        public Day03(string input) : base(input)
        {
        }

        public override string Part1()
        {
            return Traverse(3,1).ToString();
        }

        private int Traverse(int right, int down)
        {
            var lines = input.Split(Environment.NewLine);
            var trees = 0;
            var column = 0;
            var row = 0;
            while (row + down < lines.Length)
            {
                row += down;
                column = (column + right) % (lines[0].Length);

                if (lines[row][column] == '#')
                    trees++;
            }

            return trees;
        }

        public override string Part2()
        {
            var slopes = new long[]
            {
                Traverse(1, 1),
                Traverse(3, 1),
                Traverse(5, 1),
                Traverse(7, 1),
                Traverse(1, 2),
            };

            return slopes.Aggregate(1L, (a, c) => a * c).ToString();
        }
    }
}
