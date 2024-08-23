using System;
using BusinessLogic;
using LoggerModule.DerivedClasses;
using MarketDomain.Objects;

namespace MarketSpider
{
    public class Program
    {
        /* INTENDED WORKFLOW
         * Once a day: PreviousClose recorded (~2h 20m worth of calls), DailyProminence recorded (a single call).
         * Once a week: RecommendedTrend recorded (~2h 20m worth of calls, run on a weekend to avoid layering on top of RecordPreviousClose (they are the same API)).
         * Once a month: a new instance of ListedStatus is recorded (a single call), TickerDetails of any new entries to the exchange are recorded (makes as many calls as it needs to (300 an hour)).
         * Twice a year: a new instance of MarketHoliday is recorded (a single call).
         * Once a year: DividendDetails recorded (2h worth of calls for 13 days).
         * Fresh sql server: ListedStatus and MarketHoliday once, TickerDetails & DividendDetails: one and then the other, over 26 days.
         */
        public static async Task Main(string[] args)
        {
            DebugDirectory debugDirectory = new($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}", "MarketDebugLogs");
            DebugLogger debugLogger = new(debugDirectory);
            MarketServiceLocator serviceLocator = new();
            MarketService marketService = new(serviceLocator, debugLogger);

            switch (args[0])
            {
                /* IMPORTANT! POLYGON API NOTE!
                 * there are almost 8000 entries, and we get 300 calls an hour, 
                 * so this needs to run a total of 26+ hours.
                 * it's currently set up so it will run for 2 hours at a time,
                 * therefore sql server agent needs to run it for 13 days in a row 
                 */

                case "DailyClose":
                    //this records a PreviousClose snapshot of only american _stock_ exchanges.
                    await marketService.RecordPreviousClose(); //finnhub
                    break;

                case "DailyProminence":
                    //this records the top 20: gainers, losers, and most traded.
                    await marketService.RecordDailyProminence(); //alphavantage
                    break;

                case "WeeklyTrend":
                    //this records last week's Recommended Action of each stock in only american _stock_ exchanges.
                    await marketService.RecordRecommendedTrend(); //finnhub
                    break;

                case "MonthlyListings":
                    //this records all _active_ publicly traded companies.
                    await marketService.RecordListedStatus(); //alphavantage
                    break;

                case "MonthlySnapshot":
                    //this records a PreviousClose snapshot of the _entire_ market. We prob dont need this.
                    //Shackle to Shackle: if we don't need this then why does it exist?
                    await marketService.RecordSnapshot(); //finnhub 
                    break;

                case "YearlyDividends":
                    //!!Read polygon note (above).
                    //this records a DividendDetails forecast of only american _stock_ exchanges
                    await marketService.RecordDividendDetails(); //polygon
                    break;

                case "MonthlyTickerDetails":
                    //!!Read polygon note (above).
                    //For this in particular: once we get a TickerDetails snapshot,
                        //items that get appended to it will become increasingly few.
                    await marketService.RecordTickerDetails(); //polygon
                    break;

                case "BiannualMarketHoliday":
                    //this makes a single call to record an instance of the Market Holiday forecast.
                    await marketService.RecordMarketHoliday(); //polygon
                    break;

                default:
                    break;
            }
        }
    }
}