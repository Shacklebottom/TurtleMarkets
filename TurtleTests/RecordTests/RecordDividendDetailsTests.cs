using MarketDomain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TurtleTests.RecordTests
{
    [TestClass]
    public class RecordDividendDetailsTests : BaseTest
    {
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
    }

}