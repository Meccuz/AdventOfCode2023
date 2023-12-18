




namespace AdventOfCode2023.App
{
    internal static class Day16
    {
        private static string[] input = File.ReadAllLines("Inputs/16.txt");
        private static List<(int row, int col, char direction)> energizedTiles = new();

        public static void Run()
        {
            EnergizeTile(0, 0, 'R');

            Console.WriteLine($"Part 1: {new HashSet<(int, int)>(energizedTiles.Select(x => (x.row, x.col))).Count()}");
            int maxEnergizedTiles = EnergizeTilesFromAllDirections();

            Console.WriteLine($"Part 2: {maxEnergizedTiles}");
            Console.ReadLine();
        }

        private static int EnergizeTilesFromAllDirections()
        {
            var maxEnergizedTiles = 0;
            for (int i = 0; i < input.Length; i++)
            {
                // LEFT-RIGHT
                energizedTiles = new();
                EnergizeTile(i, 0, 'R');
                var newCount = new HashSet<(int, int)>(energizedTiles.Select(x => (x.row, x.col))).Count();
                if (newCount > maxEnergizedTiles) maxEnergizedTiles = newCount;

                // RIGHT-LEFT
                energizedTiles = new();
                EnergizeTile(i, input.Length - 1, 'L');
                newCount = new HashSet<(int, int)>(energizedTiles.Select(x => (x.row, x.col))).Count();
                if (newCount > maxEnergizedTiles) maxEnergizedTiles = newCount;
            }
            for (int i = 0; i < input[0].Length; i++)
            {
                // TOP-BOTTOM
                energizedTiles = new();
                EnergizeTile(0, i, 'D');
                var newCount = new HashSet<(int, int)>(energizedTiles.Select(x => (x.row, x.col))).Count();
                if (newCount > maxEnergizedTiles) maxEnergizedTiles = newCount;

                // BOTTOM-TOP
                energizedTiles = new();
                EnergizeTile(input[0].Length - 1, i, 'U');
                newCount = new HashSet<(int, int)>(energizedTiles.Select(x => (x.row, x.col))).Count();
                if (newCount > maxEnergizedTiles) maxEnergizedTiles = newCount;
            }

            return maxEnergizedTiles;
        }

        private static void EnergizeTile(int row, int col, char direction)
        {
            if (energizedTiles.Contains((row, col, direction))) return;
            char cell = '.';
            try
            {
                cell = input[row][col];
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
            energizedTiles.Add((row, col, direction));
            switch (direction)
            {
                case 'R':
                    switch (cell)
                    {
                        case '.':
                        case '-':
                            EnergizeTile(row, col + 1, direction);
                            break;
                        case '\\':
                            EnergizeTile(row + 1, col, 'D');
                            break;
                        case '/':
                            EnergizeTile(row - 1, col, 'U');
                            break;
                        case '|':
                            EnergizeTile(row + 1, col, 'D');
                            EnergizeTile(row - 1, col, 'U');
                            break;
                    }
                    break;
                case 'U':
                    switch (cell)
                    {
                        case '.':
                        case '|':
                            EnergizeTile(row - 1, col, direction);
                            break;
                        case '\\':
                            EnergizeTile(row, col - 1, 'L');
                            break;
                        case '/':
                            EnergizeTile(row, col + 1, 'R');
                            break;
                        case '-':
                            EnergizeTile(row, col - 1, 'L');
                            EnergizeTile(row, col + 1, 'R');
                            break;
                    }
                    break;
                case 'L':
                    switch (cell)
                    {
                        case '.':
                        case '-':
                            EnergizeTile(row, col - 1, direction);
                            break;
                        case '\\':
                            EnergizeTile(row - 1, col, 'U');
                            break;
                        case '/':
                            EnergizeTile(row + 1, col, 'D');
                            break;
                        case '|':
                            EnergizeTile(row + 1, col, 'D');
                            EnergizeTile(row - 1, col, 'U');
                            break;
                    }
                    break;
                case 'D':
                    switch (cell)
                    {
                        case '.':
                        case '|':
                            EnergizeTile(row + 1, col, direction);
                            break;
                        case '\\':
                            EnergizeTile(row, col + 1, 'R');
                            break;
                        case '/':
                            EnergizeTile(row, col - 1, 'L');
                            break;
                        case '-':
                            EnergizeTile(row, col + 1, 'R');
                            EnergizeTile(row, col - 1, 'L');
                            break;
                    }
                    break;
            }
        }
    }
}
