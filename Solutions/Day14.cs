using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Solutions
{
    public class Day14 : Day
    {
        public Day14(string input) : base(input)
        {
        }

        public override string Part1()
        {
            var lines = input.Split(Environment.NewLine);
            var currentMask = new List<char>();
            var mem = new Dictionary<long, long>();
            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    currentMask = line.Substring(7).ToList();
                }
                else
                {
                    var parts = Regex.Match(line, @"mem\[(\d+)\] = (\d+)");
                    var address = long.Parse(parts.Groups[1].Value);
                    var value = long.Parse(parts.Groups[2].Value);

                    var valueInBits = Convert.ToString(value, 2).PadLeft(36, '0');
                    var result = new List<char>();
                    for (int i = 0; i < currentMask.Count; i++)
                    {
                        var maskBit = currentMask[i];
                        var valueBit = valueInBits[i];
                        if (maskBit == 'X')
                        {
                            result.Add(valueBit);
                        }
                        else
                        {
                            result.Add(maskBit);
                        }
                    }

                    var newValue = Convert.ToInt64(string.Join("", result), 2);
                    mem[address] = newValue;
                }
            }

            return mem.Values.Sum().ToString();
        }

        public override string Part2()
        {
            var lines = input.Split(Environment.NewLine);
            var currentMask = new List<char>();
            var mem = new Dictionary<long, long>();
            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    currentMask = line.Substring(7).ToList();
                }
                else
                {
                    var parts = Regex.Match(line, @"mem\[(\d+)\] = (\d+)");
                    var address = long.Parse(parts.Groups[1].Value);
                    var value = long.Parse(parts.Groups[2].Value);

                    var addressInBits = Convert.ToString(address, 2).PadLeft(36, '0');
                    var result = new List<char>();
                    for (int i = 0; i < currentMask.Count; i++)
                    {
                        var maskBit = currentMask[i];
                        var addressBit = addressInBits[i];
                        if (maskBit == '0')
                        {
                            result.Add(addressBit);
                        }
                        else
                        {
                            result.Add(maskBit);
                        }
                    }

                 
                    var newAddresses = new Queue<List<char>>();
                    newAddresses.Enqueue(result);

                    while (true)
                    {
                        var current = newAddresses.Dequeue();

                        var firstFloating = current.IndexOf('X');
                        if (firstFloating == -1)
                        {
                            newAddresses.Enqueue(current);
                            break;
                        }

                        var l1 = current.ToList();
                        var l2 = current.ToList();
                        l1[firstFloating] = '0';
                        l2[firstFloating] = '1';

                        newAddresses.Enqueue(l1);
                        newAddresses.Enqueue(l2);
                    }

                    foreach (var newAddressA in newAddresses)
                    {
                        var newAddress = Convert.ToInt64(string.Join("", newAddressA), 2);
                        mem[newAddress] = value;
                    }
                }
            }

            return mem.Values.Sum().ToString();
        }
    }
}
