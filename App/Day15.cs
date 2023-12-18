using System.Text.RegularExpressions;

namespace AdventOfCode2023.App
{
    internal static class Day15
    {
        private static string[] input = File.ReadAllLines("Inputs/15.txt");
        private static Dictionary<int, List<(string label, int lens)>> boxes = new();

        public static void Run()
        {
            var instructions = input[0].Split(',');
            var res1 = 0;
            foreach (var val in instructions) res1 += ApplyHASHAlgorithm(val);

            Console.WriteLine($"Part 1: {res1}");

            for (int i = 0; i < 256; i++)
            {
                boxes.Add(i, new List<(string label, int lens)>());
            }

            foreach (var val in instructions)
            {
                var regexRes = Regex.Match(val, @"(\w*)(.*)");
                var label = regexRes.Groups[1].Value;
                var box = ApplyHASHAlgorithm(label);
                var operation = regexRes.Groups[2].Value;
                if (operation[0] == '=') AddLensToBox(box, label, int.Parse(operation[1].ToString()));
                if (operation[0] == '-') RemoveLensFromBox(box, label);
                //PrintBoxes();
            }

            int res2 = CalculateFocusingPower();

            Console.WriteLine($"Part 2: {res2}");
            Console.ReadLine();
        }

        private static int CalculateFocusingPower()
        {
            var res = 0;
            foreach (var box in boxes.Where(x => x.Value.Any()))
            {
                var boxNumber = box.Key + 1;
                for (int i = 0; i < box.Value.Count; i++)
                {
                    res += boxNumber * (i + 1) * box.Value[i].lens;
                }
            }
            return res;
        }

        private static void PrintBoxes()
        {
            foreach (var box in boxes.Where(x => x.Value.Any()))
            {
                Console.Write($"Box {box.Key}:");
                foreach (var lens in box.Value)
                {
                    Console.Write($" [{lens.label} {lens.lens}]");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void RemoveLensFromBox(int box, string label)
        {
            var currentBox = boxes[box];
            var itemToRemove = currentBox.FirstOrDefault(x => x.label == label);
            currentBox.Remove(itemToRemove);
        }

        private static void AddLensToBox(int box, string label, int lens)
        {
            var currentBox = boxes[box];
            var lensWithLabel = currentBox.FirstOrDefault(x => x.label == label);
            if (lensWithLabel.lens == 0)
            {
                currentBox.Add((label, lens));
            }
            else
            {
                var currentBoxArr = currentBox.ToArray();
                currentBoxArr[currentBox.IndexOf(lensWithLabel)] = (label, lens);
                boxes[box] = currentBoxArr.ToList();
            }
        }

        private static int ApplyHASHAlgorithm(string val)
        {
            var res = 0;

            foreach (char c in val)
            {
                //Determine the ASCII code for the current character of the string.
                var asciiCode = (int)c;
                //Increase the current value by the ASCII code you just determined.
                res += asciiCode;
                //Set the current value to itself multiplied by 17.
                res *= 17;
                //Set the current value to the remainder of dividing itself by 256.
                res %= 256;
            }

            return res;
        }
    }
}
