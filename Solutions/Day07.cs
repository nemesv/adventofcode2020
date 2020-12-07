using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Solutions
{
    public class Day07 : Day
    {
        public class Bag
        {
            public string Name { get; set; }

            public List<(int, Bag)> Content { get; } = new List<(int, Bag)>();

            public List<Bag> Parents { get; } = new List<Bag>();
        }

        public Day07(string input) : base(input)
        {
        }

        public override string Part1()
        {
            var bags = GetBags();

            var shinyBag = bags["shiny gold"];
            var containers = new HashSet<string>();
            var parents = new Queue<Bag>(shinyBag.Parents);
            while (parents.Count > 0)
            {
                var p = parents.Dequeue();
                containers.Add(p.Name);
                foreach (var pParent in p.Parents)
                {
                    parents.Enqueue(pParent);
                }
            }

            return containers.Count.ToString();
        }

        public override string Part2()
        {
            var bags = GetBags();

            var shinyBag = bags["shiny gold"];
            var r = 0;
            foreach (var content in shinyBag.Content)
            {
                r += Count(content.Item2, content.Item1);
            }

            return r.ToString();
        }

        private Dictionary<string, Bag> GetBags()
        {
            var bags = new Dictionary<string, Bag>();
            foreach (var line in input.Split(Environment.NewLine))
            {
                //light red bags contain 1 bright white bag, 2 muted yellow bags.
                var parsts = line.Split(" contain ");

                var bagName = parsts[0].Substring(0, parsts[0].Length - 5);

                Bag bag;
                if (bags.ContainsKey(bagName))
                    bag = bags[bagName];
                else
                {
                    bag = new Bag() { Name = bagName };
                    bags.Add(bag.Name, bag);
                }

                if (parsts[1] != "no other bags.")
                {

                    var contents = parsts[1].Split(",");
                    foreach (var content in contents)
                    {
                        var match = Regex.Match(content, @"\s*(\d) (.*) bag");
                        bagName = match.Groups[2].Value;
                        var bagCount = int.Parse(match.Groups[1].Value);
                        if (bags.ContainsKey(bagName))
                        {
                            var bagToAdd = bags[bagName];
                            bag.Content.Add((bagCount, bagToAdd));
                            bagToAdd.Parents.Add(bag);
                        }
                        else
                        {
                            var bagToAdd = new Bag() { Name = bagName };
                            bags.Add(bagToAdd.Name, bagToAdd);
                            bag.Content.Add((bagCount, bagToAdd));
                            bagToAdd.Parents.Add(bag);
                        }
                    }
                }
            }

            return bags;
        }

        private int Count(Bag bag, int count)
        {
            var children = 1;
            foreach (var child in bag.Content)
            {
                children += Count(child.Item2, child.Item1);
            }

            if (children == 0)
                return count + 1;


            return count * children;
        }
    }
}
