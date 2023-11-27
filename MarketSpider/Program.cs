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