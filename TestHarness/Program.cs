using MarketDomain;
using TurtleAPI;
using TurtleAPI.PolygonIO;
using TurtleAPI.AlphaVantage;
using TurtleAPI.FinnhubIO;
using TurtleSQL;

IRepository<PreviousClose> pcRepo = new PreviousCloseRepository();

Console.WriteLine(pcRepo.Get(1)?.ToString());

//var Polygon = new PolygonAPI();
//MarketDetail? marketDetail = Polygon.GetPreviousClose("MSFT");
//Console.WriteLine(marketDetail);

//var AlphaVantage = new AlphaVantageAPI();
//PreviousClose? marketDetail = AlphaVantage.GetPreviousClose("MSFT");
//Console.WriteLine(marketDetail);
