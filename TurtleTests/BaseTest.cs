using BusinessLogic;
using LoggerModule;
using LoggerModule.Interfaces;
using MarketDomain;
using MarketDomain.Enums;
using Moq;
using TurtleAPI.AlphaVantage;
using TurtleAPI.FinnhubIO;
using TurtleAPI.PolygonIO;
using TurtleSQL.Interfaces;

namespace TurtleTests
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [TestClass]
    public class BaseTest
    {
        protected Mock<ILogger> _mockLogger;
        protected Mock<IAlphaVantageAPI> _mockAlphaVantageAPI;
        protected Mock<IPolygonAPI> _mockPolygonAPI;
        protected Mock<IFinnhubAPI> _mockFinnhubAPI;
        protected Mock<IRepository<DividendDetails>> _mockDividendDetailsRepo;
        protected Mock<IRepository<ListedStatus>> _mockListedStatusRepo;
        protected Mock<IRepository<MarketHoliday>> _mockMarketHolidayRepo;
        protected Mock<IRepository<MarketStatus>> _mockMarketStatusRepo;
        protected Mock<IRepository<PreviousClose>> _mockPreviousCloseRepo;
        protected Mock<IRepository<Prominence>> _mockProminenceRepo;
        protected Mock<IRepository<RecommendedTrend>> _mockRecommendedTrendRepo;
        protected Mock<IRepository<TickerDetail>> _mockTickerDetailRepo;
        protected Mock<IRepository<TrackedTicker>> _mockTrackedTickerRepo;
        protected Mock<IRepository<PreviousClose>> _mockSnapshotRepo;

        protected MarketService _service; // UNIT UNDER TEST
        
        [TestInitialize]
        public void RunBeforeEachTest()
        {
            _mockPreviousCloseRepo = new();
            _mockDividendDetailsRepo = new();
            _mockListedStatusRepo = new();
            _mockProminenceRepo = new();
            _mockRecommendedTrendRepo = new();
            _mockTickerDetailRepo = new();
            _mockMarketHolidayRepo = new();
            _mockMarketStatusRepo = new();
            _mockTrackedTickerRepo = new();
            _mockSnapshotRepo = new();
            _mockFinnhubAPI = new();
            _mockPolygonAPI = new();
            _mockAlphaVantageAPI = new();
            _mockLogger = new();

            #region TEST ARRANGE STATEMENTS
            _mockTrackedTickerRepo.Setup(
                ls => ls.GetAll())
                .Returns(new List<TrackedTicker> { new() { Ticker = "MSFT" }, new() { Ticker = "WYNN" } });

            _mockListedStatusRepo.Setup(ls => ls.GetAll())
                .Returns(new List<ListedStatus> 
                { 
                    new() { Ticker = "MSFT", Exchange = "NYSE", Type = "Stock" },
                    new() { Ticker = "WYNN", Exchange = "NASDAQ", Type = "Stock" }
                });

            _mockAlphaVantageAPI.Setup(av => av.GetPolarizedMarkets())
                .Returns(new Dictionary<PrestigeType, IEnumerable<Prominence>>
                {
                    { PrestigeType.TopGainer, new List<Prominence>
                    { new() { PrestigeType = "TopGainer" }, new() { PrestigeType = "TopGainer" } } },
                });

            _mockAlphaVantageAPI.Setup(av => av.GetMarketStatus()).Returns(new List<MarketStatus> { new() { Exchange = "NYSE" } });

            _mockAlphaVantageAPI.Setup(av => av.GetListedStatus(ListedStatusTypes.Active)).Returns(new List<ListedStatus> { new() { Ticker = "MSFT" }, new() { Ticker = "AAA" } });

            _mockFinnhubAPI.Setup(fh => fh.GetRecommendedTrend("MSFT"))
                .Returns(new List<RecommendedTrend> { new() { Ticker = "MSFT" }, new() { Ticker = "MSFT" } });

            _mockPolygonAPI.Setup(pg => pg.GetMarketHoliday())
                .Returns(new List<MarketHoliday> { new() { Exchange = "NYSE" }, new() { Exchange = "NASDAQ" } });

            #endregion

            _service = new MarketService(
                _mockPreviousCloseRepo.Object,
                _mockDividendDetailsRepo.Object,
                _mockListedStatusRepo.Object,
                _mockProminenceRepo.Object,
                _mockRecommendedTrendRepo.Object,
                _mockTickerDetailRepo.Object,
                _mockMarketHolidayRepo.Object,
                _mockMarketStatusRepo.Object,
                _mockTrackedTickerRepo.Object,
                _mockSnapshotRepo.Object,
                _mockFinnhubAPI.Object,
                _mockPolygonAPI.Object,
                _mockAlphaVantageAPI.Object,
                _mockLogger.Object);

        }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
