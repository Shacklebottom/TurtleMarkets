using MarketSpider;

Schedule schedule = new();
schedule.RunsOnceAMonth();
schedule.RunsOnAMonday();
schedule.RunAfterMarketClose();