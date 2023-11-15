using BusinessLogic;
using BusinessLogic.Logging;
using MarketDomain;
using MarketDomain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.SqlClient;
using System.Security.Authentication;
using TurtleAPI.AlphaVantage;
using TurtleAPI.Exceptions;
using TurtleAPI.FinnhubIO;
using TurtleAPI.PolygonIO;
using TurtleSQL.Interfaces;

namespace TurtleTests
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [TestClass]
    public class MarketServiceTests
    {
        Mock<IRepository<PreviousClose>> _mockPreviousCloseRepo;
        Mock<IRepository<DividendDetails>> _mockDividendDetailsRepo;
        Mock<IRepository<ListedStatus>> _mockListedStatusRepo;
        Mock<IRepository<Prominence>> _mockProminenceRepo;
        Mock<IFinnhubAPI> _mockFinnhubAPI;
        Mock<IPolygonAPI> _mockPolygonAPI;
        Mock<IAlphaVantageAPI> _mockAlphaVantageAPI;
        Mock<ILogger> _mockLogger;
        MarketService _service; // UNIT UNDER TEST

        [TestInitialize]
        public void RunBeforeEachTest()
        {
            _mockPreviousCloseRepo = new();
            _mockDividendDetailsRepo = new();
            _mockListedStatusRepo = new();
            _mockProminenceRepo = new();
            _mockFinnhubAPI = new();
            _mockPolygonAPI = new();
            _mockAlphaVantageAPI = new();
            _mockLogger = new();

            _mockListedStatusRepo.Setup(
                ls => ls.GetAll())
                .Returns(new List<ListedStatus> { new() { Ticker = "MSFT" }, new() { Ticker = "WYNN" } });

            _mockAlphaVantageAPI.Setup(av => av.GetPolarizedMarkets())
                .Returns(new Dictionary<MarketDomain.Enums.PrestigeType, IEnumerable<Prominence?>?>
                {
                    { MarketDomain.Enums.PrestigeType.TopGainer, new List<Prominence>
                    { new() { PrestigeType = "TopGainer" }, new() { PrestigeType = "TopGainer" } } },
                });

            _mockAlphaVantageAPI.Setup(av => av.GetMarketStatus()).Returns(new List<MarketStatus> { new() { Exchange = "NYSE" } });

            _mockAlphaVantageAPI.Setup(av => av.GetListedStatus(ListedStatusTypes.Active)).Returns(new List<ListedStatus> { new() { Ticker = "MSFT" }, new() { Ticker = "AAA" } });


            _service = new MarketService(
                _mockPreviousCloseRepo.Object,
                _mockDividendDetailsRepo.Object,
                _mockListedStatusRepo.Object,
                _mockProminenceRepo.Object,
                _mockFinnhubAPI.Object,
                _mockPolygonAPI.Object,
                _mockAlphaVantageAPI.Object,
                _mockLogger.Object);

        }
        #region RecordPreviousClose
        [TestMethod]
        public void RecordPreviousClose_Logs_Start()
        {
            // Arrange


            // Act
            _service.RecordPreviousClose();

            // Assert
            _mockLogger.Verify(
                l => l.Log(It.Is<string>(s => s.StartsWith("Starting"))),
                Times.Once,
                "Logging did not indicate start exactly once");
        }

        [TestMethod]
        public void RecordPreviousClose_Repo_GetAll_Once()
        {
            // Arrange


            // Act
            _service.RecordPreviousClose();

            // Assert
            _mockListedStatusRepo.Verify(
                g => g.GetAll(),
                Times.Once,
                "GetAll() was not called exactly once");
        }

        [TestMethod]
        public void RecordPreviousClose_Logs_RecordCount()
        {
            // Arrange


            // Act
            _service.RecordPreviousClose();

            // Assert
            _mockLogger.Verify(
                l => l.Log(It.Is<string>(s => s == "...working on 2 records.")),
                Times.Once,
                "Logging did not track records received");
        }

        [TestMethod]
        public void RecordPreviousClose_Get_ForEachTicker()
        {
            // Arrange

            // Act
            _service.RecordPreviousClose();

            // Assert
            _mockFinnhubAPI.Verify(
                a => a.GetPreviousClose("MSFT"),
                Times.Once,
                "GetPreviousClose(\"MSFT\") was not called exactly once");
            _mockFinnhubAPI.Verify(
                b => b.GetPreviousClose("WYNN"),
                Times.Once,
                "GetPreviousClose(\"WYNN\") was not called exactly once");
        }

        [TestMethod]
        public void RecordPreviousClose_Logs_QueryTicker()
        {
            // Arrange


            // Act
            _service.RecordPreviousClose();

            // Assert
            _mockLogger.Verify(
                l => l.Log(It.Is<string>(s => s.StartsWith("...Querying"))),
                Times.Exactly(2),
                "Logging did not indicate querying exactly twice");
        }

        [TestMethod]
        public void RecordPreviousClose_Save_ForEachTicker()
        {
            // Arrange

            // Act
            _service.RecordPreviousClose();

            // Assert
            _mockPreviousCloseRepo.Verify(
                p => p.Save(It.IsAny<PreviousClose>()),
                Times.Exactly(2),
                "Save was not called exactly twice :(");
        }

        [TestMethod]
        public void RecordPreviousClose_Logs_Stop()
        {
            // Arrange

            // Act
            _service.RecordPreviousClose();

            // Assert
            _mockLogger.Verify(
                l => l.Log(It.Is<string>(s => s.EndsWith("complete."))),
                Times.Once,
                "Logging did not indicate complete exactly once");
        }

        [TestMethod]
        public void RecordPreviousClose_Logs_Exception()
        {
            // Arrange
            _mockPreviousCloseRepo.Setup(pc => pc.Save(It.IsAny<PreviousClose>())).Throws(new Exception());

            // Act
            _service.RecordPreviousClose();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.StartsWith("EXCEPTION:"))),
                Times.Once,
                "Logger did not indicate EXCEPTION once");
        }
        #endregion

        #region RecordDividendDetails
        [TestMethod]
        public void RecordDividendDetails_Logs_Start()
        {
            // Arrange

            // Act
            _service.RecordDividendDetails();

            // Assert
            _mockLogger.Verify(
                l => l.Log(It.Is<string>(s => s.StartsWith("Starting"))),
                Times.Once,
                "Logging did not indicate start exactly once");
        }

        [TestMethod]
        public void RecordDividendDetails_Repo_GetAll_Once()
        {
            // Arrange

            // Act
            _service.RecordDividendDetails();

            // Assert
            _mockListedStatusRepo.Verify(
                g => g.GetAll(),
                Times.Once,
                "GetAll() was not called exactly once");
        }

        [TestMethod]
        public void RecordDividendDetails_Logs_RecordCount()
        {
            // Arrange

            // Act
            _service.RecordDividendDetails();

            // Assert

            _mockLogger.Verify(
                l => l.Log(It.Is<string>(s => s == "...working on 2 records.")),
                Times.Once,
                "Logging did not track records received");
        }

        [TestMethod]
        public void RecordDividendDetails_Logs_QueryTicker()
        {
            // Arrange

            // Act
            _service.RecordDividendDetails();
            // Assert
            _mockLogger.Verify(
                l => l.Log(It.Is<string>(s => s.StartsWith("...Querying"))),
                Times.Exactly(2),
                "Logging did not indicate querying exactly twice");
        }

        [TestMethod]
        public void RecordDividendDetails_Get_ForEachTicker()
        {
            // Arrange

            // Act
            _service.RecordDividendDetails();

            // Assert
            _mockPolygonAPI.Verify(
                a => a.GetDividendDetails("MSFT"),
                Times.Once,
                "GetDividendDetails(\"MSFT\") was not called exactly once");
            _mockPolygonAPI.Verify(
                b => b.GetDividendDetails("WYNN"),
                Times.Once,
                "GetDividendDetails(\"WYNN\") was not called exactly once");
        }

        [TestMethod]
        public void RecordDividendDetails_Save_ForEachDetail()
        {
            // Arrange
            _mockPolygonAPI.Setup(
                pg => pg.GetDividendDetails("MSFT"))
                .Returns(new List<DividendDetails> { new() { Ticker = "MSFT" }, new() { Ticker = "MSFT" } });

            // Act
            _service.RecordDividendDetails();

            // Assert
            _mockDividendDetailsRepo.Verify(s => s.Save(It.IsAny<DividendDetails>()),
                Times.Exactly(2),
                "Save was not called exactly twice :(");

        }

        [TestMethod]
        public void RecordDividendDetails_Logs_Stop()
        {
            // Arrange

            // Act
            _service.RecordDividendDetails();

            // Assert
            _mockLogger.Verify(
                l => l.Log(It.Is<string>(s => s.EndsWith("complete."))),
                Times.Once,
                "Logging did not indicate complete exactly once");
        }

        [TestMethod]
        public void RecordDividendDetails_Logs_Exception()
        {
            // Arrange
            _mockDividendDetailsRepo.Setup(dd => dd.Save(It.IsAny<DividendDetails>())).Throws(new Exception());
            _mockPolygonAPI.Setup(pg => pg.GetDividendDetails("MSFT"))
                .Returns(new List<DividendDetails> { new() });

            // Act
            _service.RecordDividendDetails();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.StartsWith("EXCEPTION:"))),
                Times.Once,
                "Logger did not indicate EXCEPTION once");
        }
        #endregion

        #region RecordProminenceDetails

        [TestMethod]
        public void RecordDailyProminence_Logs_Start()
        {
            // Arrange


            // Act
            _service.RecordDailyProminence();

            // Assert
            _mockLogger.Verify(
                l => l.Log(It.Is<string>(s => s.StartsWith("Starting"))),
                Times.Once,
                "Logging did not indicate start exactly once");
        }

        [TestMethod]
        public void RecordDailyProminence_Get_Once()
        {
            // Arrange

            // Act
            _service.RecordDailyProminence();

            // Assert
            _mockAlphaVantageAPI.Verify(
                g => g.GetPolarizedMarkets(),
                Times.Once,
                "GetPolarizedmarkets() was not called exactly once");
        }

        [TestMethod]
        public void RecordDailyProminence_Save_ForEachDetail()
        {
            // Arrange

            // Act
            _service.RecordDailyProminence();

            // Assert
            _mockProminenceRepo.Verify(s => s.Save(It.IsAny<Prominence>()),
                Times.Exactly(2),
                "Save was not called exactly twice :(");
        }

        [TestMethod]
        public void RecordDailyProminence_Logs_Stop()
        {
            // Arrange

            // Act
            _service.RecordDailyProminence();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.EndsWith("complete."))),
                Times.Once,
                "Logging did not indicate complete once");
        }

        [TestMethod]
        public void RecordDailyProminence_Logs_Exception()
        {
            // Arrange
            _mockProminenceRepo.Setup(dd => dd.Save(It.IsAny<Prominence>())).Throws(new Exception());

            // Act
            _service.RecordDailyProminence();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.StartsWith("EXCEPTION:"))),
                Times.Once,
                "Logger did not indicate EXCEPTION once");
        }
        #endregion

        #region CheckMarketStatus
        [TestMethod]
        public void CheckMarketStatus_Logs_Start()
        {
            // Assign

            // Act
            _service.CheckMarketStatus("NYSE");

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.StartsWith("Starting"))),
                Times.Once,
                "Logging did not indicate start once");
        }
        [TestMethod]
        public void CheckMarketStatus_Get_Once()
        {
            // Assign

            // Act
            _service.CheckMarketStatus("NYSE");

            // Assert
            _mockAlphaVantageAPI.Verify(av => av.GetMarketStatus(),
                Times.Once,
                "GetMarketStatus() was not called exactly once");
        }

        [TestMethod]
        public void CheckMarketStatus_Logs_Stop()
        {
            // Assign

            // Act
            _service.CheckMarketStatus("NYSE");

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.EndsWith("complete."))),
                Times.Once,
                "Logging did not indicate complete once");
        }

        [TestMethod]
        public void CheckMarketStatus_Return_Value()
        {
            // Assign

            // Act
            var result = _service.CheckMarketStatus("NYSE");

            // Assert
            Assert.AreEqual("NYSE", result?.Exchange, "CheckMarketStatus did not return the expected value");
        }

        [TestMethod]
        public void CheckMarketStatus_Logs_Exception()
        {
            // Assign
            _mockAlphaVantageAPI.Setup(av => av.GetMarketStatus()).Throws(new Exception());
            // Act
            _service.CheckMarketStatus("NYSE");

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.StartsWith("EXCEPTION:"))),
                Times.Once,
                "Logging did not indicate Exception once");
        }
        #endregion

        #region RecordListedStatus
        [TestMethod]
        public void RecordListedStatus_Logs_Start()
        {
            // Assign

            // Act
            _service.RecordListedStatus();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.StartsWith("Starting"))),
                Times.Once,
                "Logging did not indicate start exactly once");
        }

        [TestMethod]
        public void RecordListedStatus_Get_Once()
        {
            // Assign

            // Act
            _service.RecordListedStatus();

            // Assert
            _mockAlphaVantageAPI.Verify(av => av.GetListedStatus(ListedStatusTypes.Active),
                Times.Once,
                "GetListedStatus was not called exactly once");
        }

        [TestMethod]
        public void RecordListedStatus_Save_ForEachDetail()
        {
            // Assign

            // Act
            _service.RecordListedStatus();

            // Assert
            _mockListedStatusRepo.Verify(ls => ls.Save(It.IsAny<ListedStatus>()),
                Times.Exactly(2),
                "Save was not called exactly twice :(");
        }

        [TestMethod]
        public void RecordListedStatus_Logs_Stop()
        {
            // Assign

            // Act
            _service.RecordListedStatus();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.EndsWith("complete."))),
                Times.Once,
                "Logging did not indicate complete once");
        }

        [TestMethod]
        public void RecordListedStatus_Logs_Exception()
        {
            // Assign
            _mockAlphaVantageAPI.Setup(av => av.GetListedStatus(ListedStatusTypes.Active)).Throws(new Exception());
            // Act
            _service.RecordListedStatus();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.StartsWith("EXCEPTION:"))),
                Times.Once,
                "Logging did not indicate Exception once");
        }


        #endregion
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}