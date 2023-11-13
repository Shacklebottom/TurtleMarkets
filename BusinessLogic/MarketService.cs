using BusinessLogic.Logging;
using MarketDomain;
using TurtleAPI.FinnhubIO;
using TurtleAPI.PolygonIO;
using TurtleSQL.Interfaces;
using TurtleSQL.TickerRepositories;

namespace BusinessLogic
{
    public class MarketService
    {
        private readonly IRepository<PreviousClose> _previousCloseRepo;
        private readonly IRepository<DividendDetails> _dividedDetailsRepo;
        private readonly IRepository<ListedStatus> _listedStatusRepo;
        private readonly IFinnhubAPI _finnhubAPI;
        private readonly ILogger _logger;

        public MarketService(
            IRepository<PreviousClose>? pcRepo = null,
            IRepository<DividendDetails>? ddRepo = null,
            IRepository<ListedStatus>? lsRepo = null,
            IFinnhubAPI? finnhubAPI = null,
            ILogger? logger = null)
        {
            _previousCloseRepo = pcRepo ?? new PreviousCloseRepository();
            _dividedDetailsRepo = ddRepo ?? new DividendDetailRepository();
            _listedStatusRepo = lsRepo ?? new ListedStatusRepository();
            _finnhubAPI = finnhubAPI ?? new FinnhubAPI();
            _logger = logger ?? new ConsoleLogger();
        }

        public void RecordPreviousClose()
        {
            try
            {
                log("Starting RecordPreviousClose()");

                var lsData = _listedStatusRepo.GetAll().ToList();
                log($"...working on {lsData.Count} records.");
                lsData.ForEach(x =>
                {
                    log($"...Querying {x.Ticker}");
                    _previousCloseRepo.Save(_finnhubAPI.GetPreviousClose(x.Ticker));
                });
                log("RecordPreviousClose() complete.");
            }
            catch( Exception ex )
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public void RecordDividendDetails()
        {


            log("Starting RecordDividendDetails");

            //var lsRepo = new ListedStatusRepository().GetAll();
            //DividendDetailRepository ddRepo = new();
            //int count = 0;
            //foreach (var item in lsRepo)
            //{
            //    Console.WriteLine($"Querying {item.Ticker}");
            //    IEnumerable<DividendDetails> ddInfo = PolygonAPI.GetDividendDetails(item.Ticker);
            //    foreach (var r in ddInfo)
            //    {
            //        ddRepo.Save(r);
            //    }
            //    count++;
            //    Console.WriteLine($"Transmission {count} Received");
            //}

            //log("RecordDividendDetails complete");
        }

        private void log(string message) { _logger.Log(message); }
    }
}