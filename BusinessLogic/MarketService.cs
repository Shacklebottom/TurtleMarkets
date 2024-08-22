using LoggerModule.DerivedClasses;
using LoggerModule.Interfaces;
using ApiModule.CustomExceptions;
using MarketDomain;
using System.Xml.Linq;
using TurtleAPI.FinnhubIO;
using TurtleAPI.PolygonIO;
using TurtleAPI.AlphaVantage;
using TurtleSQL.Interfaces;
using TurtleSQL.TickerRepositories;
using TurtleSQL.MarketStatusForecast;
using MarketDomain.Extensions;
using System.Diagnostics.CodeAnalysis;
using TurtleSQL.BaseClasses;
using System.Globalization;
using System.Data.SqlTypes;

namespace BusinessLogic
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class MarketService
    {
        private void log(string message) { _logger.Chat(message); }

        private readonly IRepository<PreviousClose> _previousCloseRepo;
        private readonly IRepository<DividendDetails> _dividedDetailsRepo;
        private readonly IRepository<ListedStatus> _listedStatusRepo;
        private readonly IRepository<RecommendedTrend> _recommendedTrendRepo;
        private readonly IRepository<Prominence> _prominenceRepo;
        private readonly IRepository<TickerDetail> _tickerDetailRepo;
        private readonly IRepository<MarketHoliday> _marketHolidayRepo;
        private readonly IRepository<MarketStatus> _marketStatusRepo;
        private readonly IRepository<TrackedTicker> _trackedTickerRepo;
        private readonly IRepository<PreviousClose> _snapShotRepo;
        private readonly IFinnhubAPI _finnhubAPI;
        private readonly IPolygonAPI _polygonAPI;
        private readonly IAlphaVantageAPI _alphavantageAPI;
        private readonly ILogger _logger;
        private readonly DebugDirectory _debugDirectory = new($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}", "MarketDebugLogs");

        public MarketService(
            IRepository<PreviousClose>? pcRepo = null,
            IRepository<DividendDetails>? ddRepo = null,
            IRepository<ListedStatus>? lsRepo = null,
            IRepository<Prominence>? proRepo = null,
            IRepository<RecommendedTrend>? rtRepo = null,
            IRepository<TickerDetail>? tdRepo = null,
            IRepository<MarketHoliday>? mhRepo = null,
            IRepository<MarketStatus>? msRepo = null,
            IRepository<TrackedTicker>? ttRepo = null,
            IRepository<PreviousClose>? ssRepo = null,
            IFinnhubAPI? finnhubAPI = null,
            IPolygonAPI? polygonAPI = null,
            IAlphaVantageAPI? alphaVantageAPI = null,
            ILogger? logger = null)
        {
            _previousCloseRepo = pcRepo ?? new PreviousCloseRepository();
            _dividedDetailsRepo = ddRepo ?? new DividendDetailRepository();
            _listedStatusRepo = lsRepo ?? new ListedStatusRepository();
            _prominenceRepo = proRepo ?? new ProminenceRepository();
            _recommendedTrendRepo = rtRepo ?? new RecommendedTrendRepository();
            _tickerDetailRepo = tdRepo ?? new TickerDetailRepository();
            _marketHolidayRepo = mhRepo ?? new MarketHolidayRepository();
            _marketStatusRepo = msRepo ?? new MarketStatusRepository();
            _trackedTickerRepo = ttRepo ?? new TrackedTickerRepository();
            _snapShotRepo = ssRepo ?? new SnapshotRepository();
            _logger = logger ?? new DebugLogger(_debugDirectory);
            _finnhubAPI = finnhubAPI ?? new FinnhubAPI(_logger);
            _polygonAPI = polygonAPI ?? new PolygonAPI(_logger);
            _alphavantageAPI = alphaVantageAPI ?? new AlphaVantageAPI(_logger);
        }



        #region Market Workshop
        /*
         * Using this space for multi-track conceptualization.
         *      (I see our ability to filter as a strong option, so there may be several ways of accomplishing this);
         */

        public IEnumerable<PreviousClose> GetFilteredTickers(int high, int low)
        {
            var x = _snapShotRepo.GetAll().Where(x => x.High < high && x.Low > low).ToList();
            log($"This range has {x.Count} entries");
            return x;
        }
        public void RecordFilteredTickers(IEnumerable<PreviousClose> ft)
        {
            ft.ForEach(i => _trackedTickerRepo.Save(new TrackedTicker { Ticker = i.Ticker }));
        }

        public IEnumerable<PreviousClose> GetByMath(IEnumerable<PreviousClose> filteredCloses, int y)
        {
            var x = filteredCloses.Where(ss => ss.High - ss.Low > y && ss.High > ss.Low).ToList();
            log($"This range has {x.Count} entries");
            return x;
        }

        public ListedStatus GetListedStatus(string ticker)
        {
            var x = _listedStatusRepo.GetAll().Where(ls => ls.Ticker == ticker).First();
            return x;
        }
        public IEnumerable<ListedStatus> GetListedStatuses()
        {
            var x = _listedStatusRepo.GetAll().Where(
                ls => (ls.Exchange.Contains("NYSE") ||
                ls.Exchange.Contains("NASDAQ")) &&
                ls.Type == "Stock").ToList();
            return x;

        }
        #endregion

        #region APIcalls

        public async Task RecordSnapshot()
        { //this should probably delete the previous snapshop so we only have one instance of a snapshot at a time :)
            try
            {
                log("Starting RecordSnapshot()");

                var lsRepo = _listedStatusRepo.GetAll().ToList();

                log($"...working on {lsRepo.Count} records.");

                foreach (var item in lsRepo)
                {
                    log($"...Querying {item.Ticker}");
                    var result = await _finnhubAPI.GetPreviousClose(item.Ticker);

                    if (result != null)
                    {
                        _snapShotRepo.Save(result); 
                    }
                }

                log("RecordSnapshot() complete.");
            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }
        public async Task RecordPreviousClose()
        {
            try
            {
                log("Starting RecordPreviousClose()");

                var lsRepo = _listedStatusRepo.GetAll().ToList();

                var filteredRepo = lsRepo.Where(
                    ls => (ls.Exchange?.Contains("NYSE") == true || 
                    ls.Exchange?.Contains("NASDAQ") == true) && 
                    ls.Type == "Stock").ToList();

                log($"...working on {filteredRepo.Count} records.");

                foreach (var item in filteredRepo)
                {
                    log($"...Querying {item.Ticker}");
                    var result = await _finnhubAPI.GetPreviousClose(item.Ticker);

                    if (result != null)
                    {
                        _previousCloseRepo.Save(result);
                    }
                }

                log("RecordPreviousClose() complete.");
            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public async Task RecordDividendDetails()
        {
            try
            {
                log("Starting RecordDividendDetails");

                var lsRepo = _listedStatusRepo.GetAll().ToList();

                var ddRepo = _dividedDetailsRepo.GetAll().GroupBy(x => x.Ticker).Select(y => y.First()).ToList();

                var capturedTicker = new List<string>();
                ddRepo.ForEach(z => capturedTicker.Add(z.Ticker));

                var filteredRepo = lsRepo
                    .Where(
                    ls => (ls.Exchange?.Contains("NYSE") == true ||
                    ls.Exchange?.Contains("NASDAQ") == true) &&
                    ls.Type == "Stock" &&
                    !capturedTicker.Contains(ls.Ticker))
                    .Take(600).ToList();

                log($"...working on {filteredRepo.Count} records.");

                foreach (var item in filteredRepo)
                {
                    log($"...Querying {item.Ticker}");
                    var result = await _polygonAPI.GetDividendDetails(item.Ticker);

                    if (result != null)
                    {
                        foreach (var detail in result)
                        {
                            _dividedDetailsRepo.Save(detail);
                        }
                    }
                }

                log("RecordDividendDetails() complete.");
            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public async Task RecordDailyProminence()
        {
            try
            {
                log("Starting RecordDailyProminence()");

                var results = await _alphavantageAPI.GetPolarizedMarkets();
                if (results != null)
                {
                    var prominence = results.Values.ToList();
                    prominence.ForEach(item =>
                    {
                        foreach (var i in item)
                        {
                            _prominenceRepo.Save(i);
                        }
                    });
                }
                
                log("RecordDailyProminence() complete.");
            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public async Task RecordMarketStatus()
        {
            try
            {
                log("Starting CheckMarketStatus()");

                var results = await _alphavantageAPI.GetMarketStatus();
                if (results != null)
                {
                    foreach (var item in results)
                    {
                        _marketStatusRepo.Save(item);
                    }
                }
                log("CheckMarketStatus() complete.");
            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public async Task RecordListedStatus()
        {
            try
            {
                log("Truncating ListedStatus Repo");

                _listedStatusRepo.TruncateTable();

                log("Truncating Complete\n\n");

                log("Starting RecordListedStatus()");

                var results = await _alphavantageAPI.GetListedStatus();


                if (results != null)
                {
                    foreach (var item in results)
                    {
                        _listedStatusRepo.Save(item);
                    }

                }
                log("RecordListedStatus() complete.");
            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public async Task RecordRecommendedTrend()
        { //This gives us weekly information
            try
            {
                log("Starting RecordRecommendedTrend()");

                var lsRepo = _listedStatusRepo.GetAll().ToList();

                var filteredRepo = lsRepo.Where(
                    ls => (ls.Exchange?.Contains("NYSE") == true ||
                    ls.Exchange?.Contains("NASDAQ") == true) &&
                    ls.Type == "Stock").ToList();

                log($"...working on {filteredRepo.Count} records.");

                foreach (var item in filteredRepo)
                {
                    log($"...Querying {item.Ticker}");
                    var result = await _finnhubAPI.GetRecommendedTrend(item.Ticker);
                    if (result != null)
                    {
                        foreach (var trend in result)
                        {
                            _recommendedTrendRepo.Save(trend);
                        }
                    }
                }

                log("RecordRecommendedTrend() complete.");
            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public async Task RecordTickerDetails()
        {
            try
            {
                log("Starting RecordTickerDetails()");

                var lsRepo = _listedStatusRepo.GetAll().ToList();

                var tdRepo = _tickerDetailRepo.GetAll().ToList();

                List<string> capturedTicker = [];
                tdRepo.ForEach(z => capturedTicker.Add(z.Ticker));

                var filteredRepo = lsRepo.Where(
                    ls => (ls.Exchange?.Contains("NYSE") == true ||
                    ls.Exchange?.Contains("NASDAQ") == true) &&
                    ls.Type == "Stock" && !capturedTicker.Contains(ls.Ticker))
                    .Take(600).ToList();

                log($"...working on {filteredRepo.Count} records.");

                foreach (var item in filteredRepo)
                {
                    log($"...Querying {item.Ticker}");
                    var result = await _polygonAPI.GetTickerDetails(item.Ticker);
                    if (result != null)
                    {
                        _tickerDetailRepo.Save(result);
                    }
                }

                log("RecordTickerDetail() complete.");
            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public async Task RecordMarketHoliday()
        {
            try
            {
                log("Starting RecordMarketHoliday()");

                var results = await _polygonAPI.GetMarketHoliday();

                if (results != null)
                {
                    foreach (var item in results)
                    {
                        _marketHolidayRepo.Save(item);
                    }
                    log("RecordMarketHoliday() complete.");
                }
            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }
        #endregion


    }
}