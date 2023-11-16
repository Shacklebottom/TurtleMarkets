using MarketDomain;
using Moq;

namespace TurtleTests.RecordTests
{
    [TestClass]
    public class RecordMarketHolidayTests : BaseTest
    {
        #region RecordMarketHoliday()

        [TestMethod]
        public void RecordMarketHoliday_Logs_Start()
        {
            // Arrange

            // Act
            _service.RecordMarketHoliday();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.StartsWith("Starting"))),
                Times.Once,
                "Logging did not indicate start exactly once");
        }

        [TestMethod]
        public void RecordMarketHoliday_Get_Once()
        {
            // Arrange

            // Act
            _service.RecordMarketHoliday();

            // Assert
            _mockPolygonAPI.Verify(pg => pg.GetMarketHoliday(),
                Times.Once,
                "GetMarketHoliday() was not called exactly once");
        }

        [TestMethod]
        public void RecordMarketHoliday_Save_ForEachDetail()
        {
            // Arrange

            // Act
            _service.RecordMarketHoliday();

            // Assert
            _mockMarketHolidayRepo.Verify(mh => mh.Save(It.IsAny<MarketHoliday>()),
                Times.Exactly(2),
                "Save was not called exactly twice :(");
        }

        [TestMethod]
        public void RecordMarketHoliday_Logs_Stop()
        {
            // Arrange

            // Act
            _service.RecordMarketHoliday();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.EndsWith("complete."))),
                Times.Once,
                "Logging did not indicate complete once");
        }

        [TestMethod]
        public void RecordMarketHoliday_Logs_Exception()
        {
            // Arrange
            _mockMarketHolidayRepo.Setup(mh => mh.Save(It.IsAny<MarketHoliday>())).Throws(new Exception());

            // Act
            _service.RecordMarketHoliday();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.StartsWith("EXCEPTION:"))),
                Times.Once,
                "Logging did not indicate exception once");
        }

        #endregion
    }

}