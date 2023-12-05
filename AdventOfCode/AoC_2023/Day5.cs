using System.Collections.Frozen;
using AdventOfCodeCore;

namespace AoC_2023
{
    public class Day5 : IDay<Maps>
    {
        public long A(List<Maps> inputs)
        {
            var map = inputs.Single();

            return map.Seeds.Min(s =>
                map.HumidityToLocation[
                    map.TemperatureToHumidity[
                        map.LightToTemperature[
                            map.WaterToLight[
                                map.FertilizerToWater[
                                    map.SoilToFertilizer[
                                        map.SeedToSoil[s]
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
            );
        }

        public long B(List<Maps> inputs)
        {
            throw new NotImplementedException();
        }

        public List<Maps> SetupInputs(string[] inputs)
        {
            Func<string, Dictionary<long, long>> dictBuilder = x =>
            {
                var nums = x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                var (dest, source, length) = (nums[0], nums[1], nums[2]);
                Dictionary<long, long> dict = [];
                for (long i = 0; i < length; i++)
                {
                    dict.Add(source + i, dest + i);
                }
                return dict;
            };

            Func<string, FrozenDictionary<long, long>> mapBuilder = x =>
            {
                return inputs.SkipWhile(i => i.StartsWith(x) is false)
                    .Skip(1)
                    .TakeWhile(i => i != "")
                    .SelectMany(dictBuilder)
                    .ToFrozenDictionary(d => d.Key, d => d.Value);
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
        public FrozenDictionary<long, long> SeedToSoil { get; set; } = null!;
        public FrozenDictionary<long, long> SoilToFertilizer { get; set; } = null!;
        public FrozenDictionary<long, long> FertilizerToWater { get; set; } = null!;
        public FrozenDictionary<long, long> WaterToLight { get; set; } = null!;
        public FrozenDictionary<long, long> LightToTemperature { get; set; } = null!;
        public FrozenDictionary<long, long> TemperatureToHumidity { get; set; } = null!;
        public FrozenDictionary<long, long> HumidityToLocation { get; set; } = null!;

    }
}
