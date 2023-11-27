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
using TurtleAPI.BaseClasses;
using MarketDomain.Extensions;
using System.Text.RegularExpressions;

#region SHARED WORKSPACE
Console.WriteLine($"-=≡> TURTLE START <≡=-\n\n");
var marketService = new MarketService();
#endregion

#region ATARI WORKSPACE

//Console.WriteLine(new PolygonAPI(6).GetPreviousClose("MSFT"));


#endregion

#region SHACKLE WORKSPACE
//var x = new ListedStatusRepository();
//var y = new SnapshotRepository();
//var nasdaq = x.GetAll().Where(x => x.Exchange == "NASDAQ").ToList();
//var underhundred = marketService.GetFilteredTickers(100, 50).ToList();
//var filteredList = new List<PreviousClose>();
//foreach (var uf in underhundred)
//{
//	foreach (var nsdq in nasdaq)
//	{
//		if (uf.Ticker == nsdq.Ticker)
//		{
//			filteredList.Add(uf);
//		}
//	}
//}
//Console.WriteLine($"under fifty has {underhundred.Count} entries");
//Console.WriteLine($"NASDAQ has {nasdaq.Count} entries");
//Console.WriteLine($"list has {filteredList.Count} entries");

//marketService.RecordDividendDetails();
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