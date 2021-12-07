using AdventOfCodeCore;

namespace AdventOfCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Advent of Code 2021");

            Year year = new AoC_2021.AoC_2021(new DayRunner());

            var run = true;
            while (run)
            {
                Console.WriteLine("Enter day number: ");
                var dayNum = int.Parse(Console.ReadLine());

                var result = year.RunDay(dayNum);

                Console.WriteLine(result);
                Console.WriteLine("\nRerun? y/N");
                run = Console.ReadLine()?.ToUpper() == "Y";
            }

        }
    }
}