using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Solutions
{
    public class Day16 : Day
    {
        public Day16(string input) : base(input)
        {
        }

        public override string Part1()
        {
            var lines = input.Split(Environment.NewLine);
            var part = 0;
            var rules = new List<(string Name, (int S, int E) first, (int S, int E) second)>();
            var ownTicket = new List<int>();
            var nearByTickets = new List<List<int>>();
            foreach (var line in lines)
            {
                if (line == "your ticket:")
                {
                    part = 1;
                    continue;
                }
                if (line == "nearby tickets:")
                {
                    part = 2;
                    continue;
                }
                if (line.Length == 0)
                    continue;

                switch (part)
                {
                    case 0:
                        var parts = Regex.Match(line, @"(\w+): (\d+)-(\d+) or (\d+)-(\d+)");
                        var firstL = int.Parse(parts.Groups[2].Value);
                        var firstU = int.Parse(parts.Groups[3].Value);
                        var secondL = int.Parse(parts.Groups[4].Value);
                        var secondU = int.Parse(parts.Groups[5].Value);
                        rules.Add((parts.Groups[1].Value, (firstL, firstU), (secondL, secondU)));
                        break;
                    case 1:
                        ownTicket = line.Split(",").Select(int.Parse).ToList();
                        break;
                    case 2:
                        nearByTickets.Add(line.Split(",").Select(int.Parse).ToList());
                        break;
                }
            }

            var result = 0;

            foreach (var ticket in nearByTickets)
            {
                foreach (var field in ticket)
                {
                    var ruleMatch = false;
                    foreach (var rule in rules)
                    {
                        if ((rule.first.S <= field && field <= rule.first.E) ||
                            (rule.second.S <= field && field <= rule.second.E))
                        {
                            ruleMatch = true;
                            break;
                        }
                    }

                    if (!ruleMatch)
                        result += field;
                }
            }

            return result.ToString();
        }

        public override string Part2()
        {
            var lines = input.Split(Environment.NewLine);
            var part = 0;
            var rules = new List<(string Name, (int S, int E) first, (int S, int E) second)>();
            var ownTicket = new List<int>();
            var nearByTickets = new List<List<int>>();
            foreach (var line in lines)
            {
                if (line == "your ticket:")
                {
                    part = 1;
                    continue;
                }

                if (line == "nearby tickets:")
                {
                    part = 2;
                    continue;
                }

                if (line.Length == 0)
                    continue;

                switch (part)
                {
                    case 0:
                        var parts = Regex.Match(line, @"(.*): (\d+)-(\d+) or (\d+)-(\d+)");
                        var firstL = int.Parse(parts.Groups[2].Value);
                        var firstU = int.Parse(parts.Groups[3].Value);
                        var secondL = int.Parse(parts.Groups[4].Value);
                        var secondU = int.Parse(parts.Groups[5].Value);
                        rules.Add((parts.Groups[1].Value, (firstL, firstU), (secondL, secondU)));
                        break;
                    case 1:
                        ownTicket = line.Split(",").Select(int.Parse).ToList();
                        break;
                    case 2:
                        nearByTickets.Add(line.Split(",").Select(int.Parse).ToList());
                        break;
                }
            }

            var result = 1L;
            var validTickets = new List<List<int>>();
            foreach (var ticket in nearByTickets)
            {
                var isValid = true;
                foreach (var field in ticket)
                {
                    var ruleMatch = false;
                    foreach (var rule in rules)
                    {
                        if ((rule.first.S <= field && field <= rule.first.E) ||
                            (rule.second.S <= field && field <= rule.second.E))
                        {
                            ruleMatch = true;
                            break;
                        }
                    }

                    if (!ruleMatch)
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                    validTickets.Add(ticket);
            }

            var matchingRules = new Dictionary<int, List<(string Name, (int S, int E) first, (int S, int E) second)>>();
            validTickets.Add(ownTicket);
            for (int i = 0; i < rules.Count; i++)
            {
                var possibleValues = validTickets.Select(t => t[i]).ToList();
                foreach (var rule in rules)
                {
                    var ruleMatch = true;
                    foreach (var field in possibleValues)
                    {
                        if (rule.first.S <= field && field <= rule.first.E ||
                            rule.second.S <= field && field <= rule.second.E)
                        {
                        }
                        else
                        {
                            ruleMatch = false;
                            break;
                        }
                    }

                    if (ruleMatch)
                    {
                        if (!matchingRules.ContainsKey(i))
                            matchingRules[i] = new List<(string Name, (int S, int E) first, (int S, int E) second)>();
                        matchingRules[i].Add(rule);
                    }
                }
            }

            var rulesInOrder = new Dictionary<int, (string Name, (int S, int E) first, (int S, int E) second)>();
            while (true)
            {
                var ruleToRemove = default((string Name, (int S, int E) first, (int S, int E) second));
                foreach (var matchingRule in matchingRules)
                {
                    if (matchingRule.Value.Count == 1)
                    {
                        rulesInOrder[matchingRule.Key] = matchingRule.Value.First();
                        ruleToRemove = rulesInOrder[matchingRule.Key];
                        break;
                    }
                }

                if (ruleToRemove == default((string Name, (int S, int E) first, (int S, int E) second)))
                    break;

                foreach (var matchingRule in matchingRules)
                {
                    matchingRule.Value.Remove(ruleToRemove);
                }

            }

            foreach (var rule in rulesInOrder)
            {
                if (rule.Value.Name.StartsWith("departure"))
                {
                    result *= ownTicket[rule.Key];
                }
            }

            return result.ToString();
        }
    }
}
