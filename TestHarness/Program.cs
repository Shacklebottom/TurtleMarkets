﻿using MarketDomain;
using TurtleAPI;
using TurtleAPI.PolygonIO;
using TurtleAPI.AlphaVantage;
using TurtleAPI.FinnhubIO;
using TurtleSQL;



#region SHACKLE WORKSPACE



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
//PreviousClose? marketDetail = AlphaVantageAPI.GetPreviousClose("MSFT");
//PreviousClose? marketDetail = PolygonAPI.GetPreviousClose("MSFT");
//PreviousClose? marketDetail = FinnhubAPI.GetPreviousClose("MSFT");
//pcRepo.Save(marketDetail);
//Console.WriteLine($"Entry Submitted as {marketDetail}");

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
//IRepository<ListedStatus> lsRepo = new ListedStatusRepository();
//IEnumerable<ListedStatus>? ListedStatus = AlphaVantageAPI.GetListedStatus(Activity.Active);
//IEnumerable<ListedStatus>? ListedStatus = AlphaVantageAPI.GetListedStatus(Activity.Delisted);

//foreach (var item in ListedStatus)
//{
//    lsRepo.Save(item);
//    Console.WriteLine($"Entry Submitted as {item}");
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

#endregion