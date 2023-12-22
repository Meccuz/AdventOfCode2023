





namespace AdventOfCode2023.App
{
    internal static class Day17
    {
        private static string[] input = File.ReadAllLines("Inputs/17.txt");
        private static int minHeatLost = int.MaxValue;

        public static void Run()
        {
            //MoveCrucible(0, 1, 'R');
            //MoveCrucible(1, 0, 'D');

            //Console.WriteLine($"Part 1: {minHeatLost}");

            minHeatLost = int.MaxValue;
            MoveUltraCrucible(0, 1, 'R');
            MoveUltraCrucible(1, 0, 'D');

            Console.WriteLine($"Part 2: {minHeatLost}");
            Console.ReadLine();
        }

        private static void MoveUltraCrucible(int startRow, int startCol, char startDirection)
        {
            Stack<State> stack = new Stack<State>();
            Dictionary<string, int> usedPaths = new Dictionary<string, int>();

            stack.Push(new State(startRow, startCol, 1, 0, startDirection)); // Push initial state

            while (stack.Count > 0)
            {
                State currentState = stack.Pop();

                int row = currentState.Row;
                int col = currentState.Col;
                int movesInDirection = currentState.MovesInDirection;
                int tot = currentState.Total;
                char direction = currentState.Direction;

                var val = int.Parse(input[row][col].ToString());
                tot += val;

                var pathKey = $"{row}-{col}-{movesInDirection}-{direction}";
                if ((usedPaths.ContainsKey(pathKey) && usedPaths[pathKey] <= tot) || tot >= minHeatLost)
                    continue;

                usedPaths[pathKey] = tot;

                if (row == input.Length - 1 && col == input[0].Length - 1)
                {
                    Console.WriteLine(tot);
                    minHeatLost = tot < minHeatLost ? tot : minHeatLost;
                    continue;
                }

                switch (direction)
                {
                    case 'R':
                        if (movesInDirection < 10 && col < input[0].Length - 1)
                            stack.Push(new State(row, col + 1, movesInDirection + 1, tot, direction));
                        if (movesInDirection >= 4 && row > 0)
                            stack.Push(new State(row - 1, col, 1, tot, 'T'));
                        if (movesInDirection >= 4 && row < input.Length - 1)
                            stack.Push(new State(row + 1, col, 1, tot, 'D'));
                        break;
                    case 'L':
                        if (movesInDirection < 10 && col > 0)
                            stack.Push(new State(row, col - 1, movesInDirection + 1, tot, direction));
                        if (movesInDirection >= 4 && row > 0)
                            stack.Push(new State(row - 1, col, 1, tot, 'T'));
                        if (movesInDirection >= 4 && row < input.Length - 1)
                            stack.Push(new State(row + 1, col, 1, tot, 'D'));
                        break;
                    case 'T':
                        if (movesInDirection < 10 && row > 0)
                            stack.Push(new State(row - 1, col, movesInDirection + 1, tot, direction));
                        if (movesInDirection >= 4 && col > 0)
                            stack.Push(new State(row, col - 1, 1, tot, 'L'));
                        if (movesInDirection >= 4 && col < input[0].Length - 1)
                            stack.Push(new State(row, col + 1, 1, tot, 'R'));
                        break;
                    case 'D':
                        if (movesInDirection < 10 && row < input.Length - 1)
                            stack.Push(new State(row + 1, col, movesInDirection + 1, tot, direction));
                        if (movesInDirection >= 4 && col > 0)
                            stack.Push(new State(row, col - 1, 1, tot, 'L'));
                        if (movesInDirection >= 4 && col < input[0].Length - 1)
                            stack.Push(new State(row, col + 1, 1, tot, 'R'));
                        break;
                }
            }
        }

        private static void MoveCrucible(int startRow, int startCol, char startDirection)
        {
            Stack<State> stack = new Stack<State>();
            Dictionary<string, int> usedPaths = new Dictionary<string, int>();

            stack.Push(new State(startRow, startCol, 1, 0, startDirection)); // Push initial state

            while (stack.Count > 0)
            {
                State currentState = stack.Pop();

                int row = currentState.Row;
                int col = currentState.Col;
                int movesInDirection = currentState.MovesInDirection;
                int tot = currentState.Total;
                char direction = currentState.Direction;

                var val = int.Parse(input[row][col].ToString());
                tot += val;

                var pathKey = $"{row}-{col}-{movesInDirection}-{direction}";
                if ((usedPaths.ContainsKey(pathKey) && usedPaths[pathKey] <= tot) || tot > minHeatLost)
                    continue;

                usedPaths[pathKey] = tot;

                if (row == input.Length - 1 && col == input[0].Length - 1)
                {
                    Console.WriteLine(tot);
                    minHeatLost = tot < minHeatLost ? tot : minHeatLost;
                    continue;
                }

                switch (direction)
                {
                    case 'R':
                        if (movesInDirection < 3 && col < input[0].Length - 1)
                            stack.Push(new State(row, col + 1, movesInDirection + 1, tot, direction));
                        if (row > 0)
                            stack.Push(new State(row - 1, col, 1, tot, 'T'));
                        if (row < input.Length - 1)
                            stack.Push(new State(row + 1, col, 1, tot, 'D'));
                        break;
                    case 'L':
                        if (movesInDirection < 3 && col > 0)
                            stack.Push(new State(row, col - 1, movesInDirection + 1, tot, direction));
                        if (row > 0)
                            stack.Push(new State(row - 1, col, 1, tot, 'T'));
                        if (row < input.Length - 1)
                            stack.Push(new State(row + 1, col, 1, tot, 'D'));
                        break;
                    case 'T':
                        if (movesInDirection < 3 && row > 0)
                            stack.Push(new State(row - 1, col, movesInDirection + 1, tot, direction));
                        if (col > 0)
                            stack.Push(new State(row, col - 1, 1, tot, 'L'));
                        if (col < input[0].Length - 1)
                            stack.Push(new State(row, col + 1, 1, tot, 'R'));
                        break;
                    case 'D':
                        if (movesInDirection < 3 && row < input.Length - 1)
                            stack.Push(new State(row + 1, col, movesInDirection + 1, tot, direction));
                        if (col > 0)
                            stack.Push(new State(row, col - 1, 1, tot, 'L'));
                        if (col < input[0].Length - 1)
                            stack.Push(new State(row, col + 1, 1, tot, 'R'));
                        break;
                }
            }
        }

        private class State
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public int MovesInDirection { get; set; }
            public int Total { get; set; }
            public char Direction { get; set; }

            public State(int row, int col, int movesInDirection, int total, char direction)
            {
                Row = row;
                Col = col;
                MovesInDirection = movesInDirection;
                Total = total;
                Direction = direction;
            }
        }
    }
}
