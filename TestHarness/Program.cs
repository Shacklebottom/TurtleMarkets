using MarketDomain;
using TurtleAPI;
using TurtleAPI.PolygonIO;
using TurtleAPI.AlphaVantage;
using TurtleAPI.FinnhubIO;
using TurtleSQL;


//var Polygon = new PolygonAPI();
//MarketDetail? marketDetail = Polygon.GetPreviousClose("MSFT");
//Console.WriteLine(marketDetail);

var AlphaVantage = new AlphaVantageAPI();
MarketDetail? marketDetail = AlphaVantage.GetPreviousClose("MSFT");
Console.WriteLine(marketDetail);
