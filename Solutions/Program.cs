using System;
using System.IO;
using System.Reflection;

namespace Solutions
{
    class Program
    {
        static void Main(string[] args)
        {
            var dayNumber = DateTime.Now.Day;
            if (args.Length > 0)
            {
                dayNumber = int.Parse(args[0]);
            }
            var currentDate = dayNumber.ToString().PadLeft(2, '0'); ;
            var dayType = Assembly.GetExecutingAssembly().GetType($"Solutions.Day{currentDate}");
            var currentDay = (Day)Activator.CreateInstance(dayType, File.ReadAllText($"Inputs\\Day{currentDate}.txt"));
            Console.WriteLine(currentDay.Part1());
            Console.WriteLine(currentDay.Part2());

            Console.ReadKey();
        }
    }
}
