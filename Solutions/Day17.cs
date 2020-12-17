using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public class Day17 : Day
    {
        public Day17(string input) : base(input)
        {
        }

        public override string Part1()
        {
            return Solve(false);
        }

        public override string Part2()
        {
            return Solve(true);
        }

        public record Point(int X, int Y, int Z, int W);

        public string Solve(bool includeW)
        {
            var lines = input.Split(Environment.NewLine).ToList();
            var activeCells = new HashSet<Point>();
            for (int y = 0; y < lines.Count; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    if (lines[y][x] == '#')
                        activeCells.Add(new Point(x, y, 0, 0));
                }
            }

            var cycles = 6;
            for (int cycle = 0; cycle < cycles; cycle++)
            {
                var newActive = new HashSet<Point>();
                foreach (var activeCell in activeCells)
                {
                    var neighbors = GetNeighbors(activeCell, includeW);

                    var activeNeighbors = neighbors.Where(n => activeCells.Contains(n)).ToList();
                    var inactiveNeighbors = neighbors.Where(n => !activeCells.Contains(n)).ToList();
                    if (activeNeighbors.Count == 2 || activeNeighbors.Count == 3)
                        newActive.Add(activeCell);

                    foreach (var inactiveNeighbor in inactiveNeighbors)
                    {
                        var candidates = GetNeighbors(inactiveNeighbor, includeW);
                        var activeCandidates = candidates.Where(n => activeCells.Contains(n)).ToList();
                        if (activeCandidates.Count == 3)
                            newActive.Add(inactiveNeighbor);
                    }
                }

                activeCells = newActive;
            }

            return activeCells.Count.ToString();
        }

        private List<Point> GetNeighbors(Point p, bool includeW)
        {
            var offsets = new [] { -1, 0, 1 };
            var result = new List<Point>();
            foreach (var xOffset in offsets)
            {
                foreach (var yOffSet in offsets)
                {
                    foreach (var zOffset in offsets)
                    {
                        if (includeW)
                        {
                            foreach (var wOffset in offsets)
                            {
                                result.Add(new Point(p.X + xOffset, p.Y + yOffSet, p.Z + zOffset, p.W + wOffset));
                            }
                        }
                        else
                            result.Add(new Point(p.X + xOffset, p.Y + yOffSet, p.Z + zOffset, 0));
                    }
                }
            }

            result.Remove(p);
            return result;
        }
    }
}
