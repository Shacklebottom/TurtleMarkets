using BusinessLogic.Logging;
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

namespace BusinessLogic
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class MarketService
    {
        private void log(string message) { _logger.Log(message); }

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
            _finnhubAPI = finnhubAPI ?? new FinnhubAPI();
            _polygonAPI = polygonAPI ?? new PolygonAPI();
            _alphavantageAPI = alphaVantageAPI ?? new AlphaVantageAPI();
            _logger = logger ?? new ConsoleLogger();
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
            var x = _listedStatusRepo.GetAll().Where(ls => ls.Ticker  == ticker).First();
            return x;
        }
        #endregion

        #region APIcalls

        public void RecordSnapshot()
        { //this should probably delete the previous snapshop so we only have one instance of a snapshot at a time :)
            try
            {
                log("Starting RecordSnapshot()");

                var lsRepo = _listedStatusRepo.GetAll().ToList();
                log($"...working on {lsRepo.Count} records.");

                lsRepo.ForEach(x =>
                {
                    log($"...Querying {x.Ticker}");
                    _snapShotRepo.Save(_finnhubAPI.GetPreviousClose(x.Ticker));
                });

                log("RecordSnapshot() complete.");
            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }
        public void RecordPreviousClose()
        {
            try
            {
                log("Starting RecordPreviousClose()");

                var ttRepo = _trackedTickerRepo.GetAll().ToList();
                log($"...working on {ttRepo.Count} records.");

                ttRepo.ForEach(x =>
                {
                    log($"...Querying {x.Ticker}");
                    _previousCloseRepo.Save(_finnhubAPI.GetPreviousClose(x.Ticker));
                });

                log("RecordPreviousClose() complete.");
            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }
        public void RecordDividendDetails()
        {
            try
            {
                log("Starting RecordDividendDetails");

                var ttRepo = _trackedTickerRepo.GetAll().ToList();

                log($"...working on {ttRepo.Count} records.");

                ttRepo.ForEach(x =>
                {
                    log($"...Querying {x.Ticker}");

                    foreach (var item in _polygonAPI.GetDividendDetails(x.Ticker))
                    {
                        _dividedDetailsRepo.Save(item);
                    }
                });

                log("RecordDividendDetails() complete.");
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public void RecordMarketStatus()
        {
            try
            {
                log("Starting CheckMarketStatus()");
                foreach (var item in _alphavantageAPI.GetMarketStatus())
                {
                    _marketStatusRepo.Save(item);
                }

                log("CheckMarketStatus() complete.");


            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public void RecordListedStatus()
        {
            try
            {
                log("Starting RecordListedStatus()");

                foreach (var item in _alphavantageAPI.GetListedStatus())
                {
                    _listedStatusRepo.Save(item);
                }

                log("RecordListedStatus() complete.");
            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public void RecordRecommendedTrend()
        { //This gives us weekly information
            try
            {
                log("Starting RecordRecommendedTrend()");

                var ttRepo = _trackedTickerRepo.GetAll().ToList();

                log($"...working on {ttRepo.Count} records.");

                ttRepo.ForEach(x =>
                {
                    log($"...Querying {x.Ticker}");
                    foreach (var item in _finnhubAPI.GetRecommendedTrend($"{x.Ticker}"))
                    {
                        _recommendedTrendRepo.Save(item);
                    }
                });
                log("RecordRecommendedTrend() complete.");
            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public void RecordTickerDetails()
        {
            try
            {
                log("Starting RecordTickerDetails()");

                var ttRepo = _trackedTickerRepo.GetAll().ToList();

                log($"...working on {ttRepo.Count} records.");

                ttRepo.ForEach(x =>
                {
                    log($"...Querying {x.Ticker}");
                    _tickerDetailRepo.Save(_polygonAPI.GetTickerDetails(x.Ticker));
                });
                log("RecordTickerDetail() complete.");
            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public void RecordMarketHoliday()
        {
            try
            {
                log("Starting RecordMarketHoliday()");
                foreach (var item in _polygonAPI.GetMarketHoliday())
                {
                    _marketHolidayRepo.Save(item);
                }
                log("RecordMarketHoliday() complete.");

            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }
        #endregion


    }
}