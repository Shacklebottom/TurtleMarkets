using BusinessLogic;
using LoggerModule.DerivedClasses;
using TurtleAPI.PolygonIO;




#region SHARED WORKSPACE

Console.WriteLine($"-=≡> TURTLE START <≡=-\n\n");
var marketService = new MarketService();

var debugDir = new DebugDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}", "TrutleHarnss");
var debugLogger = new DebugLogger(debugDir);
var polyAPI = new PolygonAPI(debugLogger);

#endregion

#region ATARI WORKSPACE

//Console.WriteLine(new PolygonAPI(6).GetPreviousClose("MSFT"));


#endregion

#region SHACKLE WORKSPACE

await marketService.RecordMarketHoliday();

Console.WriteLine($"-=≡> TURTLE END <≡=-");

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

#endregion