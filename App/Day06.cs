namespace AdventOfCode2023.App
{
    internal static class Day06
    {
        public static void Run()
        {
            var input = File.ReadAllLines("Inputs/06.txt");
            var raceTimes = input[0].Split(' ')
                .Where(x => int.TryParse(x, out int _))
                .Select(int.Parse).ToArray();
            var raceRecords = input[1].Split(' ')
                .Where(x => int.TryParse(x, out int _))
                .Select(int.Parse).ToArray();

            int res1 = 1;
            for (int i = 0; i < raceTimes.Count(); i++)
            {
                var timeToBeat = raceRecords[i];
                int winningAttempts = 0;
                for (int j = 0; j < raceTimes[i]; j++)
                {
                    if (j * (raceTimes[i] - j) > timeToBeat) winningAttempts++;
                }
                res1 *= winningAttempts;
            }

            Console.WriteLine($"Part 1: {res1}");

            var newRaceTime = long.Parse(string.Join(string.Empty, raceTimes));
            var newRaceRecord = long.Parse(string.Join(string.Empty, raceRecords));

            long res2 = 0;
            for (long j = 0; j < newRaceTime; j++)
            {
                if (j * (newRaceTime - j) > newRaceRecord) res2++;
            }

            Console.WriteLine($"Part 2: {res2}");
            Console.ReadLine();
        }
    }
}
