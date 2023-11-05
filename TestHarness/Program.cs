using MarketDomain;
using TurtleAPI;
using TurtleAPI.PolygonIO;
using TurtleAPI.AlphaVantage;
using TurtleAPI.FinnhubIO;
using TurtleSQL;

IRepository<PreviousClose> pcRepo = new PreviousCloseRepository();

//Console.WriteLine(pcRepo.GetById(1)?.ToString());

//var Polygon = new PolygonAPI();
//MarketDetail? marketDetail = Polygon.GetPreviousClose("MSFT");
//Console.WriteLine(marketDetail);

//var AlphaVantage = new AlphaVantageAPI();
//PreviousClose? marketDetail = AlphaVantage.GetPreviousClose("MSFT");
//Console.WriteLine(marketDetail);

var Finnhub = new FinnhubAPI();
PreviousClose previousClose = Finnhub.GetPreviousClose("MSFT") ?? 
    throw new Exception("previousClose is NULL");
Console.WriteLine(previousClose);

var recordId = pcRepo.Save(previousClose);
Console.WriteLine($"Saved Finnhub result as record {recordId}");

var record = pcRepo.GetById(recordId);