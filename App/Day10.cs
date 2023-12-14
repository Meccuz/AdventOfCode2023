namespace AdventOfCode2023.App
{
    internal static class Day10
    {
        private static string[] input = File.ReadAllLines("Inputs/10.txt");
        private static HashSet<(long, long)> pipeCoords = new HashSet<(long, long)>();
        private static HashSet<(long, long)> insideCoords = new HashSet<(long, long)>();

        public static void Run()
        {
            var startingLine = input.ToList().IndexOf(input.First(x => x.Contains('S')));
            var startingCol = input.First(x => x.Contains('S')).IndexOf('S');

            long totalLoopLength = CalculateLoopLength((startingLine, startingCol));

            Console.WriteLine($"Part 1: {totalLoopLength / 2}");

            var res2 = CalculateAreaInsideLoop();

            Console.WriteLine($"Part 2: {res2}");
            Console.ReadLine();
        }

        private static long CalculateAreaInsideLoop()
        {
            long res = 0;
            for (int i = 0; i < input.Count(); i++)
            {
                // HACK: replace S with the correct val
                var line = input[i].Replace('S', 'L');

                var isInside = false;
                char? lastLoopCorner = null;
                for (int j = 0; j < line.Length; j++)
                {
                    var val = line[j];

                    if (pipeCoords.Contains((i, j)))
                    {
                        if (val == '|')
                        {
                            isInside = !isInside;
                        }
                        else if ("FJL7".Contains(val))
                        {
                            if (!lastLoopCorner.HasValue)
                            {
                                lastLoopCorner = val;
                            }
                            else
                            {
                                switch (lastLoopCorner, val)
                                {
                                    case ('F', 'J'):
                                    case ('L', '7'):
                                        isInside = !isInside;
                                        lastLoopCorner = null;
                                        break;
                                    case ('F', '7'):
                                    case ('L', 'J'):
                                        lastLoopCorner = null;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                    else if (isInside)
                    {
                        res++;
                    }
                }
            }

            return res;
        }

        private static long CalculateLoopLength(
            (int line, int col) coords)
        {
            pipeCoords.Add((coords.line, coords.col));
            var cell = input[coords.line][coords.col];
            char? direction = null;
            // check all cells around to find starting pipe
            // check north cell
            if (coords.line - 1 > 0 && "7|F".Contains(input[coords.line - 1][coords.col]))
            {
                direction = 'N';
                coords.line--;
            }
            // check east cell
            else if ("7-J".Contains(input[coords.line][coords.col + 1]))
            {
                direction = 'E';
                coords.col++;
            }
            // check south cell
            else if ("J|L".Contains(input[coords.line + 1][coords.col]))
            {
                direction = 'S';
                coords.line++;
            }
            // check west cell
            else if ("L-F".Contains(input[coords.line][coords.col - 1]))
            {
                direction = 'W';
                coords.col--;
            }
            long length = 1;
            cell = input[coords.line][coords.col];
            pipeCoords.Add((coords.line, coords.col));
            // navigate pipe
            while (cell != 'S')
            {
                switch ((cell, direction))
                {
                    case ('|', 'N'):
                        direction = 'N';
                        coords.line--;
                        break;
                    case ('|', 'S'):
                        direction = 'S';
                        coords.line++;
                        break;
                    case ('-', 'E'):
                        direction = 'E';
                        coords.col++;
                        break;
                    case ('-', 'W'):
                        direction = 'W';
                        coords.col--;
                        break;
                    case ('7', 'E'):
                        direction = 'S';
                        coords.line++;
                        break;
                    case ('7', 'N'):
                        direction = 'W';
                        coords.col--;
                        break;
                    case ('F', 'W'):
                        direction = 'S';
                        coords.line++;
                        break;
                    case ('F', 'N'):
                        direction = 'E';
                        coords.col++;
                        break;
                    case ('J', 'E'):
                        direction = 'N';
                        coords.line--;
                        break;
                    case ('J', 'S'):
                        direction = 'W';
                        coords.col--;
                        break;
                    case ('L', 'W'):
                        direction = 'N';
                        coords.line--;
                        break;
                    case ('L', 'S'):
                        direction = 'E';
                        coords.col++;
                        break;
                    default:
                        throw new Exception("Invalid cell value or direction");
                }
                length++;
                cell = input[coords.line][coords.col];
                pipeCoords.Add((coords.line, coords.col));
            }
            return length;
        }
    }
}
