﻿using MarketDomain;
using TurtleAPI;
using TurtleAPI.PolygonIO;
using TurtleAPI.AlphaVantage;
using TurtleAPI.FinnhubIO;
using Microsoft.Identity.Client;


//var Polygon = new PolygonAPI();
//MarketDetail? marketDetail = Polygon.GetPreviousClose("MSFT");
//Console.WriteLine(marketDetail);

var AlphaVantage = new AlphaVantageAPI();
AlphaVantage.GetPreviousClose("MSFT");
