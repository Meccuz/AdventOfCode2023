



namespace AdventOfCode2023.App
{
    internal static class Day14
    {
        private static string[] input = File.ReadAllLines("Inputs/14.txt");
        private static HashSet<string> memo = new();

        public static void Run()
        {
            string[] rocksMovedNorth = MoveRocksNorth(input);
            int res1 = CalcLoad(rocksMovedNorth);
            Console.WriteLine($"Part 1: {res1}");

            string[] tilted = input;
            int cycles = 1000000000;
            for (int i = 0; i < cycles; i++)
            {
                tilted = MoveRocksNorth(tilted);
                tilted = MoveRocksWest(tilted);
                tilted = MoveRocksSouth(tilted);
                tilted = MoveRocksEast(tilted);

                var key = string.Join('-', tilted);
                if (memo.Contains(key))
                {
                    var index = memo.ToList().IndexOf(key);
                    var toRepeat = memo.Skip(index);
                    var remaining = (cycles - i) % toRepeat.Count();
                    tilted = toRepeat.ToArray()[remaining - 1].Split('-');
                    break;
                }

                memo.Add(key);
            }

            var res2 = CalcLoad(tilted);

            Console.WriteLine($"Part 2: {res2}");
            Console.ReadLine();
        }

        private static void PrintTerrain(string[] terrain)
        {
            for (int i = 0; i < terrain.Length; i++)
            {
                Console.WriteLine(terrain[i]);
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static int CalcLoad(string[] rocksMovedNorth)
        {
            var res = 0;
            for (int i = 0; i < rocksMovedNorth.Length; i++)
            {
                var points = rocksMovedNorth.Length - i;
                res += (rocksMovedNorth[i].Count(x => x == 'O') * points);
            }
            return res;
        }

        private static string[] MoveRocksNorth(string[] terrain)
        {
            var terrainWithMovedRocks = terrain.Select(x => x.ToCharArray()).ToArray();
            for (int i = 0; i < terrain[0].Length; i++)
            {
                var moveNorth = 0;
                for (int j = 0; j < terrain.Length; j++)
                {
                    var val = terrain[j][i];
                    switch (val)
                    {
                        case 'O':
                            if (moveNorth > 0)
                            {
                                terrainWithMovedRocks[j - moveNorth][i] = 'O';
                                terrainWithMovedRocks[j][i] = '.';
                            }
                            break;
                        case '#':
                            moveNorth = 0;
                            break;
                        case '.':
                            moveNorth++;
                            break;
                        default:
                            throw new Exception("Invalid character");
                    }
                }
            }

            return terrainWithMovedRocks.Select(x => new string(x)).ToArray();
        }

        private static string[] MoveRocksSouth(string[] terrain)
        {
            var terrainWithMovedRocks = terrain.Select(x => x.ToCharArray()).ToArray();
            for (int i = 0; i < terrain[0].Length; i++)
            {
                var moveSouth = 0;
                for (int j = terrain.Length - 1; j >= 0; j--)
                {
                    var val = terrain[j][i];
                    switch (val)
                    {
                        case 'O':
                            if (moveSouth > 0)
                            {
                                terrainWithMovedRocks[j + moveSouth][i] = 'O';
                                terrainWithMovedRocks[j][i] = '.';
                            }
                            break;
                        case '#':
                            moveSouth = 0;
                            break;
                        case '.':
                            moveSouth++;
                            break;
                        default:
                            throw new Exception("Invalid character");
                    }
                }
            }

            return terrainWithMovedRocks.Select(x => new string(x)).ToArray();
        }

        private static string[] MoveRocksEast(string[] terrain)
        {
            var terrainWithMovedRocks = terrain.Select(x => x.ToCharArray()).ToArray();
            for (int i = 0; i < terrain.Length; i++)
            {
                var moveEast = 0;
                for (int j = terrain[0].Length - 1; j >= 0; j--)
                {
                    var val = terrain[i][j];
                    switch (val)
                    {
                        case 'O':
                            if (moveEast > 0)
                            {
                                terrainWithMovedRocks[i][j + moveEast] = 'O';
                                terrainWithMovedRocks[i][j] = '.';
                            }
                            break;
                        case '#':
                            moveEast = 0;
                            break;
                        case '.':
                            moveEast++;
                            break;
                        default:
                            throw new Exception("Invalid character");
                    }
                }
            }

            return terrainWithMovedRocks.Select(x => new string(x)).ToArray();
        }

        private static string[] MoveRocksWest(string[] terrain)
        {
            var terrainWithMovedRocks = terrain.Select(x => x.ToCharArray()).ToArray();
            for (int i = 0; i < terrain.Length; i++)
            {
                var moveWest = 0;
                for (int j = 0; j < terrain[0].Length; j++)
                {
                    var val = terrain[i][j];
                    switch (val)
                    {
                        case 'O':
                            if (moveWest > 0)
                            {
                                terrainWithMovedRocks[i][j - moveWest] = 'O';
                                terrainWithMovedRocks[i][j] = '.';
                            }
                            break;
                        case '#':
                            moveWest = 0;
                            break;
                        case '.':
                            moveWest++;
                            break;
                        default:
                            throw new Exception("Invalid character");
                    }
                }
            }

            return terrainWithMovedRocks.Select(x => new string(x)).ToArray();
        }
    }
}
