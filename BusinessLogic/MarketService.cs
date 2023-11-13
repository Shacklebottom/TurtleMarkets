using BusinessLogic.Logging;
using MarketDomain;
using TurtleAPI.FinnhubIO;
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
                _logger.Log("Starting RecordPreviousClose()");

                var lsData = _listedStatusRepo.GetAll().ToList();
                _logger.Log($"...working on {lsData.Count} records.");
                lsData.ForEach(x =>
                {
                    _logger.Log($"...Querying {x.Ticker}");
                    _previousCloseRepo.Save(_finnhubAPI.GetPreviousClose(x.Ticker));
                });
                _logger.Log("RecordPreviousClose() complete.");
            }
            catch( Exception ex )
            {
                _logger.Log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public void RecordDividendDetails()
        {


            log("RecordDividendDetails started");
            Thread.Sleep(1000);
            log("...pretending to do work...");
            Thread.Sleep(2500);
            log("RecordDividendDetails complete");
        }

        private void log(string message) { _logger.Log(message); }
    }
}