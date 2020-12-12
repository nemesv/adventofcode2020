using System;
using System.Linq;

namespace Solutions
{
    public class Day12 : Day
    {
        public Day12(string input) : base(input)
        {
        }

        public override string Part1()
        {
            var actions = input.Split(Environment.NewLine)
                .Select(a => (a[0], int.Parse(a[1..]))).ToArray();
            var position = (0, 0);
            var direction = 'E';
            var directions = new[] {'N', 'E', 'S', 'W'};

            foreach (var action in actions)
            {
                switch (action.Item1)
                {
                    case 'N':
                        position = (position.Item1 + action.Item2, position.Item2);
                        break;
                    case 'S':
                        position = (position.Item1 - action.Item2, position.Item2);
                        break;
                    case 'E':
                        position = (position.Item1, position.Item2 + action.Item2);
                        break;
                    case 'W':
                        position = (position.Item1, position.Item2 - action.Item2);
                        break;
                    case 'L':
                        direction = directions[(Array.IndexOf(directions, direction) + 4 - action.Item2 / 90) % 4];
                        break;
                    case 'R':
                        direction = directions[(Array.IndexOf(directions, direction) + action.Item2 / 90) % 4];

                        break;
                    case 'F':

                        switch (direction)
                        {
                            case 'N':
                                position = (position.Item1 + action.Item2, position.Item2);
                                break;
                            case 'S':
                                position = (position.Item1 - action.Item2, position.Item2);
                                break;
                            case 'E':
                                position = (position.Item1, position.Item2 + action.Item2);
                                break;
                            case 'W':
                                position = (position.Item1, position.Item2 - action.Item2);
                                break;
                        }

                        break;
                }
            }

            return (Math.Abs(position.Item1) + Math.Abs(position.Item2)).ToString();
        }

        public override string Part2()
        {
            var actions = input.Split(Environment.NewLine)
                .Select(a => (a[0], int.Parse(a[1..]))).ToArray();
            var position = (0, 0);
            var waypoint = (1, 10);

            foreach (var action in actions)
            {
                switch (action.Item1)
                {
                    case 'N':
                        waypoint = (waypoint.Item1 + action.Item2, waypoint.Item2);
                        break;
                    case 'S':
                        waypoint = (waypoint.Item1 - action.Item2, waypoint.Item2);
                        break;
                    case 'E':
                        waypoint = (waypoint.Item1, waypoint.Item2 + action.Item2);
                        break;
                    case 'W':
                        waypoint = (waypoint.Item1, waypoint.Item2 - action.Item2);
                        break; 
                    //https://math.stackexchange.com/questions/1330161/how-to-rotate-points-through-90-degree
                    case 'R':
                        for (int i = 0; i < action.Item2 / 90; i++)
                        {
                            waypoint = (-waypoint.Item2, waypoint.Item1);
                        }
                        break;
                    case 'L':
                        for (int i = 0; i < action.Item2 / 90; i++)
                        {
                            waypoint = (waypoint.Item2, -waypoint.Item1);
                        }
                        break;
                    case 'F':
                        position = (position.Item1 + waypoint.Item1 * action.Item2,
                            position.Item2 + waypoint.Item2 * action.Item2);
                        break;
                }
            }

            return (Math.Abs(position.Item1) + Math.Abs(position.Item2)).ToString();
        }
    }
}
