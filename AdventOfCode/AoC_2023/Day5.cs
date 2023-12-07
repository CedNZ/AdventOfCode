using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day5 : IDay<Maps>
    {
        public long A(List<Maps> inputs)
        {
            var min = long.MaxValue;
            var maps = inputs[0];
            foreach (var seed in maps.Seeds)
            {
                var soil = GetMapping(seed, maps.SeedToSoil);
                var fertilizer = GetMapping(soil, maps.SoilToFertilizer);
                var water = GetMapping(fertilizer, maps.FertilizerToWater);
                var light = GetMapping(water, maps.WaterToLight);
                var temperature = GetMapping(light, maps.LightToTemperature);
                var humidity = GetMapping(temperature, maps.TemperatureToHumidity);
                var location = GetMapping(humidity, maps.HumidityToLocation);

                min = Math.Min(min, location);
            }
            return min;
        }

        private long GetMapping(long num, List<Map> map)
        {
            try
            {
                var m = map.First(x => num >= x.RangeStart && num < x.RangeEnd);
                return num - m.Diff;
            } catch
            {
                return num;
            }
        }

        public long B(List<Maps> inputs)
        {
            var min = long.MaxValue;
            var maps = inputs[0];
            foreach (var seedChunk in maps.Seeds.Chunk(2))
            {
                var seeds = new List<(long start, long end)>
                {
                    (seedChunk[0], seedChunk[0] + seedChunk[1] - 1)
                };

                var soils = seeds.Select(s => RunMap(s.start, s.end, maps.SeedToSoil))
                    .SelectMany(x => x)
                    .ToList();
                var fertilizers = soils.Select(s => RunMap(s.start, s.end, maps.SoilToFertilizer))
                    .SelectMany(x => x)
                    .ToList();
                var waters = fertilizers.Select(s => RunMap(s.start, s.end, maps.FertilizerToWater))
                    .SelectMany(x => x)
                    .ToList();
                var lights = waters.Select(s => RunMap(s.start, s.end, maps.WaterToLight))
                    .SelectMany(x => x)
                    .ToList();
                var temps = lights.Select(s => RunMap(s.start, s.end, maps.LightToTemperature))
                    .SelectMany(x => x)
                    .ToList();
                var humidities = temps.Select(s => RunMap(s.start, s.end, maps.TemperatureToHumidity))
                    .SelectMany(x => x)
                    .ToList();
                var locations = humidities.Select(s => RunMap(s.start, s.end, maps.HumidityToLocation))
                    .SelectMany(x => x)
                    .ToList();

                min = Math.Min(min, locations.Min(x => x.start));
            }
            return min;
        }

        private IEnumerable<(long start, long end)> RunMap(long start, long end, List<Map> mapping)
        {
            var map = mapping.FirstOrDefault(x => x.RangeStart <= start && start <= x.RangeEnd);
            if (map is null)
            {
                yield return (start, end);
            }
            else
            {
                if (end <= map.RangeEnd)
                {
                    yield return (start - map.Diff, end - map.Diff);
                }
                else
                {
                    yield return (start - map.Diff, map.RangeEnd - map.Diff);
                    foreach (var element in RunMap(map.RangeEnd + 1, end, mapping.Except([map]).ToList()))
                    {
                        yield return element;
                    }
                }
            }
        }

        public List<Maps> SetupInputs(string[] inputs)
        {
            Func<string, Map> dictBuilder = x =>
            {
                var nums = x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                return new Map(nums[0], nums[1], nums[2]);
            };

            Func<string, List<Map>> mapBuilder = x =>
            {
                return inputs.SkipWhile(i => i.StartsWith(x) is false)
                    .Skip(1)
                    .TakeWhile(i => i != "")
                    .Select(dictBuilder)
                    .OrderBy(x => x.RangeStart)
                    .ToList();
            };

            var map = new Maps
            {
                Seeds = inputs[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList(),
                SeedToSoil = mapBuilder("seed-to-soil"),
                SoilToFertilizer = mapBuilder("soil-to-fertilizer"),
                FertilizerToWater = mapBuilder("fertilizer-to-water"),
                WaterToLight = mapBuilder("water-to-light"),
                LightToTemperature = mapBuilder("light-to-temperature"),
                TemperatureToHumidity = mapBuilder("temperature-to-humidity"),
                HumidityToLocation = mapBuilder("humidity-to-location"),
            };
            return [map];
        }
    }

    public class Maps
    {
        public List<long> Seeds { get; set; } = [];
        public List<Map> SeedToSoil { get; set; } = null!;
        public List<Map> SoilToFertilizer { get; set; } = null!;
        public List<Map> FertilizerToWater { get; set; } = null!;
        public List<Map> WaterToLight { get; set; } = null!;
        public List<Map> LightToTemperature { get; set; } = null!;
        public List<Map> TemperatureToHumidity { get; set; } = null!;
        public List<Map> HumidityToLocation { get; set; } = null!;

    }

    public record Map
    {
        public long RangeStart { get; init; }
        public long RangeEnd { get; init; }
        public long Diff { get; init; }

        public Map(long dest, long source, long range)
        {
            RangeStart = source;
            RangeEnd = source + range - 1;
            Diff = source - dest;
        }
    }
}
