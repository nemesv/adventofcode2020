using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public class Day19 : Day
    {
        public Day19(string input) : base(input)
        {
        }

        public class Rule
        {
            public int Id { get; set; }

            public string Value { get; set; }

            public List<List<Rule>> Children { get; } = new List<List<Rule>>();
        }

        public override string Part1()
        {
            var lines = input.Split(Environment.NewLine);
            var rules = lines.TakeWhile(l => l.Length > 0).ToList();
            var messages = lines.SkipWhile(l => l.Length > 0).Skip(1).ToList();
            var parsedRules = new Dictionary<string, List<List<string>>>();
            foreach (var rule in rules)
            {
                var mainParts = rule.Split(":");
                var id = mainParts[0];

                if (rule.Contains('"'))
                {
                    parsedRules[id] = new List<List<string>> { new List<string>() { mainParts[1].Trim().Trim('"') } };
                }
                else
                {
                    var details = mainParts[1].Trim().Split("|");
                    var options = new List<List<string>>();
                    foreach (var detail in details)
                    {
                        options.Add(detail.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList());
                    }

                    parsedRules[id] = options;
                }
            }

            //0: 4 1 5
            //1: 2 3 | 3 2
            //2: 4 4 | 5 5
            //3: 4 5 | 5 4
            //4: "a"
            //5: "b"

            var resolved = new Dictionary<string, List<string>>();
            foreach (var rule in parsedRules)
            {
                if (rule.Value[0][0] == "a" || rule.Value[0][0] == "b")
                {
                    resolved[rule.Key] = new List<string>() { rule.Value[0][0] };
                }
            }
            while (!resolved.ContainsKey("0"))
            {
                foreach (var parsedRule in parsedRules)
                {
                    if (resolved.ContainsKey(parsedRule.Key))
                        continue;

                    var canResolve = true;
                    var r = new List<string>() { };
                    foreach (var option in parsedRule.Value)
                    {
                        var partResult = new List<string>();
                        foreach (var value in option)
                        {
                            if (!resolved.ContainsKey(value))
                            {
                                canResolve = false;
                                break;
                            }

                            if (partResult.Count == 0)
                            {
                                partResult.AddRange(resolved[value]);
                            }
                            else
                            {
                                var n = new List<string>();
                                foreach (var res in resolved[value])
                                {
                                    n.AddRange(partResult.Select(p => p + res));
                                }

                                partResult = n;
                            }
                        }
                        if (!canResolve)
                            break;
                        r.AddRange(partResult);
                    }

                    if (canResolve)
                    {
                        resolved[parsedRule.Key] = r;
                    }
                }
            }

            var allSolutions = new HashSet<string>(resolved["0"]);
            return messages.Count(m => allSolutions.Contains(m)).ToString();
        }

        public override string Part2()
        {
            var lines = input.Split(Environment.NewLine);
            var rules = lines.TakeWhile(l => l.Length > 0).ToList();
            var messages = lines.SkipWhile(l => l.Length > 0).Skip(1).ToList();
            var parsedRules = new Dictionary<string, List<List<string>>>();
            foreach (var rule in rules)
            {
                var mainParts = rule.Split(":");
                var id = mainParts[0];

                if (rule.Contains('"'))
                {
                    parsedRules[id] = new List<List<string>> { new List<string>() { mainParts[1].Trim().Trim('"') } };
                }
                else
                {
                    var details = mainParts[1].Trim().Split("|");
                    var options = new List<List<string>>();
                    foreach (var detail in details)
                    {
                        options.Add(detail.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList());
                    }

                    parsedRules[id] = options;
                }
            }
            
            var resolved = new Dictionary<string, List<string>>();
            foreach (var rule in parsedRules)
            {
                if (rule.Value[0][0] == "a" || rule.Value[0][0] == "b")
                {
                    resolved[rule.Key] = new List<string>() { rule.Value[0][0] };
                }
            }

            var maxMessageLength = messages.Max(m => m.Length);
            while (!resolved.ContainsKey("0"))
            {
                foreach (var parsedRule in parsedRules)
                {
                    if (resolved.ContainsKey(parsedRule.Key))
                        continue;

                    var canResolve = true;
                    var r = new List<string>() { };
                    foreach (var option in parsedRule.Value)
                    {
                        var partResult = new List<string>();
                        foreach (var value in option)
                        {
                            if (!resolved.ContainsKey(value))
                            {
                                canResolve = false;
                                break;
                            }

                            if (partResult.Count == 0)
                            {
                                partResult.AddRange(resolved[value]);
                            }
                            else
                            {
                                var n = new List<string>();
                                foreach (var res in resolved[value])
                                {
                                    n.AddRange(partResult.Select(p => p + res).Where(s => s.Length <= maxMessageLength));
                                }

                                partResult = n;
                            }
                        }
                        if (!canResolve)
                            break;
                        r.AddRange(partResult);
                    }

                    if (canResolve)
                    {
                            resolved[parsedRule.Key] = r;
                    }
                }
            }

            var result = 0;
            var patternLength = resolved["8"][0].Length;
            foreach (var message in messages)
            {
                if (message.Length % patternLength != 0)
                    continue;

                var patterns = new List<int>();
                for (int i = 0; i < message.Length / patternLength; i++)
                {
                    //0 7 15
                    var part = message.Substring(i*patternLength, patternLength);
                    if (resolved["42"].Contains(part))
                        patterns.Add(42);
                    else if (resolved["31"].Contains(part))
                        patterns.Add(31);
                    else
                    {
                        patterns.Clear();
                        break;
                    }
                }

                if (patterns[0] == 42 && patterns.Last() == 31)
                {
                    var firstPart = patterns.TakeWhile(p => p == 42).ToList();
                    var secondPart = patterns.SkipWhile(p => p == 42).ToList();
                    
                    if (firstPart.Count > secondPart.Count && secondPart.All(p => p == 31))
                        result++;
                }
            }
            
            return result.ToString();
        }
    }
}
