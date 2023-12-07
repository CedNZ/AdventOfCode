using System.Net;
using AdventOfCodeCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdventOfCode
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddTransient<AdventOfCodeRunner>();
                })
                .ConfigureHostConfiguration(configure =>
                {
                    configure.AddEnvironmentVariables()
                        .AddCommandLine(args)
                        .AddJsonFile("appsettings.json");
                })
                .ConfigureAppConfiguration(configure =>
                    configure.AddJsonFile("appsettings.json"));

            var host = builder.Build();

            var runner = host.Services.GetService<AdventOfCodeRunner>();
            var config = host.Services.GetService<IConfiguration>();


            await runner!.ExecuteAsync(config!["Values:AdventOfCodeSession"]!);
        }

        internal class AdventOfCodeRunner
        {
            public async Task ExecuteAsync(string sessionCode)
            {
                Console.WriteLine("Welcome to Advent of Code");

                HttpClientHandler httpHandler = new();
                httpHandler.CookieContainer.Add(
                    new Cookie
                    (
                        "session",
                        sessionCode,
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
                        2015 => new AoC_2015.AoC_2015(new DayRunner(httpClient, yearNum)),
                        2018 => new AoC_2018.AoC_2018(new DayRunner(httpClient, yearNum)),
                        2021 => new AoC_2021.AoC_2021(new DayRunner(httpClient, yearNum)),
                        2022 => new AoC_2022.AoC_2022(new DayRunner(httpClient, yearNum)),
                        2023 => new AoC_2023.AoC_2023(new DayRunner(httpClient, yearNum)),
                        _ => new AoC_2023.AoC_2023(new DayRunner(httpClient, yearNum)),
                    };

                    Console.WriteLine("Enter day number: ");
                    var dayNum = double.Parse(Console.ReadLine()!);

                    var result = await year.RunDayAsync(dayNum);

                    Console.WriteLine(result);
                    Console.WriteLine("\nRerun? y/N");
                    run = Console.ReadLine()?.ToUpper() == "Y";
                }
            }
        }
    }
}