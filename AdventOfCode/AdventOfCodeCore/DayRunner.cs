using System.Diagnostics;
using System.Net;
using System.Net.Mime;

namespace AdventOfCodeCore
{
    public class DayRunner
    {
        private HttpClient _httpClient;
        private int _year;

        public DayRunner(HttpClient httpClient, int year)
        {
            _httpClient = httpClient;
            _year = year;
        }

        public async Task DownloadInput(int day)
        {
            if (Directory.Exists($@".\Input\{_year}") is false)
            {
                Directory.CreateDirectory($@".\Input\{_year}");
            }

            if (File.Exists($@".\Input\{_year}\day{day}.txt") is false)
            {
                var request = new HttpRequestMessage() 
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://adventofcode.com/{_year}/day/{day}/input"),
                };
                request.Headers.UserAgent.ParseAdd($".NET 8.0 (+via https://github.com/CedNZ/AdventOfCode by cbourneville@gmail.com)");

                var response = await _httpClient.SendAsync(request);

                var body = await response.Content.ReadAsStringAsync();

                File.WriteAllText($@".\Input\{_year}\day{day}.txt", body);
            }
        }

        public async Task SubmitAnswer(DayResult dayResult)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(dayResult.Day.ToString()), "level" },
            };
            if (dayResult.OutputB == default)
            {
                content.Add(new StringContent(dayResult.OutputA.ToString()), "answer");
            }

            //var response = await _httpClient.PostAsync($"https://adventofcode.com/{_year}/day/{dayResult.Day}/answer", content);
            //var body = await response.Content.ReadAsStringAsync();

        }

        public DayResult RunDay<TIn, TOut>(int dayNum, Func<IDayOut<TIn, TOut>> GetDay)
        {
            IDayOut<TIn, TOut> day = GetDay();

            var inputsA = day.SetupInputs(File.ReadAllLines($".\\Input\\{_year}\\day{dayNum}.txt"));
            var inputsB = day.SetupInputs(File.ReadAllLines($".\\Input\\{_year}\\day{dayNum}.txt"));

            var stopwatch = Stopwatch.StartNew();

            var outputA = day.A(inputsA);
            var runtimeA = stopwatch.Elapsed;

            try
            {
                var outputB = day.B(inputsB);
                var runtimeB = stopwatch.Elapsed - runtimeA;

                return new DayResult
                {
                    Day = dayNum,
                    OutputA = outputA,
                    OutputB = outputB,
                    RuntimeA = runtimeA,
                    RuntimeB = runtimeB
                };
            } catch (NotImplementedException)
            {

                return new DayResult
                {
                    Day = dayNum,
                    OutputA = outputA,
                    RuntimeA = runtimeA,
                };
            }
            finally
            {
                stopwatch.Stop();
            }
        }
    }

    public class DayResult
    {
        public int Day;
        public object? OutputA;
        public object? OutputB;
        public TimeSpan RuntimeA;
        public TimeSpan RuntimeB;

        public override string ToString()
        {
            return $"{Day} A: {OutputA}\t{RuntimeA}{Environment.NewLine}{Day} B: {OutputB}\t{RuntimeB}";
        }
    }
}
