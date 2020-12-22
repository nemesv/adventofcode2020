using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public class Day22 : Day
    {
        public Day22(string input) : base(input)
        {
        }

        public override string Part1()
        {
            var lines = input.Split(Environment.NewLine);
            var deck1 = new List<int>();
            var deck2 = new List<int>();
            bool player1 = true;
            foreach (var line in lines)
            {
                if (line == "Player 1:")
                    continue;
                if (line == "")
                    continue;
                if (line == "Player 2:")
                {
                    player1 = false;
                    continue;
                }
                if (player1)
                    deck1.Add(int.Parse(line));
                else
                    deck2.Add(int.Parse(line));
            }

            var rounds = 1;
            while (deck1.Count > 0 && deck2.Count > 0)
            {
                var top1 = deck1[0];
                deck1.RemoveAt(0);
                var top2 = deck2[0];
                deck2.RemoveAt(0);
                if (top1 > top2)
                {
                    deck1.Add(top1);
                    deck1.Add(top2);
                }
                else
                {
                    deck2.Add(top2);
                    deck2.Add(top1);
                }

                rounds++;
            }

            var winnerDeck = deck1.Count > 0 ? deck1 : deck2;
            var result = 0;
            for (int i = 0; i < winnerDeck.Count; i++)
            {
                result += winnerDeck[i] * (winnerDeck.Count - i);
            }

            return result.ToString();
        }

        public override string Part2()
        {
            var lines = input.Split(Environment.NewLine);
            var deck1 = new List<int>();
            var deck2 = new List<int>();
            bool player1 = true;
            foreach (var line in lines)
            {
                if (line == "Player 1:")
                    continue;
                if (line == "")
                    continue;
                if (line == "Player 2:")
                {
                    player1 = false;
                    continue;
                }
                if (player1)
                    deck1.Add(int.Parse(line));
                else
                    deck2.Add(int.Parse(line));
            }

            void PlayAGame(List<int> deck1, List<int> deck2)
            {
                var rounds = new HashSet<string>();
                while (deck1.Count > 0 && deck2.Count > 0)
                {
                    var config = string.Join(",", deck1) + "|" + string.Join(",", deck2);
                    if (rounds.Contains(config))
                    {   
                        break;
                    }
                    
                    var top1 = deck1[0];
                    deck1.RemoveAt(0);
                    var top2 = deck2[0];
                    deck2.RemoveAt(0);

                    if (top1 > deck1.Count || top2 > deck2.Count)
                    {
                        if (top1 > top2)
                        {
                            deck1.Add(top1);
                            deck1.Add(top2);
                        }
                        else
                        {
                            deck2.Add(top2);
                            deck2.Add(top1);
                        }
                    }
                    else
                    {
                        var subDeck1 = deck1.Take(top1).ToList();
                        var subDeck2 = deck2.Take(top2).ToList();
                        PlayAGame(subDeck1, subDeck2);
                        var subWinner1 = subDeck1.Count > 0;
                        if (subWinner1)
                        {
                            deck1.Add(top1);
                            deck1.Add(top2);
                        }
                        else
                        {
                            deck2.Add(top2);
                            deck2.Add(top1);
                        }
                    }

                    rounds.Add(config);
                }
            }

            PlayAGame(deck1, deck2);


            var winnerDeck = deck1.Count > 0 ? deck1 : deck2;
            var result = 0;
            for (int i = 0; i < winnerDeck.Count; i++)
            {
                result += winnerDeck[i] * (winnerDeck.Count - i);
            }

            return result.ToString();
        }
    }
}
