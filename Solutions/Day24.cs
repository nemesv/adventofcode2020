using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public class Day24 : Day
    {
        public Day24(string input) : base(input)
        {
        }

        public record Tile(int X, int Y, int Z);

        public override string Part1()
        {
            return GetBlackTiles().Count.ToString();
        }

        private HashSet<Tile> GetBlackTiles()
        {
            //e, se, sw, w, nw, ne

            var lines = base.input.Split(Environment.NewLine);
            var blackTiles = new HashSet<Tile>();
            foreach (var line in lines)
            {
                var currentPosition = new Tile(0, 0, 0);
                for (int index = 0; index < line.Length; index++)
                {
                    var current = line[index];
                    switch (current)
                    {
                        case 'e':
                            currentPosition
                                = new Tile(currentPosition.X + 1, currentPosition.Y - 1, currentPosition.Z);
                            break;
                        case 'w':
                            currentPosition
                                = new Tile(currentPosition.X - 1, currentPosition.Y + 1, currentPosition.Z);
                            break;
                        case 's':
                            if (line[index + 1] == 'e')
                            {
                                currentPosition
                                    = new Tile(currentPosition.X, currentPosition.Y - 1, currentPosition.Z + 1);
                            }
                            else
                            {
                                currentPosition
                                    = new Tile(currentPosition.X - 1, currentPosition.Y, currentPosition.Z + 1);
                            }

                            index++;
                            break;
                        case 'n':
                            if (line[index + 1] == 'e')
                            {
                                currentPosition
                                    = new Tile(currentPosition.X + 1, currentPosition.Y, currentPosition.Z - 1);
                            }
                            else
                            {
                                currentPosition
                                    = new Tile(currentPosition.X, currentPosition.Y + 1, currentPosition.Z - 1);
                            }

                            index++;
                            break;
                    }
                }

                if (blackTiles.Contains(currentPosition))
                    blackTiles.Remove(currentPosition);
                else
                    blackTiles.Add(currentPosition);
            }

            return blackTiles;
        }

        private List<Tile> GetAdjacent(Tile tile)
        {
            var tiles = new List<Tile>
            {
                new(tile.X + 1, tile.Y - 1, tile.Z),
                new(tile.X - 1, tile.Y + 1, tile.Z),
                new(tile.X, tile.Y - 1, tile.Z + 1),
                new(tile.X - 1, tile.Y, tile.Z + 1),
                new(tile.X + 1, tile.Y, tile.Z - 1),
                new(tile.X, tile.Y + 1, tile.Z - 1)
            };

            return tiles;
        }

        public override string Part2()
        {
            var blackTiles = GetBlackTiles();

            for (int day = 0; day < 100; day++)
            {
                var newConfig = new HashSet<Tile>();
                foreach (var blackTile in blackTiles)
                {
                    var adjacents = GetAdjacent(blackTile);
                    var blackCount = adjacents.Count(a => blackTiles.Contains(a));
                    if (blackCount == 1 || blackCount == 2)
                    {
                        newConfig.Add(blackTile);
                    }

                    foreach (var whiteTile in adjacents.Where(a => !blackTiles.Contains(a)))
                    {
                        var whiteAdjacents = GetAdjacent(whiteTile);
                        var black = whiteAdjacents.Count(a => blackTiles.Contains(a));
                        if (black == 2)
                            newConfig.Add(whiteTile);
                    }
                }

                blackTiles = newConfig;
            }

            return blackTiles.Count.ToString();
        }
    }
}
