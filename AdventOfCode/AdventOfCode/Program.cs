using System.Linq.Expressions;
using System.Net;
using System.Reflection;
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
                        2024 => new AoC_2024.AoC_2024(new DayRunner(httpClient, yearNum)),
                        2025 => new AoC_2025.AoC_2025(new DayRunner(httpClient, yearNum)),
                        _ => new AoC_2024.AoC_2024(new DayRunner(httpClient, yearNum)),
                    };

                    Console.WriteLine("Enter day number: ");
                    var dayNum = double.Parse(Console.ReadLine()!);

                    var dayType = typeof(AoC_2025.AoC_2025)
                        .Assembly
                        .ExportedTypes
                        .FirstOrDefault(x => x.Name == $"Day{dayNum}")
                        ?? throw new Exception("Failed to find day");

                    var interfaceType = dayType.GetInterfaces()
                        .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDayOut<,>))
                        ?? throw new Exception("Failed to find interface");

                    var genericArgs = interfaceType.GetGenericArguments();
                    var tIn = genericArgs[0];
                    var tOut = genericArgs[1];

                    var ctor = dayType.GetConstructor(Type.EmptyTypes) ?? throw new Exception("Can't find day ctor");
                    var newEpr = Expression.New(ctor);
                    var castExpr = Expression.Convert(newEpr, interfaceType);
                    var funcType = typeof(Func<>).MakeGenericType(interfaceType);
                    var lambda = Expression.Lambda(funcType, castExpr);
                    var dayFunc = lambda.Compile();

                    var runOpen = year.GetType()
                        .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy)
                        .FirstOrDefault(m =>
                            m is { Name: "RunDayAsync", IsGenericMethodDefinition: true }
                            && m.GetGenericArguments().Length == 2)
                        ?? throw new Exception("Can't make run method");

                    var runner = runOpen.MakeGenericMethod(tIn, tOut);

                    var task = runner.Invoke(year, [dayNum, dayFunc]);
                    var result = await ((Task<DayResult>)task!).ConfigureAwait(false);

                    Console.WriteLine(result);
                    Console.WriteLine("\nRerun? y/N");
                    run = Console.ReadLine()?.ToUpper() == "Y";
                }
            }
        }
    }
}