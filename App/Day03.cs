namespace AdventOfCode2023.App
{
    internal static class Day03
    {
        public static void Run()
        {
            var input = File.ReadAllLines("Inputs/03.txt");

            var currentNum = string.Empty;
            var res1 = 0;
            var res2 = 0;

            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    var val = input[y][x].ToString();
                    if (int.TryParse(val, out int num))
                    {
                        currentNum += num;
                        if (x + 1 < input[y].Length)
                        {
                            continue;
                        }
                    }
                    if (!string.IsNullOrEmpty(currentNum))
                    {
                        if (CheckSymbolsAroundNumber(
                            input,
                            y,
                            x - currentNum.Length - 1,
                            x - 1))
                        {
                            res1 += int.Parse(currentNum);
                        }

                        currentNum = string.Empty;
                    }

                    // PART 2
                    if (val == "*")
                    {
                        res2 += CheckNumbersAroundGear(input, y, x);
                    }
                }
            }

            Console.WriteLine($"Part 1: {res1}");
            Console.WriteLine($"Part 2: {res2}");
            Console.ReadLine();
        }

        private static bool CheckSymbolsAroundNumber(string[] input, int y, int xStart, int xEnd)
        {
            for (int y1 = y - 1; y1 <= y + 1; y1++)
            {
                for (int x1 = xStart; x1 <= xEnd + 1; x1++)
                {
                    try
                    {
                        var val = input[y1][x1].ToString();
                        if (!int.TryParse(val, out int _) && val != ".")
                        {
                            return true;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return false;
        }

        private static int CheckNumbersAroundGear(string[] input, int y, int x)
        {
            var validNumbers = new List<int>();
            int x1 = x;
            string currentNum = string.Empty;
            // 1. Check number on the left
            try
            {
                while (int.TryParse(input[y][--x1].ToString(), out int num)) { currentNum = currentNum.Insert(0, num.ToString()); }
            }
            catch (Exception) { }
            if (int.TryParse(currentNum, out int n))
            {
                validNumbers.Add(n);
            }
            // 2. Check number on the right
            x1 = x;
            currentNum = string.Empty;
            try
            {
                while (int.TryParse(input[y][++x1].ToString(), out int num)) { currentNum += num; }
            }
            catch (Exception) { }
            if (int.TryParse(currentNum, out n))
            {
                validNumbers.Add(n);
            }
            // 3. Check number on top
            x1 = x;
            currentNum = string.Empty;
            try
            {
                if (int.TryParse(input[y - 1][x1].ToString(), out n))
                {
                    currentNum += n;
                    try
                    {
                        while (int.TryParse(input[y - 1][++x1].ToString(), out int num)) { currentNum += num; }
                    }
                    catch (Exception)
                    {
                    }
                    x1 = x;
                    try
                    {
                        while (int.TryParse(input[y - 1][--x1].ToString(), out int num)) { currentNum = currentNum.Insert(0, num.ToString()); }
                    }
                    catch (Exception)
                    {
                    }
                    validNumbers.Add(int.Parse(currentNum));
                }
                // 3.1. If no number on top check number on top-left and top-right
                else
                {
                    try
                    {
                        while (int.TryParse(input[y - 1][--x1].ToString(), out int num)) { currentNum = currentNum.Insert(0, num.ToString()); }
                    }
                    catch (Exception)
                    {
                    }
                    if (int.TryParse(currentNum, out n))
                    {
                        validNumbers.Add(n);
                    }
                    x1 = x;
                    currentNum = string.Empty;
                    try
                    {
                        while (int.TryParse(input[y - 1][++x1].ToString(), out int num)) { currentNum += num; }
                    }
                    catch (Exception)
                    {
                    }
                    if (int.TryParse(currentNum, out n))
                    {
                        validNumbers.Add(n);
                    }
                }
            }
            catch (Exception) { }
            // 4. Check number on bottom
            x1 = x;
            currentNum = string.Empty;
            try
            {
                if (int.TryParse(input[y + 1][x1].ToString(), out n))
                {
                    currentNum += n;
                    while (int.TryParse(input[y + 1][++x1].ToString(), out int num)) { currentNum += num; }
                    x1 = x;
                    while (int.TryParse(input[y + 1][--x1].ToString(), out int num)) { currentNum = currentNum.Insert(0, num.ToString()); }
                    validNumbers.Add(int.Parse(currentNum));
                }
                // 4.1. If no number on bottom check number on bottom-left and bottom-right
                else
                {
                    try
                    {
                        while (int.TryParse(input[y + 1][--x1].ToString(), out int num)) { currentNum = currentNum.Insert(0, num.ToString()); }
                    }
                    catch (Exception)
                    {
                    }
                    if (int.TryParse(currentNum, out n))
                    {
                        validNumbers.Add(n);
                    }
                    x1 = x;
                    currentNum = string.Empty;
                    try
                    {
                        while (int.TryParse(input[y + 1][++x1].ToString(), out int num)) { currentNum += num; }
                    }
                    catch (Exception)
                    {
                    }
                    if (int.TryParse(currentNum, out n))
                    {
                        validNumbers.Add(n);
                    }
                }
            }
            catch (Exception) { }

            if (validNumbers.Count == 2) return validNumbers[0] * validNumbers[1];
            return 0;
        }
    }
}
