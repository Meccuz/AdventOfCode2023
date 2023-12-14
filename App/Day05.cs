



namespace AdventOfCode2023.App
{
    internal static class Day05
    {
        private static string[] seeds;
        private static string[] input;
        private static List<(long sourceMin, long destMin, long range)> map1 = new();
        private static List<(long sourceMin, long destMin, long range)> map2 = new();
        private static List<(long sourceMin, long destMin, long range)> map3 = new();
        private static List<(long sourceMin, long destMin, long range)> map4 = new();
        private static List<(long sourceMin, long destMin, long range)> map5 = new();
        private static List<(long sourceMin, long destMin, long range)> map6 = new();
        private static List<(long sourceMin, long destMin, long range)> map7 = new();

        public static void Run()
        {
            input = File.ReadAllLines("Inputs/05.txt");

            for (long i = 0; i < input.Length; i++)
            {
                string val = input[i];
                if (val.StartsWith("seeds:"))
                {
                    seeds = val.Replace("seeds: ", string.Empty).Split(' ');
                }

                if (val == "seed-to-soil map:")
                {
                    i = PopulateMap(map1, i + 1);
                }
                if (val == "soil-to-fertilizer map:")
                {
                    i = PopulateMap(map2, i + 1);
                }
                if (val == "fertilizer-to-water map:")
                {
                    i = PopulateMap(map3, i + 1);
                }
                if (val == "water-to-light map:")
                {
                    i = PopulateMap(map4, i + 1);
                }
                if (val == "light-to-temperature map:")
                {
                    i = PopulateMap(map5, i + 1);
                }
                if (val == "temperature-to-humidity map:")
                {
                    i = PopulateMap(map6, i + 1);
                }
                if (val == "humidity-to-location map:")
                {
                    i = PopulateMap(map7, i + 1);
                }
            }

            // PART 1
            Console.WriteLine($"Part 1: {ConvertSeedsAndGetMin()}");

            // PART 2
            Console.WriteLine($"Part 2: {ConvertSeedsAndGetMinV2()}");
            Console.ReadLine();
        }

        private static long PopulateMap(List<(long sourceMin, long destMin, long range)> map, long i)
        {
            while (i < input.Length && !string.IsNullOrEmpty(input[i]))
            {
                var values = input[i].Split(" ").Select(long.Parse).ToArray();
                map.Add((values[1], values[0], values[2]));
                i++;
            }

            return i;
        }

        private static long ConvertSeedsAndGetMin()
        {
            var res = new List<long>();
            foreach (var seed in seeds.Select(long.Parse))
            {
                var val = seed;
                val = MapVal(val, map1);
                val = MapVal(val, map2);
                val = MapVal(val, map3);
                val = MapVal(val, map4);
                val = MapVal(val, map5);
                val = MapVal(val, map6);
                val = MapVal(val, map7);
                res.Add(val);
            }
            return res.Min();
        }

        private static long MapVal(long val, List<(long sourceMin, long destMin, long range)> map)
        {
            try
            {
                var correctMap = map
                    .FirstOrDefault(x => val >= x.sourceMin && val <= x.sourceMin + x.range);
                return correctMap.destMin + (val - correctMap.sourceMin);
            }
            catch { return val; }
        }

        private static long ConvertSeedsAndGetMinV2()
        {
            var ranges = new List<(long start, long range)>();
            for (int i = 0; i < seeds.Length; i += 2)
            {
                ranges.Add((long.Parse(seeds[i]), long.Parse(seeds[i + 1])));
            }

            ranges = MapRanges(ranges, map1);
            ranges = MapRanges(ranges, map2);
            ranges = MapRanges(ranges, map3);
            ranges = MapRanges(ranges, map4);
            ranges = MapRanges(ranges, map5);
            ranges = MapRanges(ranges, map6);
            ranges = MapRanges(ranges, map7);

            return ranges.Select(x => x.start).Min();
        }

        private static List<(long start, long range)> MapRanges(
            List<(long start, long range)> ranges,
            List<(long sourceMin, long destMin, long range)> maps)
        {
            var res = new List<(long start, long range)>();
            var newMaps = maps.Select(x => (min: x.sourceMin, max: x.sourceMin + x.range - 1, toAdd: x.destMin - x.sourceMin));
            var newRanges = ranges.Select(x => (min: x.start, max: x.start + x.range));
            foreach (var range in newRanges)
            {
                var validMaps = newMaps.Where(map => map.min < range.max && map.max > range.min)
                    .OrderBy(map => map.min)
                    .ToList();
                if (!validMaps.Any())
                {
                    res.Add((range.min, range.max - range.min));
                    continue;
                }

                var minMap = newMaps.Select(map => map.min).Min();
                var maxMap = newMaps.Select(map => map.max).Max();
                // add range values outside all map
                // add values lower than the map
                if (range.min < minMap)
                    res.Add((range.min, minMap - range.min));
                // add values higher than the map
                if (range.max > maxMap)
                    res.Add((maxMap, range.max - maxMap));

                foreach (var map in validMaps)
                {
                    // add values that can be mapped
                    if (range.min >= map.min && range.max <= map.max)
                        res.Add((range.min + map.toAdd, range.max - range.min));
                    else if (range.min >= map.min && range.max > map.max)
                        res.Add((range.min + map.toAdd, map.max - range.min));
                    else if (range.min < map.min && range.max <= map.max)
                        res.Add((map.min + map.toAdd, range.max - map.min));
                    else if (range.min < map.min && range.max > map.max)
                        res.Add((map.min + map.toAdd, map.max - map.min));
                }
            }

            return res;
        }
    }
}
