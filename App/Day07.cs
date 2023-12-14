namespace AdventOfCode2023.App
{
    internal static class Day07
    {
        public static void Run()
        {
            var input = File.ReadAllLines("Inputs/07.txt");
            var handsWithBidV1 = input.Select(x =>
            {
                var splitVal = x.Split(" ");
                return new HandWithBidV1
                {
                    Hand = splitVal[0],
                    Bid = long.Parse(splitVal[1])
                };
            }).ToArray();
            var handsWithBidV2 = input.Select(x =>
            {
                var splitVal = x.Split(" ");
                return new HandWithBidV2
                {
                    Hand = splitVal[0],
                    Bid = long.Parse(splitVal[1])
                };
            }).ToArray();

            Array.Sort(handsWithBidV1);
            long res1 = 0;
            for (int i = 0; i < handsWithBidV1.Length; i++)
            {
                res1 += handsWithBidV1[i].Bid * (i + 1);
            }

            Console.WriteLine($"Part 1: {res1}");

            Array.Sort(handsWithBidV2);
            long res2 = 0;
            for (int i = 0; i < handsWithBidV2.Length; i++)
            {
                res2 += handsWithBidV2[i].Bid * (i + 1);
            }

            Console.WriteLine($"Part 2: {res2}");
            Console.ReadLine();
        }

        public class HandWithBidV2 : IComparable<HandWithBidV2>
        {
            public string Hand { get; set; }
            public long Bid { get; set; }


            public int CompareTo(HandWithBidV2 that)
            {
                string cards = "AKQT98765432J";
                if (this.Score() < that.Score()) return -1;
                else if (this.Score() > that.Score()) return 1;
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (cards.IndexOf(this.Hand[i]) < cards.IndexOf(that.Hand[i])) return 1;
                        if (cards.IndexOf(this.Hand[i]) > cards.IndexOf(that.Hand[i])) return -1;
                    }
                }

                return 0;
            }

            public int Score()
            {
                var groupedCards = Hand.GroupBy(x => x).Select(x => (card: x.First(), count: x.Count())).ToList();
                var totJ = groupedCards.FirstOrDefault(x => x.card == 'J').count;
                groupedCards.RemoveAll(x => x.card == 'J');
                if (groupedCards.Count() == 0) return 7;
                var bestCard = groupedCards.First(x => x.count == groupedCards.Max(x => x.count));

                groupedCards.Remove(bestCard);
                bestCard.count += totJ;
                groupedCards.Add(bestCard);

                // Five of a kind (AAAAA)
                if (groupedCards.Count() == 1)
                    return 7;
                // Four of a kind (AAAA2)
                if (groupedCards.Count() == 2 && groupedCards.Max(x => x.count) == 4)
                    return 6;
                // Full house (23332)
                if (groupedCards.Count() == 2 && groupedCards.Max(x => x.count) == 3)
                    return 5;
                // Three of a kind (TTT98)
                if (groupedCards.Count() == 3 && groupedCards.Max(x => x.count) == 3)
                    return 4;
                // Two pair (23432)
                if (groupedCards.Count() == 3 && groupedCards.Max(x => x.count) == 2)
                    return 3;
                // One pair (A23A4)
                if (groupedCards.Count() == 4)
                    return 2;
                // High card (23456)
                return 1;
            }
        }

        public class HandWithBidV1 : IComparable<HandWithBidV1>
        {
            public string Hand { get; set; }
            public long Bid { get; set; }

            public int CompareTo(HandWithBidV1 that)
            {
                string cards = "AKQJT98765432";
                if (this.Score() < that.Score()) return -1;
                else if (this.Score() > that.Score()) return 1;
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (cards.IndexOf(this.Hand[i]) < cards.IndexOf(that.Hand[i])) return 1;
                        if (cards.IndexOf(this.Hand[i]) > cards.IndexOf(that.Hand[i])) return -1;
                    }
                }

                return 0;
            }

            public int Score()
            {
                var groupedCards = Hand.GroupBy(x => x).Select(x => x.Count());
                // Five of a kind (AAAAA)
                if (groupedCards.Count() == 1)
                    return 7;
                // Four of a kind (AAAA2)
                if (groupedCards.Count() == 2 && groupedCards.Max() == 4)
                    return 6;
                // Full house (23332)
                if (groupedCards.Count() == 2 && groupedCards.Max() == 3)
                    return 5;
                // Three of a kind (TTT98)
                if (groupedCards.Count() == 3 && groupedCards.Max() == 3)
                    return 4;
                // Two pair (23432)
                if (groupedCards.Count() == 3 && groupedCards.Max() == 2)
                    return 3;
                // One pair (A23A4)
                if (groupedCards.Count() == 4)
                    return 2;
                // High card (23456)
                return 1;
            }
        }
    }
}
