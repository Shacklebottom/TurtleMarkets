using System;
using BusinessLogic;

namespace MarketSpider
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MarketService _marketService = new();
            switch (args[0])
            {
                case "WeeklyMonday":
                    _marketService.RecordRecommendedTrend();
                    break;
                case "DailyClose":
                    _marketService.RecordPreviousClose();
                    break;
                case "DailyProminence":
                    _marketService.RecordDailyProminence();
                    break;
                case "MonthlyListings":
                    _marketService.RecordListedStatus();
                    break;
                case "MonthlySnapshot":
                    _marketService.RecordSnapshot();
                    break;
                case "YearlyDividends":
                    /*
                     * there are almost 8000 entries, and we get 300 calls an hour, 
                     * so this needs to run a total of 26+ hours.
                     * it's currently set up so it will run for an hour,
                     * therefore sql server agent needs to run it for 26 days in a row 
                     * and it may not be set up proper.
                     */
                    _marketService.RecordDividendDetails();
                    break;
                case "WeeklyTickerDetails":
                    _marketService.RecordTickerDetails();
                    break;
                case "BiannualMarketHoliday":
                    _marketService.RecordMarketHoliday();
                    break;
                default:
                    break;
            }
        }
    }
}