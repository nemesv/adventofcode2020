using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public class Day11 : Day
    {
        public Day11(string input) : base(input)
        {
        }

        public override string Part1()
        {
            var currentLayout = input.Split(Environment.NewLine)
                .Select(s => s.Select(c => c.ToString()).ToList()).ToList();

            var iterations = 0;
            while (true)
            {
                var newLayout = new List<List<string>>();

                for (int rowIndex = 0; rowIndex < currentLayout.Count; rowIndex++)
                {
                    newLayout.Add(new List<string>());
                    for (int columnIndex = 0; columnIndex < currentLayout[0].Count; columnIndex++)
                    {
                        var current = currentLayout[rowIndex][columnIndex];

                        if (current == ".")
                        {
                            newLayout[rowIndex].Add(".");
                            continue;
                        }

                        var occupied = GetNeighbors(currentLayout, rowIndex, columnIndex)
                            .Count(n => n == "#");

                        if (current == "L")
                        {
                            newLayout[rowIndex].Add(current == "L" && occupied == 0 ? "#" : "L");
                            continue;
                        }

                        newLayout[rowIndex].Add(current == "#" && occupied >= 4 ? "L" : "#");
                    }
                }

                iterations++;

                if (LayoutToString(currentLayout) == LayoutToString(newLayout))
                    break;

                currentLayout = newLayout;
            }

            return currentLayout.Sum(r => r.Count(s => s == "#")).ToString();
        }

        private List<string> GetNeighbors(
            List<List<string>> layout, int row, int column)
        {
            var result = new List<string>();

            var offsets = new int[] {-1, 0, 1};
            foreach (var rowOffset in offsets)
            {
                foreach (var columnOffset in offsets)
                {
                    if (rowOffset == 0 && columnOffset == 0)
                        continue;

                    var cRow = row + rowOffset;
                    var cCloumn = column + columnOffset;

                    if (cRow > -1 && cRow < layout.Count)
                    {
                        if (cCloumn > -1 && cCloumn < layout[cRow].Count)
                        {
                            result.Add(layout[cRow][cCloumn]);
                        }
                    }
                }
            }
 
            return result;
        }

        private string LayoutToString(IEnumerable<IEnumerable<string>> layout)
        {
            return String.Join("", layout.Select(s => string.Join("",s)));
        }

        public override string Part2()
        {
            var currentLayout = input.Split(Environment.NewLine)
                .Select(s => s.Select(c => c.ToString()).ToList()).ToList();

            var iterations = 0;
            while (true)
            {
                var newLayout = new List<List<string>>();

                for (int rowIndex = 0; rowIndex < currentLayout.Count; rowIndex++)
                {
                    newLayout.Add(new List<string>());
                    for (int columnIndex = 0; columnIndex < currentLayout[0].Count; columnIndex++)
                    {
                        var current = currentLayout[rowIndex][columnIndex];

                        if (current == ".")
                        {
                            newLayout[rowIndex].Add(".");
                            continue;
                        }

                        var occupied = GetOccupied(currentLayout, rowIndex, columnIndex)
                            .Count(n => n == "#");

                        if (current == "L")
                        {
                            newLayout[rowIndex].Add(current == "L" && occupied == 0 ? "#" : "L");
                            continue;
                        }

                        newLayout[rowIndex].Add(current == "#" && occupied >= 5 ? "L" : "#");
                    }
                }

                iterations++;

                if (LayoutToString(currentLayout) == LayoutToString(newLayout))
                    break;

                currentLayout = newLayout;
            }

            return currentLayout.Sum(r => r.Count(s => s == "#")).ToString();
        }

        private List<string> GetOccupied(
            List<List<string>> layout, int row, int column)
        {
            var result = new List<string>();

            var offsets = new int[] { -1, 0, 1 };
            foreach (var rowOffset in offsets)
            {
                foreach (var columnOffset in offsets)
                {
                    if (rowOffset == 0 && columnOffset == 0)
                        continue;

                    var cRow = row + rowOffset;
                    var cCloumn = column + columnOffset;


                    while (true)
                    {
                        if (cRow > -1 && cRow < layout.Count)
                        {
                            if (cCloumn > -1 && cCloumn < layout[cRow].Count)
                            {
                                var current = layout[cRow][cCloumn];
                                if (current == ".")
                                {
                                    cRow += rowOffset;
                                    cCloumn += columnOffset;
                                    continue;
                                }
                                result.Add(current);
                            }
                        }
                        break;
                    }
                }
            }

            return result;
        }
    }
}
