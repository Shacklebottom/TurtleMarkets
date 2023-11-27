using BusinessLogic;
using BusinessLogic.Logging;
using System.Globalization;



namespace MarketSpider
{
   public class Schedule
    {
        private readonly CultureInfo _culture = new("en-US");
        private readonly Calendar _calendar = CultureInfo.CurrentCulture.Calendar;
        private readonly DateTime _today = DateTime.Now;

        private MarketService _marketService = new();
        private ConsoleLogger _logger = new();

        private void log(string message) { _logger.Log(message); }

        public void RunsOnAMonday()
        {
            try
            {
                if (_calendar.GetDayOfWeek(_today) == DayOfWeek.Monday)
                {
                    _marketService.RecordRecommendedTrend();
                }

            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public void RunAfterMarketClose()
        {
            try
            {
                if (
                    _calendar.GetDayOfWeek(_today) == DayOfWeek.Saturday ||
                    _calendar.GetDayOfWeek(_today) == DayOfWeek.Sunday
                    ) { return; }
                DateTime expectedClose = DateTime.ParseExact("5:00 PM", "h:mm tt", _culture);
                DateTime expectedOpen = DateTime.ParseExact("5:00 AM", "h:mm tt", _culture);
                if (_today.TimeOfDay >= expectedClose.TimeOfDay || _today.TimeOfDay <= expectedOpen.TimeOfDay)
                {
                    _marketService.RecordPreviousClose();
                    _marketService.RecordDailyProminence();
                }
            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public void RunsOnceAMonth()
        {
            try
            {
                if (_today.Date.Day == 1)
                {
                    _marketService.RecordSnapshot();
                }
            }
            catch (Exception ex)
            {
                log($"EXCEPTION:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }
    }
}
