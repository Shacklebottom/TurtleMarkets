using MarketDomain;
using TurtleAPI;
using TurtleAPI.PolygonIO;
using TurtleAPI.AlphaVantage;
using TurtleAPI.FinnhubIO;
using TurtleSQL;

//IRepository<PreviousClose> pcRepo = new PreviousCloseRepository();

//Console.WriteLine(pcRepo.GetById(1)?.ToString());

//var Polygon = new PolygonAPI();
//MarketDetail? marketDetail = Polygon.GetPreviousClose("MSFT");
//Console.WriteLine(marketDetail);

//var AlphaVantage = new AlphaVantageAPI();
//PreviousClose? marketDetail = AlphaVantage.GetPreviousClose("MSFT");
//Console.WriteLine(marketDetail);

//var Finnhub = new FinnhubAPI();
//PreviousClose previousClose = Finnhub.GetPreviousClose("MSFT") ?? 
//    throw new Exception("previousClose is NULL");
//Console.WriteLine(previousClose);

//var recordId = pcRepo.Save(previousClose);
//Console.WriteLine($"Saved Finnhub result as record {recordId}");

//var pc = pcRepo.GetById(5) ?? throw new Exception("Couldn't find Id 5");
//pc.Ticker = "XXXX";
//pcRepo.Save(pc);
//Console.WriteLine("Updated.");

//var Polygon = new PolygonAPI();
//TickerDetail? tickerDetail = Polygon.GetTickerDetails("MSFT");
//Console.WriteLine(tickerDetail);

//var AlphaV = new AlphaVantageAPI();
//MarketStatus? marketStatus = AlphaV.GetMarketStatus();
//Console.WriteLine(marketStatus);

//var Finnhub = new FinnhubAPI();
//RecommendedTrend? recommendedTrend = Finnhub.GetRecommendedTrend("MSFT");
//Console.WriteLine(recommendedTrend);

//var AlphaV = new AlphaVantageAPI();
//Prominence? Prominence = AlphaV.GetPolarizedMarkets()[PrestigeType.TopGainer]?.First();
//Console.WriteLine(Prominence);

//var AlphaV = new AlphaVantageAPI();
//ListedStatus? ListedStatus = AlphaV.GetListedStatus("active").First();
//Console.WriteLine(ListedStatus);

//var Polygon = new PolygonAPI();
//MarketHoliday? MarketHoliday = Polygon.GetMarketHoliday().First();
//Console.WriteLine(MarketHoliday);