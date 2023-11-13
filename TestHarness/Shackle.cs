using MarketDomain;
using MarketDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using TurtleAPI.FinnhubIO;
using TurtleAPI.PolygonIO;
using TurtleSQL.Interfaces;
using TurtleSQL.MarketStatusForecast;
using TurtleSQL.TickerRepositories;

namespace TestHarness
{
    public static class Shackle
    {
        public static void RecordPreviousClose()
        {
            VaporTransmission();
            var lsRepo = new ListedStatusRepository().GetAll();
            PreviousCloseRepository pcRepo = new();
            var fhAPI = new FinnhubAPI();
            int count = 0;
            foreach (var item in lsRepo)
            {
                Console.WriteLine($"Querying {item.Ticker}");
                PreviousClose pcInfo = fhAPI.GetPreviousClose(item.Ticker);
                pcRepo.Save(pcInfo);
                count++;
                Console.WriteLine($"Transmission {count} Received");
            }
            Console.WriteLine("Vapor Released");
        }

        public static void RecordDividendDetails()
        {
            VaporTransmission();
            var lsRepo = new ListedStatusRepository().GetAll();
            DividendDetailRepository ddRepo = new();
            var pgAPI = new PolygonAPI();
            int count = 0;
            foreach (var item in lsRepo)
            {
                Console.WriteLine($"Querying {item.Ticker}");
                IEnumerable<DividendDetails> ddInfo = pgAPI.GetDividendDetails(item.Ticker);
                foreach (var r in ddInfo)
                {
                    ddRepo.Save(r);
                }
                count++;
                Console.WriteLine($"Transmission {count} Received");
            }
            Console.WriteLine("Vapor Released");
        }
        private static void VaporTransmission()
        {
            Console.WriteLine("Transmission sequence activated");
            Console.WriteLine("Vapor Engaged");
            Console.WriteLine("Awaiting transmission approval");
            Thread.Sleep(1000);
            Console.WriteLine("Approval Received");
            Console.WriteLine("[...]");
            Console.WriteLine("[...]");
            Console.WriteLine("Engaging Vapor");
        }

    }
    public class Dumbell
    {

    }
}
