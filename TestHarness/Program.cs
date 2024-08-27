using BusinessLogic;
using LoggerModule.DerivedClasses;
using MarketDomain.Objects;
using TurtleAPI.PolygonIO;




#region SHARED WORKSPACE

Console.WriteLine($"-=≡> TURTLE START <≡=-\n\n");

DebugDirectory debugDirectory = new($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}", "MarketDebugLogs");
DebugLogger debugLogger = new(debugDirectory);
MarketServiceLocator serviceLocator = new();
MarketService marketService = new(serviceLocator, debugLogger);
//var polygonAPI = new PolygonAPI(debugLogger);
#endregion

#region ATARI WORKSPACE

//Console.WriteLine(new PolygonAPI(6).GetPreviousClose("MSFT"));


#endregion

#region SHACKLE WORKSPACE

//var x = await polygonAPI.GetTickerDetails("CREX");

//debugLogger.Chat($"{x}");

//await marketService.RecordDividendDetails();
//await marketService.RecordMarketHoliday();
await marketService.RecordTickerDetails();
//await marketService.RecordListedStatus();
//await marketService.RecordDailyProminence();

Console.WriteLine($"\n\n-=≡> TURTLE END <≡=-");

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