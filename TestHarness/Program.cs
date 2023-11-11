﻿using MarketDomain;
using TurtleAPI;
using TurtleAPI.PolygonIO;
using TurtleAPI.AlphaVantage;
using TurtleAPI.FinnhubIO;
using TurtleSQL;



#region SHACKLE WORKSPACE

//PreviousCloseRepository pcRepo = new PreviousCloseRepository();
//var data = pcRepo.GetByTicker("AAPL");
//foreach (var item in data)
//{
//    Console.WriteLine(item);

//}

#endregion

#region FUNCTION CHECK
//This API call is not meant to be a repo, but a checker/driver
//IEnumerable<MarketStatus>? marketStatus = AlphaVantageAPI.GetMarketStatus();
//foreach (var item in marketStatus)
//{
//    Console.WriteLine(item);
//}
#endregion

#region VALIDATION

//VALIDATED!

//IRepository<PreviousClose> pcRepo = new PreviousCloseRepository();
//PreviousClose? marketDetail0 = AlphaVantageAPI.GetPreviousClose("MSFT");
//PreviousClose? marketDetail1 = PolygonAPI.GetPreviousClose("MSFT");
//PreviousClose? marketDetail2 = FinnhubAPI.GetPreviousClose("MSFT");
//pcRepo.Save(marketDetail0);
//pcRepo.Save(marketDetail1);
//pcRepo.Save(marketDetail2);
//var loadedRepo = pcRepo.GetAll();
//foreach (var item in loadedRepo)
//{
//    Console.WriteLine(item);
//}

//VALIDATED!
//TickerDetail? tickerDetail = PolygonAPI.GetTickerDetails("MSFT");
//Console.WriteLine(tickerDetail);
//IRepository<TickerDetail> tdRepo = new TickerDetailRepository();
//tdRepo.Save(tickerDetail);
//Console.WriteLine("Entry Submitted");

//VALIDATED!
//IRepository<Prominence> proRepo = new ProminenceRepository();
//IEnumerable<Prominence>? Prominence = AlphaVantageAPI.GetPolarizedMarkets()[PrestigeType.TopGainer];
//IEnumerable<Prominence>? Prominence = AlphaVantageAPI.GetPolarizedMarkets()[PrestigeType.TopLoser];
//IEnumerable<Prominence>? Prominence = AlphaVantageAPI.GetPolarizedMarkets()[PrestigeType.MostTraded];
//foreach (var item in Prominence)
//{
//    proRepo.Save(item);
//    Console.WriteLine($"Entry Submitted as {item}");
//}

//VALIDATED!
//IRepository<RecommendedTrend> rtRepo = new RecommendedTrendRepository();
//RecommendedTrend? recommendedTrend = FinnhubAPI.GetRecommendedTrend("MSFT");
//rtRepo.Save(recommendedTrend);
//Console.WriteLine($"Entry Submitted as {recommendedTrend}");

//VALIDATED!
//ITickerRepository<ListedStatus> lsRepo = new ListedStatusRepository();
//var listedStatus = lsRepo.GetByTicker("MSFT");
//IEnumerable<ListedStatus>? ListedStatus = AlphaVantageAPI.GetListedStatus(Activity.Active);
//IEnumerable<ListedStatus>? ListedStatus = AlphaVantageAPI.GetListedStatus(Activity.Delisted);

//foreach (var item in listedStatus)
//{
//    Console.WriteLine($"{item}");
//}
//Console.WriteLine("<======END OF CALL======>");

//VALIDATED!
//IRepository<MarketHoliday> mhRepo = new MarketHolidayRepository();
//IEnumerable<MarketHoliday>? MarketHoliday = PolygonAPI.GetMarketHoliday();
//foreach (var item in MarketHoliday)
//{
//    mhRepo.Save(item);
//    Console.WriteLine($"Entry Submitted as {item}");
//}

//VALIDATED!
//IRepository<ListedStatus> lsRepo = new ListedStatusRepository();
//IRepository<PreviousClose> pcRepo = new PreviousCloseRepository();
//IRepository<RecommendedTrend> rtRepo = new RecommendedTrendRepository();
//IRepository<WorkTicker> wtRepo = new WorkTickerRepository();
//var loadedRepo = lsRepo.GetAll();
//var strippedTicker = loadedRepo.Where(s => s.Status == Activity.Active).Select(t => new WorkTicker
//{
//    Ticker = t.Ticker,
//});
//foreach (var item in strippedTicker)
//{
//    wtRepo.Save(item);
//    Console.WriteLine($"Active Ticker Recorded: {item.Ticker}");
//}
//ITickerRepository<ListedStatus> lsRepo = new ListedStatusRepository();
//ITickerRepository<DividendDetails> ddRepo = new DividendDetailRepository();
//var loadedRepo = ddRepo.GetAll();
//foreach (var item in loadedRepo)
//{
//    Console.WriteLine($"Entry PayoutPerShare: {item.PayoutPerShare}");
//}
//var loadedRepo = lsRepo.GetAll();
//var count = 0;
//foreach (var item in loadedRepo)
//{
//    if (count != 4)
//    {
//        count++;
//        IEnumerable<DividendDetails> dividendDetails = PolygonAPI.GetDividendDetails($"{item.Ticker}");
//        foreach (var detail in dividendDetails)
//        {
//            ddRepo.Save(detail);
//            Console.WriteLine($"Entry Recorded as {detail}");
//            Console.WriteLine($"+====THE COUNT IS: {count}====+");
//        }
//    }
//    else return;

//}
#endregion