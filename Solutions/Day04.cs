using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Solutions
{
    public class Day04 : Day
    {
        public Day04(string input) : base(input)
        {
        }

        public override string Part1()
        {
            var fields = new[]
            {
                "byr",
                "iyr",
                "eyr",
                "hgt",
                "hcl",
                "ecl",
                "pid",
            };

            var currentPassword = new Dictionary<string, string>();
            var result = 0;
            foreach (var line in input.Split(Environment.NewLine))
            {
                if (line.Length == 0)
                {
                    if (fields.All(f => currentPassword.ContainsKey(f)))
                        result += 1;

                    currentPassword = new Dictionary<string, string>();
                    continue;
                }

                var parts = line.Split(" ");
                foreach (var part in parts)
                {
                    var fieldParts = part.Split(":");
                    currentPassword.Add(fieldParts[0], fieldParts[1]);
                }
            }

            if (fields.All(f => currentPassword.ContainsKey(f)))
                result += 1;

            return result.ToString();
        }

        private bool IsValid(Dictionary<string, string> fields)
        {
            var requiredFields = new[]
            {
                "byr",
                "iyr",
                "eyr",
                "hgt",
                "hcl",
                "ecl",
                "pid",
            };

            if (!requiredFields.All(fields.ContainsKey))
                return false;

            var byr = int.Parse(fields["byr"]);
            if (1920 > byr || byr > 2002)
                return false;

            var iyr = int.Parse(fields["iyr"]);
            if (2010 > iyr || iyr > 2020)
                return false;

            var eyr = int.Parse(fields["eyr"]);
            if (2020 > eyr || eyr > 2030)
                return false;

            if (fields["hgt"].Contains("cm"))
            {
                var hgt = int.Parse(fields["hgt"].TrimEnd('c', 'm'));
                if (150 > hgt || hgt > 193)
                    return false;
            }
            else if (fields["hgt"].Contains("in"))
            {
                var hgt = int.Parse(fields["hgt"].TrimEnd('i', 'n'));
                if (59 > hgt || hgt > 76)
                    return false;
            }
            else
            {
                return false;
            }

            if (!Regex.IsMatch(fields["hcl"], @"#([0-9]|[a-f]){6}"))
                return false;

            var colors = new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
            if (!colors.Contains(fields["ecl"]))
                return false;

            if (!Regex.IsMatch(fields["pid"], @"^\d{9}$"))
                return false;

            return true;
        }

        public override string Part2()
        {
            var currentPassword = new Dictionary<string, string>();
            var result = 0;
            foreach (var line in input.Split(Environment.NewLine))
            {
                if (line.Length == 0)
                {
                    if (IsValid(currentPassword))
                        result += 1;

                    currentPassword = new Dictionary<string, string>();
                    continue;
                }

                var parts = line.Split(" ");
                foreach (var part in parts)
                {
                    var fieldParts = part.Split(":");
                    currentPassword.Add(fieldParts[0], fieldParts[1]);
                }
            }

            if (IsValid(currentPassword))
                result += 1;

            return result.ToString();
        }
    }
}
