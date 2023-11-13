using MarketDomain;
using TurtleAPI;
using TurtleAPI.PolygonIO;
using TurtleAPI.AlphaVantage;
using TurtleAPI.FinnhubIO;
using CsvHelper;
using System.Globalization;
using System.Net;
using TestHarness;
using MarketDomain.Enums;
using MarketDomain.Interfaces;
using System.Linq.Expressions;
using TurtleAPI.Exceptions;
using System.Diagnostics;
using TurtleSQL.TickerRepositories;
using BusinessLogic;
using BusinessLogic.Logging;

int x = 0;
#region ATARI WORKSPACE

//var dbLogger = new DatabaseLogger();
//var markets = new MarketService(logger: dbLogger);
//markets.RecordDividendDetails();

#endregion

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

#endregion