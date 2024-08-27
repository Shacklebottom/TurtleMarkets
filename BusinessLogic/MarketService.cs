using LoggerModule.Interfaces;
using TurtleAPI.FinnhubIO;
using TurtleAPI.PolygonIO;
using TurtleAPI.AlphaVantage;
using TurtleSQL.Interfaces;
using TurtleSQL.TickerRepositories;
using TurtleSQL.MarketStatusForecast;
using MarketDomain.Objects;
using MarketDomain.Interfaces;
using MarketDomain.Enums;

namespace BusinessLogic
{
    #pragma warning disable IDE1006 //naming styles
    public class MarketService
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly ILogger _logger;
        private void log(string message) { _logger.Chat(message); }

        public MarketService(IServiceLocator serviceLocator, ILogger logger)
        {
            _logger = logger;
            _serviceLocator = serviceLocator;

            _serviceLocator.RegisterService<IRepository<PreviousClose>>($"{EnumMarketService.PreviousClose}", () => new PreviousCloseRepository());
            _serviceLocator.RegisterService<IRepository<DividendDetails>>($"{EnumMarketService.DividendDetails}", () => new DividendDetailRepository());
            _serviceLocator.RegisterService<IRepository<ListedStatus>>($"{EnumMarketService.ListedStatus}", () => new ListedStatusRepository());
            _serviceLocator.RegisterService<IRepository<RecommendedTrend>>($"{EnumMarketService.RecommendedTrend}", () => new RecommendedTrendRepository());
            _serviceLocator.RegisterService<IRepository<Prominence>>($"{EnumMarketService.Prominence}", () => new ProminenceRepository());
            _serviceLocator.RegisterService<IRepository<TickerDetail>>($"{EnumMarketService.TickerDetail}", () => new TickerDetailRepository());
            _serviceLocator.RegisterService<IRepository<MarketHoliday>>($"{EnumMarketService.MarketHoliday}", () => new MarketHolidayRepository());
            _serviceLocator.RegisterService<IRepository<MarketStatus>>($"{EnumMarketService.MarketStatus}", () => new MarketStatusRepository());
            _serviceLocator.RegisterService<IRepository<TrackedTicker>>($"{EnumMarketService.TrackedTicker}", () => new TrackedTickerRepository());
            _serviceLocator.RegisterService<IRepository<PreviousClose>>($"{EnumMarketService.Snapshot}", () => new SnapshotRepository());
            _serviceLocator.RegisterService<IFinnhubAPI>($"{EnumMarketService.FinnhubAPI}", () => new FinnhubAPI(_logger));
            _serviceLocator.RegisterService<IAlphaVantageAPI>($"{EnumMarketService.AlphaVantageAPI}", () => new AlphaVantageAPI(_logger));
            _serviceLocator.RegisterService<IPolygonAPI>($"{EnumMarketService.PolygonAPI}", () => new PolygonAPI(_logger));
        }

        #region Market Workshop
        ///*
        // * Using this space for multi-track conceptualization.
        // *      (I see our ability to filter as a strong option, so there may be several ways of accomplishing this);
        // */

        //public IEnumerable<PreviousClose> GetFilteredTickers(int high, int low)
        //{
        //    var x = _snapShotRepo.GetAll().Where(x => x.High < high && x.Low > low).ToList();
        //    log($"This range has {x.Count} entries");
        //    return x;
        //}
        //public void RecordFilteredTickers(IEnumerable<PreviousClose> ft)
        //{
        //    ft.ForEach(i => _trackedTickerRepo.Save(new TrackedTicker { Ticker = i.Ticker }));
        //}

        //public IEnumerable<PreviousClose> GetByMath(IEnumerable<PreviousClose> filteredCloses, int y)
        //{
        //    var x = filteredCloses.Where(ss => ss.High - ss.Low > y && ss.High > ss.Low).ToList();
        //    log($"This range has {x.Count} entries");
        //    return x;
        //}

        //public ListedStatus GetListedStatus(string ticker)
        //{
        //    var x = _listedStatusRepo.GetAll().Where(ls => ls.Ticker == ticker).First();
        //    return x;
        //}
        //public IEnumerable<ListedStatus> GetListedStatuses()
        //{
        //    var x = _listedStatusRepo.GetAll().Where(
        //        ls => (ls.Exchange?.Contains("NYSE") == true ||
        //        ls.Exchange?.Contains("NASDAQ") == true) &&
        //        ls.Type == "Stock").ToList();
        //    return x;

        //}
        #endregion

        #region APIcalls
        public async Task RecordSnapshot()
        {
            var snapShotRepo = _serviceLocator.GetService<IRepository<PreviousClose>>($"{EnumMarketService.Snapshot}");
            var listedStatusRepo = _serviceLocator.GetService<IRepository<ListedStatus>>($"{EnumMarketService.ListedStatus}");
            var finnhubAPI = _serviceLocator.GetService<IFinnhubAPI>($"{EnumMarketService.FinnhubAPI}");

            try
            {
                log("Truncating the Snapshot Repo");

                snapShotRepo.TruncateTable();

                log("Truncate complete");

                log("Starting RecordSnapshot()");

                var lsRepo = listedStatusRepo.GetAll().ToList();

                log($"...working on {lsRepo.Count} records.");

                foreach (var item in lsRepo)
                {
                    log($"...Querying {item.Ticker}");
                    var result = await finnhubAPI.GetPreviousClose(item.Ticker);

                    if (result != null)
                    {
                        snapShotRepo.Save(result);
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
            var listedStatusRepo = _serviceLocator.GetService<IRepository<ListedStatus>>($"{EnumMarketService.ListedStatus}");
            var previousCloseRepo = _serviceLocator.GetService<IRepository<PreviousClose>>($"{EnumMarketService.PreviousClose}");
            var finnhubAPI = _serviceLocator.GetService<IFinnhubAPI>($"{EnumMarketService.FinnhubAPI}");

            try
            {
                log("Starting RecordPreviousClose()");

                var lsRepo = listedStatusRepo.GetAll().ToList();

                var filteredRepo = lsRepo.Where(
                    ls => (ls.Exchange?.Contains("NYSE") == true ||
                    ls.Exchange?.Contains("NASDAQ") == true) &&
                    ls.Type == "Stock").ToList();

                log($"...working on {filteredRepo.Count} records.");

                foreach (var item in filteredRepo)
                {
                    log($"...Querying {item.Ticker}");
                    var result = await finnhubAPI.GetPreviousClose(item.Ticker);

                    if (result != null)
                    {
                        previousCloseRepo.Save(result);
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
            var listedStatusRepo = _serviceLocator.GetService<IRepository<ListedStatus>>($"{EnumMarketService.ListedStatus}");
            var dividendDetailsRepo = _serviceLocator.GetService<IRepository<DividendDetails>>($"{EnumMarketService.DividendDetails}");
            var polygonAPI = _serviceLocator.GetService<IPolygonAPI>($"{EnumMarketService.PolygonAPI}");

            try
            {
                log("Truncating DividendDetails Repo");

                dividendDetailsRepo.TruncateTable();

                log("Truncate complete");

                log("Starting RecordDividendDetails");

                var lsRepo = listedStatusRepo.GetAll().ToList();

                var ddRepo = dividendDetailsRepo.GetAll().GroupBy(x => x.Ticker).Select(y => y.First()).ToList();

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
                    var result = await polygonAPI.GetDividendDetails(item.Ticker);

                    if (result != null)
                    {
                        foreach (var detail in result)
                        {
                            dividendDetailsRepo.Save(detail);
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
            var prominenceRepo = _serviceLocator.GetService<IRepository<Prominence>>($"{EnumMarketService.Prominence}");
            var alphaVantageAPI = _serviceLocator.GetService<IAlphaVantageAPI>($"{EnumMarketService.AlphaVantageAPI}");

            try
            {
                log("Truncating Prominence Repo");

                prominenceRepo.TruncateTable();

                log("Truncate complete");

                log("Starting RecordDailyProminence()");

                var results = await alphaVantageAPI.GetPolarizedMarkets();
                if (results != null)
                {
                    var prominence = results.Values.ToList();
                    prominence.ForEach(item =>
                    {
                        foreach (var i in item)
                        {
                            prominenceRepo.Save(i);
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
            var marketStatusRepo = _serviceLocator.GetService<IRepository<MarketStatus>>($"{EnumMarketService.MarketStatus}");
            var alphaVantageAPI = _serviceLocator.GetService<IAlphaVantageAPI>($"{EnumMarketService.AlphaVantageAPI}");

            try
            {
                log("Truncating MarketStatus Repo");

                marketStatusRepo.TruncateTable();

                log("Truncate complete");

                log("Starting CheckMarketStatus()");

                var results = await alphaVantageAPI.GetMarketStatus();
                if (results != null)
                {
                    foreach (var item in results)
                    {
                        marketStatusRepo.Save(item);
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
            var listedStatusRepo = _serviceLocator.GetService<IRepository<ListedStatus>>($"{EnumMarketService.ListedStatus}");
            var alphaVantageAPI = _serviceLocator.GetService<IAlphaVantageAPI>($"{EnumMarketService.AlphaVantageAPI}");

            try
            {
                log("Truncating ListedStatus Repo");

                listedStatusRepo.TruncateTable();

                log("Truncating Complete\n\n");

                log("Starting RecordListedStatus()");

                var results = await alphaVantageAPI.GetListedStatus();

                if (results != null)
                {
                    foreach (var item in results)
                    {
                        listedStatusRepo.Save(item);
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
            var listedStatusRepo = _serviceLocator.GetService<IRepository<ListedStatus>>($"{EnumMarketService.ListedStatus}");
            var recommendedTrendRepo = _serviceLocator.GetService<IRepository<RecommendedTrend>>($"{EnumMarketService.RecommendedTrend}");
            var finnhubAPI = _serviceLocator.GetService<IFinnhubAPI>($"{EnumMarketService.FinnhubAPI}");

            try
            {
                log("Truncating RecommendedTrend Repo");

                recommendedTrendRepo.TruncateTable();

                log("Truncate complete");

                log("Starting RecordRecommendedTrend()");

                var lsRepo = listedStatusRepo.GetAll().ToList();

                var filteredRepo = lsRepo.Where(
                    ls => (ls.Exchange?.Contains("NYSE") == true ||
                    ls.Exchange?.Contains("NASDAQ") == true) &&
                    ls.Type == "Stock").ToList();

                log($"...working on {filteredRepo.Count} records.");

                foreach (var item in filteredRepo)
                {
                    log($"...Querying {item.Ticker}");
                    var result = await finnhubAPI.GetRecommendedTrend(item.Ticker);
                    if (result != null)
                    {
                        foreach (var trend in result)
                        {
                            recommendedTrendRepo.Save(trend);
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
            var listedStatusRepo = _serviceLocator.GetService<IRepository<ListedStatus>>($"{EnumMarketService.ListedStatus}");
            var tickerDetailRepo = _serviceLocator.GetService<IRepository<TickerDetail>>($"{EnumMarketService.TickerDetail}");
            var polygonAPI = _serviceLocator.GetService<IPolygonAPI>($"{EnumMarketService.PolygonAPI}");

            try
            {
                log("Starting RecordTickerDetails()");

                var lsRepo = listedStatusRepo.GetAll().ToList();

                var tdRepo = tickerDetailRepo.GetAll().ToList();

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
                    var result = await polygonAPI.GetTickerDetails(item.Ticker);
                    if (result != null)
                    {
                        tickerDetailRepo.Save(result);
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
            var marketHolidayRepo = _serviceLocator.GetService<IRepository<MarketHoliday>>($"{EnumMarketService.MarketHoliday}");
            var polygonAPI = _serviceLocator.GetService<IPolygonAPI>($"{EnumMarketService.PolygonAPI}");

            try
            {
                log("Truncating MarketHoliday Repo");

                marketHolidayRepo.TruncateTable();

                log("Truncate complete");

                log("Starting RecordMarketHoliday()");

                var results = await polygonAPI.GetMarketHoliday();

                if (results != null)
                {
                    foreach (var item in results)
                    {
                        marketHolidayRepo.Save(item);
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