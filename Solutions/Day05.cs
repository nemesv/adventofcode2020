using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Solutions
{
    public class Day05 : Day
    {
        public Day05(string input) : base(input)
        {
        }

        public override string Part1()
        {
            var passes = GetPasses();

            return passes.Max(p => p.Id).ToString();
        }

        public override string Part2()
        {
            var passes = GetPasses().OrderBy(p => p.Id).ToList();

            var pass = 0;
            for (int i = 0; i < passes.Count - 1; i++)
            {
                if (passes[i + 1].Id - passes[i].Id == 2)
                {
                    pass = passes[i + 1].Id - 1;
                }
            }

            return pass.ToString();
        }

        private List<(int Row, int Column, int Id)> GetPasses()
        {
            //0-127 F-B
            //0-7 L-R
            var passes = new List<(int Row, int Column, int Id)>();
            foreach (var line in input.Split(Environment.NewLine))
            {
                var rowMin = 0;
                var rowMax = 127;
                var row = 0;
                for (int i = 0; i < 7; i++)
                {
                    if (line[i] == 'F')
                    {
                        rowMax -= (rowMax + 1 - rowMin) / 2;
                        row = rowMax;
                    }
                    else
                    {
                        rowMin += (rowMax + 1 - rowMin) / 2;
                        row = rowMin;
                    }
                }
                var columnMin = 0;
                var columnMax = 7;
                var column = 0;
                for (int j = 7; j < 10; j++)
                {
                    if (line[j] == 'L')
                    {
                        columnMax -= (columnMax + 1 - columnMin) / 2;
                        column = columnMax;
                    }
                    else
                    {
                        columnMin += (columnMax + 1 - columnMin) / 2;
                        column = columnMin;
                    }
                }
                passes.Add((row, column, row * 8 + column));
            }

            return passes;
        }
    }
}
