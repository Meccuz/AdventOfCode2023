
namespace AdventOfCode2023.App
{
    internal static class Day09
    {
        public static void Run()
        {
            var input = File.ReadAllLines("Inputs/09.txt");

            var res1 = input.Sum(x => CalculateLastNumber(x));

            Console.WriteLine($"Part 1: {res1}");

            var res2 = input.Sum(x => CalculateFirstNumber(x));

            Console.WriteLine($"Part 2: {res2}");
            Console.ReadLine();
        }

        private static long CalculateLastNumber(string x)
        {
            List<List<long>> sequences = GetSequences(x);

            for (int i = sequences.Count - 2; i >= 0; i--)
            {
                sequences[i].Add(sequences[i].Last() + sequences[i + 1].Last());
            }

            return sequences.First().Last();
        }

        private static long CalculateFirstNumber(string x)
        {
            List<List<long>> sequences = GetSequences(x);

            for (int i = sequences.Count - 2; i >= 0; i--)
            {
                sequences[i].Add(sequences[i].First() - sequences[i + 1].Last());
            }

            return sequences.First().Last();
        }

        private static List<List<long>> GetSequences(string x)
        {
            var numbers = x.Split(' ').Select(long.Parse).ToList();
            var sequences = new List<List<long>> { numbers };
            var newSequence = new List<long>();
            while (!sequences.Last().All(x => x == 0))
            {
                for (int i = 0; i < sequences.Last().Count - 1; i++)
                {
                    newSequence.Add(sequences.Last()[i + 1] - sequences.Last()[i]);
                }
                sequences.Add(new List<long>(newSequence));
                newSequence.Clear();
            }

            return sequences;
        }
    }
}
