using MarketDomain;
using Moq;

namespace TurtleTests.RecordTests
{
    [TestClass]
    public class RecordProminenceDetailsTests : BaseTest
    {

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
    }

}