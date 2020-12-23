using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public class Day23 : Day
    {
        public Day23(string input) : base(input)
        {
        }

        public override string Part1()
        {
            var cups = input
                .Select(c => int.Parse(c.ToString()))
                .ToList();

            var rounds = 100;
            var current = 0;
            var size = cups.Count;
            for (int round = 0; round < rounds; round++)
            {
                var currentValue = cups[current];
                var pickUp = new List<int>();
                for (int i = 1; i <= 3; i++)
                {
                    var pickUpIndex = (current + i) % size;
                    pickUp.Add(cups[pickUpIndex]);
                }

                foreach (var c in pickUp)
                {
                    cups.Remove(c);
                }

                var destination = currentValue - 1;
                while (cups.IndexOf(destination) == -1)
                {
                    destination = destination - 1;
                    if (destination < 1)
                    {
                        destination = cups.Max();
                    }
                }

                var indexOfDestination = cups.IndexOf(destination);
                cups.InsertRange(indexOfDestination + 1, pickUp);

                current = (cups.IndexOf(currentValue) + 1) % size;
            }

            var indexOf1 = cups.IndexOf(1);
            return
                string.Join("", cups.Skip(indexOf1 + 1)
                    .Concat(cups.Take(indexOf1)));

        }

        public override string Part2()
        {
            var cups = input
                .Select(c => int.Parse(c.ToString()))
                .ToList();

            cups.AddRange(Enumerable.Range(10, 1_000_000-cups.Count));

            var linkedCups = new LinkedList<int>(cups);
            var index = new Dictionary<int, LinkedListNode<int>>();
            var node = linkedCups.First;
            while (node != null)
            {
                index[node.Value] = node;
                node = node.Next;
            }

            var rounds = 10_000_000;
            var current = linkedCups.First;
            for (int round = 0; round < rounds; round++)
            {
                var currentValue = current.Value;
                var pickUp = new List<LinkedListNode<int>>();
                for (int i = 1; i <= 3; i++)
                {
                    var next = current.Next;
                    if (next == null)
                        next = linkedCups.First;
                    pickUp.Add(next);
                    linkedCups.Remove(next);
                }

                var destination = currentValue - 1;
                if (destination == 0)
                    destination = linkedCups.Count + 3;
                while (pickUp.Any(n => n.Value == destination))
                {
                    destination = destination - 1;
                    if (destination < 1)
                    {
                        destination = linkedCups.Count + 3;
                    }
                }

                var destinationNode = index[destination];
                foreach (var i in pickUp)
                {
                    linkedCups.AddAfter(destinationNode, i);
                    destinationNode = i;
                }

                current = current.Next;
                if (current == null)
                    current = linkedCups.First;
            }

            var numberOne = index[1];
            return (numberOne.Next.Value * (long)numberOne.Next.Next.Value).ToString();
        }
    }
}
