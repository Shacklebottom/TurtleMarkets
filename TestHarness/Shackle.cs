using MarketDomain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleAPI.FinnhubIO;
using TurtleAPI.PolygonIO;
using TurtleSQL.TickerRepositories;

namespace TestHarness
{
    public static class Shackle
    {
        public static void RecordPreviousClose()
        {
            VaporTransmission();
            ListedStatusRepository lsRepo = new();
            IEnumerable<ListedStatus> lsData = lsRepo.GetAll();
            PreviousCloseRepository pcRepo = new();
            int count = 0;
            Stopwatch sw = new();
            foreach (var item in lsData)
            {
                if (sw.Elapsed < TimeSpan.FromMinutes(1))
                {
                    Console.WriteLine($"Querying {item.Ticker}");
                    PreviousClose pcInfo = FinnhubAPI.GetPreviousClose(item.Ticker);
                    pcRepo.Save(pcInfo);
                    count++;
                    Console.WriteLine($"Transmission {count} Received");
                    Thread.Sleep(850);
                }
                sw.Restart();
            }
            Console.WriteLine("Vapor Released");
        }
        public static void RecordDividendDetails()
        {
            VaporTransmission();
            ListedStatusRepository lsRepo = new();
            IEnumerable<ListedStatus> lsData = lsRepo.GetAll();
            DividendDetailRepository ddRepo = new();
            int entryCount = 0;
            int APIcallCount = 0;
            Stopwatch sw = new();
            foreach (var item in lsData)
            {
                if(sw.Elapsed < TimeSpan.FromMinutes(1) && APIcallCount < 5)
                {
                    Console.WriteLine($"Querying {item.Ticker}");
                    IEnumerable<DividendDetails> ddInfo = PolygonAPI.GetDividendDetails(item.Ticker);
                    foreach (var r in ddInfo)
                    {
                        ddRepo.Save(r);
                    }
                    entryCount++;
                    APIcallCount++;
                    Console.WriteLine($"Transmission {entryCount} Received");
                    Thread.Sleep(12000);
                }
                APIcallCount = 0;
                sw.Restart();
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
}
