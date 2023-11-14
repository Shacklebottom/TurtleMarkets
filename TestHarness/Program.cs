﻿using MarketDomain;
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

Console.WriteLine($"-=≡> TURTLE START <≡=-\n\n");

#region ATARI WORKSPACE

Console.WriteLine(new PolygonAPI(6).GetPreviousClose("MSFT"));


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