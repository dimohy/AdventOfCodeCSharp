using System.Linq;

namespace _2023;

public class Day04 : ISolve
{
    public static string Solve1(string input, params object[] args)
    {
        var lines = input.Split(Environment.NewLine);
        var sum = 0;
        foreach (var line in lines)
        {
            var card = Card.Parse(line);
            sum += card.GetScore();
        }
        
        return sum.ToString();
    }

    public static string Solve2(string input, params object[] args)
    {
        var lines = input.Split(Environment.NewLine);
        var cards = lines.Select(Card.Parse).ToArray();

        var count = cards.Select((x, i) => (x, i)) .Sum(x => GetCardCount(cards, x.i));
        return count.ToString();

        static int GetCardCount(Card[] cards, int index)
        {
            var card = cards[index];
            var count = card.GetMatchCount();
            if (count is 0)
                return 1;

            var sum = 1;
            for (var i = index + 1; i < index + 1 + count; i++)
            {
                var result = GetCardCount(cards, i);
                sum += result;
            }

            return sum;
        }
    }

    class Card(string name, int count, int score)
    {
        public string Name => name;

        public static Card Parse(string line)
        {
            var parts = line.Split(":");
            var name = parts[0].Trim();
            var numbers = parts[1].Split("|");
            var numbers1 = numbers[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var numbers2 = numbers[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var count = numbers2.Count(x => numbers1.Contains(x));
            var score = (int)Math.Pow(2, count - 1);

            return new Card(name, count, score);
        }

        public int GetScore() => score;
        public int GetMatchCount() => count;
    }
}