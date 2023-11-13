using BusinessLogic;
using BusinessLogic.Logging;
using MarketDomain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TurtleAPI.Exceptions;
using TurtleAPI.FinnhubIO;
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
        Mock<IFinnhubAPI> _mockFinnhubAPI;
        Mock<ILogger> _mockLogger;
        MarketService _service; // UNIT UNDER TEST

        [TestInitialize]
        public void RunBeforeEachTest()
        {
            _mockPreviousCloseRepo = new();
            _mockDividendDetailsRepo = new();
            _mockListedStatusRepo = new();
            _mockFinnhubAPI = new();
            _mockLogger = new();

            _mockListedStatusRepo.Setup(
                m => m.GetAll())
                .Returns(new List<ListedStatus> { new() { Ticker = "MSFT" }, new() { Ticker = "WYNN" } });

            _service = new MarketService(
                _mockPreviousCloseRepo.Object,
                _mockDividendDetailsRepo.Object,
                _mockListedStatusRepo.Object,
                _mockFinnhubAPI.Object,
                _mockLogger.Object);

        }
        #region RecordPreviousClose
        [TestMethod]
        public void RecordPreviousClose_Logs_Start()
        {
            // Arrange
            // nothing to setup

            // Act
            _service.RecordPreviousClose();

            // Assert
            _mockLogger.Verify(
                l => l.Log(It.Is<string>(s => s.StartsWith("Starting"))),
                Times.Once,
                "Logging did not indicate start exactly once");
        }

        [TestMethod]
        public void RecordPreviousClose_lsRepo_GetAll_Once()
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
        public void RecordPreviousClose_GetPreviousClose_ForEachTicker()
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
        public void RecordPreviousClose_SavePreviousClose_ForEachTicker()
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
            // nothing to setup

            // Act
            _service.RecordPreviousClose();

            // Assert
            _mockLogger.Verify(
                l => l.Log(It.Is<string>(s => s.EndsWith("complete."))),
                Times.Once,
                "Logging did not indicate complete exactly once");
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


        #endregion
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}