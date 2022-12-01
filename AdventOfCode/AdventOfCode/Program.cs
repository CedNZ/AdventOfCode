using System.Net;
using AdventOfCodeCore;

namespace AdventOfCode
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to Advent of Code 2021");

            HttpClientHandler httpHandler = new();
            httpHandler.CookieContainer.Add(
                new Cookie
                (
                    "session",
                    "53616c7465645f5f3e6b8ab2e5cae1b8817808907c869343934d17a8adf484a8f16d56e025d9e8b63ea01e494434c23cb102972d7387707c06a77f48a6aaacb0",
                    "/",
                    "adventofcode.com"
                ));
            HttpClient httpClient = new(httpHandler);

            var run = true;
            while (run)
            {
                Console.WriteLine("Enter Year: ");
                var yearNum = int.TryParse(Console.ReadLine(), out var num) ? num : DateTime.Now.Year;
                Year? year = yearNum switch
                {
                    2018 => new AoC_2018.AoC_2018(new DayRunner(httpClient, yearNum)),
                    2021 => new AoC_2021.AoC_2021(new DayRunner(httpClient, yearNum)),
                    _ => new AoC_2021.AoC_2021(new DayRunner(httpClient, yearNum)),
                };

                Console.WriteLine("Enter day number: ");
                var dayNum = int.Parse(Console.ReadLine());

                var result = await year.RunDayAsync(dayNum);

                Console.WriteLine(result);
                Console.WriteLine("\nRerun? y/N");
                run = Console.ReadLine()?.ToUpper() == "Y";
            }

        }
    }
}