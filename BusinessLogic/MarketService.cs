using BusinessLogic.Logging;
using MarketDomain;
using System.Xml.Linq;
using TurtleAPI.FinnhubIO;
using TurtleAPI.PolygonIO;
using TurtleAPI.AlphaVantage;
using TurtleSQL.Interfaces;
using TurtleSQL.TickerRepositories;

namespace BusinessLogic
{
    public class MarketService
    {
        private void log(string message) { _logger.Log(message); }

        private readonly IRepository<PreviousClose> _previousCloseRepo;
        private readonly IRepository<DividendDetails> _dividedDetailsRepo;
        private readonly IRepository<ListedStatus> _listedStatusRepo;
        private readonly IRepository<Prominence> _prominenceRepo;
        private readonly IFinnhubAPI _finnhubAPI;
        private readonly IPolygonAPI _polygonAPI;
        private readonly IAlphaVantageAPI _alphavantageAPI;
        private readonly ILogger _logger;

        public MarketService(
            IRepository<PreviousClose>? pcRepo = null,
            IRepository<DividendDetails>? ddRepo = null,
            IRepository<ListedStatus>? lsRepo = null,
            IRepository<Prominence>? proRepo = null,
            IFinnhubAPI? finnhubAPI = null,
            IPolygonAPI? polygonAPI = null,
            IAlphaVantageAPI? alphaVantageAPI = null,
            ILogger? logger = null)
        {
            _previousCloseRepo = pcRepo ?? new PreviousCloseRepository();
            _dividedDetailsRepo = ddRepo ?? new DividendDetailRepository();
            _listedStatusRepo = lsRepo ?? new ListedStatusRepository();
            _prominenceRepo = proRepo ?? new ProminenceRepository();
            _finnhubAPI = finnhubAPI ?? new FinnhubAPI();
            _polygonAPI = polygonAPI ?? new PolygonAPI();
            _alphavantageAPI = alphaVantageAPI ?? new AlphaVantageAPI();
            _logger = logger ?? new ConsoleLogger();
        }

        public void RecordPreviousClose()
        {
            try
            {
                log("Starting RecordPreviousClose()");

                var lsRepo = _listedStatusRepo.GetAll().ToList();
                log($"...working on {lsRepo.Count} records.");

                lsRepo.ForEach(x =>
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
            try
            {
                log("Starting RecordDividendDetails");

                var lsRepo = _listedStatusRepo.GetAll().ToList();

                log($"...working on {lsRepo.Count} records.");
                
                lsRepo.ForEach(x =>
                {
                    log($"...Querying {x.Ticker}");
                    
                    foreach (var item in _polygonAPI.GetDividendDetails(x.Ticker))
                    {
                        _dividedDetailsRepo.Save(item);
                    }
                });

                log("RecordDividendDetails() complete.");
            } 
            catch( Exception ex ) 
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public void RecordDailyProminence()
        {
            try
            {
                log("Starting RecordDailyProminence()");
                
                var prominence = _alphavantageAPI.GetPolarizedMarkets().Values.ToList();
                
                prominence.ForEach(x =>
                {
                    foreach (var item in x)
                    {
                        _prominenceRepo.Save(item);
                    }
                });

                log("RecordDailyProminence() complete.");
            }
            catch (Exception ex )
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public MarketStatus? CheckMarketStatus(string exchange)
        {
            try
            {
                log("Starting CheckMarketStatus()");
                
                var marketStatus = _alphavantageAPI?.GetMarketStatus()?.Where(e => e.Exchange == exchange).First();
                log("CheckMarketStatus() complete.");
                return marketStatus;

            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
                return null;
            }
        }
    }
}