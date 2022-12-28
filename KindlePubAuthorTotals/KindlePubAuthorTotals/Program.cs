using System.Transactions;

namespace KindlePubAuthorTotals
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var csvFiles = File.ReadAllLines(args[0]);
            DateTime firstDate = DateTime.Parse(args[1]);
            // ABCDEFGHIJLKLMNOPQRSTUVWXYZ
            // 012345678901234567890123456789
            Dictionary<string, double> royaltyPerAuthor= new Dictionary<string, double>();
            Dictionary<string, double> royaltyPerAuthorSum = new Dictionary<string, double>();
            foreach (var line in csvFiles.Skip(1))
            {
                string[] fields = line.Split(',');
                DateTime royaltyDate = DateTime.Parse(fields[0]);
                if (royaltyDate < firstDate)
                    continue;
                string author = fields[3];
                double royalty = double.Parse(fields[14]);

                if (!royaltyPerAuthor.ContainsKey(author))
                    royaltyPerAuthor.Add(author, 0);
                royaltyPerAuthor[author] += royalty;
            }
            foreach (var line in csvFiles.Skip(1))
            {
                string[] fields = line.Split(',');
                DateTime royaltyDate = DateTime.Parse(fields[0]);
                if (royaltyDate < firstDate)
                    continue;
                string author = fields[3];
                double royalty = double.Parse(fields[14]);
                if (!royaltyPerAuthorSum.ContainsKey(author))
                    royaltyPerAuthorSum.Add(author, 0);
                royaltyPerAuthorSum[author] += royalty;
                Console.WriteLine($"{line},{royaltyPerAuthorSum[author]*.4:F2},{royaltyPerAuthor[author]*.4:F2}");
            }
            double totalRoyalties = royaltyPerAuthor.Values.Sum(v => v);
            double payoutPct = totalRoyalties * .4;
            foreach (var author in royaltyPerAuthor.Keys.OrderBy(k => k))
            {
                Console.WriteLine($"{author} {royaltyPerAuthor[author]*.4:F2}");
            }
            foreach (var author in royaltyPerAuthor.OrderBy(k => k.Value))
            {
                Console.WriteLine($"{author.Key} {royaltyPerAuthor[author.Key] * .4:F2}");
            }
        }
    }
}