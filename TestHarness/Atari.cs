using CsvHelper;
using MarketDomain.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleAPI.AlphaVantage;

namespace TestHarness
{
    public static class Atari
    {
        public static void ParseCSV()
        {
            var csvData = File.ReadAllText(@"c:\code\c#\turtlemarkets\sampledata\alphavantage\getlistedstatus.csv");
            var data = ParseListedStatus(csvData).ToList();

            data.ForEach(Console.WriteLine);
        }

        private static IEnumerable<ListedStatus> ParseListedStatus(string csvData)
        {
            var result = new List<ListedStatus>();

            var lines = csvData.Split("\r\n");
            //var header = lines[0];
            var data = lines.Skip(1);

            foreach (var line in data)
            {
                if(string.IsNullOrEmpty(line)) continue;

                var elements = line.Split(',');

                var ls = new ListedStatus
                {
                    Ticker = elements[0],
                    Name = elements[1],
                    Exchange = elements[2],
                    Type = elements[3],
                    IPOdate = elements[4] == "null" ? null : DateTime.Parse(elements[4]),
                    DelistingDate = elements[5] == "null" ? null : DateTime.Parse(elements[5]),
                    Status = elements[6]
                };

                result.Add(ls);
            }
            return result;
        }
    }
}
