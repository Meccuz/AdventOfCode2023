using System.Text.RegularExpressions;

namespace AdventOfCode2023.App
{
    internal static class Day08
    {
        public static void Run()
        {
            var input = File.ReadAllLines("Inputs/08.txt");

            string directions = input[0];
            var nodes = new HashSet<Node>();
            var nodeRegex = new Regex(@"(\w+) = \((\w+), (\w+)\)");

            for (int i = 2; i < input.Length; i++)
            {
                var match = nodeRegex.Match(input[i]);
                nodes.Add(new Node { Value = match.Groups[1].Value });
            }
            for (int i = 2; i < input.Length; i++)
            {
                var match = nodeRegex.Match(input[i]);
                var node = nodes.First(x => x.Value == match.Groups[1].Value);
                node.Left = nodes.First(x => x.Value == match.Groups[2].Value);
                node.Right = nodes.First(x => x.Value == match.Groups[3].Value);
            }

            var currentNode = nodes.First(x => x.Value == "AAA");
            long res1 = 0;
            for (long i = 0; true; i++)
            {
                var direction = directions[(int)(i % directions.Length)];
                currentNode = currentNode.Move(direction);

                if (currentNode.Value == "ZZZ")
                {
                    res1 = i + 1;
                    break;
                }
            }

            Console.WriteLine($"Part 1: {res1}");

            var currentNodes = nodes.Where(x => x.Value.Last() == 'A').ToList();
            long res2 = 0;
            var ghostTimes = new List<long>();
            foreach (var node in currentNodes)
            {
                var n = node;
                for (long i = 0; true; i++)
                {
                    var direction = directions[(int)(i % directions.Length)];
                    n = n.Move(direction);

                    if (n.Value.Last() == 'Z')
                    {
                        ghostTimes.Add(i + 1);
                        break;
                    }
                }
            }

            res2 = LCM(ghostTimes);

            Console.WriteLine($"Part 2: {res2}");
            Console.ReadLine();
        }

        static long LCM(List<long> numbers)
        {
            return numbers.Aggregate((a, b) => Math.Abs(a * b) / GCD(a, b));
        }

        static long GCD(long a, long b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        class Node
        {
            public string Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node Move(char direction)
            {
                return direction == 'L' ? Left : Right;
            }

            public override string ToString()
            {
                return $"{Value} {Left.Value} {Right.Value}";
            }
        }
    }
}
