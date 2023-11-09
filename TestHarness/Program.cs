using MarketDomain;
using TurtleAPI;
using TurtleAPI.PolygonIO;
using TurtleAPI.AlphaVantage;
using TurtleAPI.FinnhubIO;
using TurtleSQL;



//This is not meant to be a repo, but a checker/driver
//var AlphaV = new AlphaVantageAPI();
//MarketStatus? marketStatus = AlphaV.GetMarketStatus();
//Console.WriteLine(marketStatus);



#region VALIDATED REPOSITORIES

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
//Prominence? Prominence = AlphaVantageAPI.GetPolarizedMarkets()[PrestigeType.TopGainer]?.First();
//Console.WriteLine(Prominence);
//IRepository<Prominence> proRepo = new ProminenceRepository();
//proRepo.Save(Prominence);
//Console.WriteLine("Entry Submitted");

//VALIDATED!
//IRepository<RecommendedTrend> rtRepo = new RecommendedTrendRepository();
//RecommendedTrend? recommendedTrend = FinnhubAPI.GetRecommendedTrend("MSFT");
//rtRepo.Save(recommendedTrend);
//Console.WriteLine($"Entry Submitted as {recommendedTrend}");

//VALIDATED!
//IRepository<ListedStatus> lsRepo = new ListedStatusRepository();
//ListedStatus? ListedStatus = AlphaVantageAPI.GetListedStatus("active").First();
//lsRepo.Save(ListedStatus);
//Console.WriteLine($"Entry Submitted as {ListedStatus}");

//VALIDATED!
//IRepository<MarketHoliday> mhRepo = new MarketHolidayRepository();
//MarketHoliday? MarketHoliday = PolygonAPI.GetMarketHoliday().First();
//mhRepo.Save(MarketHoliday);
//Console.WriteLine($"Entry Submitted as {MarketHoliday}");

#endregion