﻿using BusinessLogic.Logging;
using MarketDomain;
using System.Xml.Linq;
using TurtleAPI.FinnhubIO;
using TurtleAPI.PolygonIO;
using TurtleAPI.AlphaVantage;
using TurtleSQL.Interfaces;
using TurtleSQL.TickerRepositories;
using TurtleSQL.MarketStatusForecast;

namespace BusinessLogic
{
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
            _finnhubAPI = finnhubAPI ?? new FinnhubAPI();
            _polygonAPI = polygonAPI ?? new PolygonAPI();
            _alphavantageAPI = alphaVantageAPI ?? new AlphaVantageAPI();
            _logger = logger ?? new ConsoleLogger();
        }


        #region APIcalls
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
        { //may refactor?
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
        {
            try
            {
                log("Starting RecordRecommendedTrend()");

                var lsRepo = _listedStatusRepo.GetAll().ToList();

                log($"...working on {lsRepo.Count} records.");

                lsRepo.ForEach(x => 
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

                var lsRepo = _listedStatusRepo.GetAll().ToList();

                log($"...working on {lsRepo.Count} records.");

                lsRepo.ForEach(x =>
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