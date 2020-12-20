using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public class Day20 : Day
    {
        public Day20(string input) : base(input)
        {
        }

        public class Tile
        {
            public string Id { get; set; }
            public List<string> Lines { get; } = new();

            public bool Match(Tile left, Tile top)
            {
                if (left == null && top == null)
                    return true;

                var topMath = true;
                if (top != null)
                {
                    topMath = top.Lines.Last() == Lines.First();
                }

                var leftMatch = true;
                if (left != null)
                {
                    var leftRightSide = string.Join("", left.Lines.Select(l => l.Last()));
                    var ownLeftSide = string.Join("", Lines.Select(l => l.First()));
                    leftMatch = leftRightSide == ownLeftSide;
                }

                return topMath && leftMatch;
            }

            public Tile FlipH()
            {
                var flipped = new Tile()
                {
                    Id = Id + "|FH"

                };

                foreach (var line in Lines)
                {
                    flipped.Lines.Add(string.Join("", line.Reverse()));
                }

                return flipped;
            }

            public Tile FlipV()
            {
                var flipped = new Tile()
                {
                    Id = Id + "|FV"

                };

                foreach (var line in Lines.AsEnumerable().Reverse())
                {
                    flipped.Lines.Add(line);
                }

                return flipped;
            }

            public Tile Rotate()
            {
                var rotated = new Tile()
                {
                    Id = Id + "|R"

                };
                var result = new List<List<char>>();
                for (int row = 0; row < Height; row++)
                {
                    var r = new List<char>();
                    result.Add(r);
                    for (int column = 0; column < Width; column++)
                    {
                        r.Add(Lines[column][(Height - 1) - row]);
                    }
                }

                rotated.Lines.AddRange(result.Select(r => string.Join("", r)));
                return rotated;
            }

            public int Width => Lines[0].Length;

            public int Height => Lines.Count;

            public void Print()
            {
                Console.WriteLine();
                Console.WriteLine($"Tile {Id}");
                foreach (var line in Lines)
                {
                    Console.WriteLine(line);
                }
            }

            public string ReadId => Id.Split("|")[0];
        }

        private List<Tile> GetVariants(Tile tile)
        {
            var variants = new List<Tile>();
            var variant = tile;
            for (int i = 0; i < 4; i++)
            {
                variants.Add(variant);
                variants.Add(variant.FlipH());
                variants.Add(variant.FlipV());
                variant = variant.Rotate();
            }

            return variants;
        }

        private Dictionary<(int, int), Tile> GetArrangement()
        {
            var lines = input.Split(Environment.NewLine);
            var tiles = new List<Tile>();
            var current = new Tile();
            foreach (var line in lines)
            {
                if (line.Length == 0)
                {
                    tiles.Add(current);
                    current = new Tile();
                }
                else if (line.StartsWith("Tile "))
                {
                    current.Id = line.Substring(5, 4);
                }
                else
                {
                    current.Lines.Add(line);
                }
            }
            tiles.Add(current);



            var arrangement = new Dictionary<(int, int), Tile>();

            var workQueue = new Queue<(int, int, Tile)>();
            workQueue.Enqueue((0, 0, tiles.First()));
            arrangement[(0, 0)] = tiles.First();

            while (workQueue.Count > 0)
            {
                var currentItem = workQueue.Dequeue();
                var currentTile = currentItem.Item3;

                var others = tiles.Where(t1 => currentTile.ReadId != t1.ReadId)
                    .Where(t1 => arrangement.Values.All(a => a.ReadId != t1.ReadId))
                    .SelectMany(GetVariants).ToList();

                var tops = new List<Tile>();
                var bottoms = new List<Tile>();
                var lefts = new List<Tile>();
                var rights = new List<Tile>();

                foreach (var other in others)
                {
                    if (currentTile.Match(null, other))
                    {
                        tops.Add(other);
                        workQueue.Enqueue((currentItem.Item1 - 1, currentItem.Item2, other));
                        arrangement[(currentItem.Item1 - 1, currentItem.Item2)] = other;
                    }
                    if (other.Match(null, currentTile))
                    {
                        bottoms.Add(other);
                        workQueue.Enqueue((currentItem.Item1 + 1, currentItem.Item2, other));
                        arrangement[(currentItem.Item1 + 1, currentItem.Item2)] = other;
                    }
                    if (currentTile.Match(other, null))
                    {
                        lefts.Add(other);
                        workQueue.Enqueue((currentItem.Item1, currentItem.Item2 - 1, other));
                        arrangement[(currentItem.Item1, currentItem.Item2 - 1)] = other;

                    }
                    if (other.Match(currentTile, null))
                    {
                        rights.Add(other);
                        workQueue.Enqueue((currentItem.Item1, currentItem.Item2 + 1, other));
                        arrangement[(currentItem.Item1, currentItem.Item2 + 1)] = other;
                    }
                }
            }

            var rowOffset = -arrangement.Keys.Min(k => k.Item1);
            var columnOffset = -arrangement.Keys.Min(k => k.Item2);

            arrangement = arrangement
                .ToDictionary(old => (old.Key.Item1 + rowOffset, old.Key.Item2 + columnOffset), old => old.Value);


            return arrangement;
        }

        public override string Part1()
        {
            var arrangement = GetArrangement();
            var size = (int)Math.Sqrt(arrangement.Count);
            var result = new[]
            {
                arrangement[(0, 0)].ReadId,
                arrangement[(0, size - 1)].ReadId,
                arrangement[(size - 1, 0)].ReadId,
                arrangement[(size - 1, size - 1)].ReadId,
            };
            return result.Select(long.Parse).Aggregate(1L, (i, a) => i * a).ToString();
        }

        public override string Part2()
        {
            var arrangement = GetArrangement();
            var size = (int)Math.Sqrt(arrangement.Count);
            var bigTile = new Tile();
            for (int i = 0; i < size; i++)
            {
                var lines = Enumerable.Range(1, 8).Select(_ => "").ToList();
                for (int j = 0; j < size; j++)
                {
                    var tile = arrangement[(i, j)];
                    for (int k = 1; k < tile.Lines.Count - 1; k++)
                    {
                        lines[k - 1] += string.Join("", tile.Lines[k].Skip(1).SkipLast(1));
                    }
                }
                bigTile.Lines.AddRange(lines);
            }

            var monsterOffsets = new[]
            {
                (0, 18),
                (1, 0),
                (1, 5),
                (1, 6),
                (1, 11),
                (1, 12),
                (1, 17),
                (1, 18),
                (1, 19),
                (2, 1),
                (2, 4),
                (2, 7),
                (2, 10),
                (2, 13),
                (2, 16),
            };

            var variants = GetVariants(bigTile);

            foreach (var variant in variants)
            {
                var monsterCount = 0;
                for (int row = 0; row < variant.Height; row++)
                {
                    for (int column = 0; column < variant.Width; column++)
                    {
                        var monsterMatch = true;
                        foreach (var monsterOffset in monsterOffsets)
                        {
                            var candidate = (row + monsterOffset.Item1, column + monsterOffset.Item2);
                            if (candidate.Item1 >= variant.Height || candidate.Item2 >= variant.Width ||
                                variant.Lines[candidate.Item1][candidate.Item2] != '#')
                            {
                                monsterMatch = false;
                                break;
                            }
                        }

                        if (monsterMatch)
                        {
                            foreach (var monsterOffset in monsterOffsets)
                            {
                                var candidate = (row + monsterOffset.Item1, column + monsterOffset.Item2);
                                variant.Lines[candidate.Item1] =
                                    string.Join("",
                                        variant.Lines[candidate.Item1]
                                            .Select((c, i) => i == candidate.Item2 ? 'O' : c));
                            }
                            monsterCount++;
                        }
                    }
                }

                if (monsterCount > 0)
                    return variant.Lines.Sum(l => l.Count(l => l == '#')).ToString();
            }

            return "";
        }
    }
}
