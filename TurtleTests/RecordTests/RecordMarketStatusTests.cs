using MarketDomain;
using Moq;

namespace TurtleTests.RecordTests
{
    [TestClass]
    public class RecordMarketStatusTests : BaseTest
    {

        #region RecordMarketStatus
        [TestMethod]
        public void RecordMarketStatus_Logs_Start()
        {
            // Assign

            // Act
            _service.RecordMarketStatus();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.StartsWith("Starting"))),
                Times.Once,
                "Logging did not indicate start once");
        }
        [TestMethod]
        public void RecordMarketStatus_Get_Once()
        {
            // Assign

            // Act
            _service.RecordMarketStatus();

            // Assert
            _mockAlphaVantageAPI.Verify(av => av.GetMarketStatus(),
                Times.Once,
                "GetMarketStatus() was not called exactly once");
        }

        [TestMethod]
        public void RecordMarketStatus_Save_ForAllDetails()
        {
            // Assign
            _mockAlphaVantageAPI.Setup(av => av.GetMarketStatus())
                .Returns(new List<MarketStatus> { new() { Exchange = "NYSE" }, new() { Exchange = "NASDAQ" } });
            // Act
            _service.RecordMarketStatus();

            // Assert
            _mockMarketStatusRepo.Verify(ms => ms.Save(It.IsAny<MarketStatus>()),
                Times.Exactly(2),
                "Save was not called exactly twice :(");
        }

        [TestMethod]
        public void RecordMarketStatus_Logs_Stop()
        {
            // Assign

            // Act
            _service.RecordMarketStatus();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.EndsWith("complete."))),
                Times.Once,
                "Logging did not indicate complete once");
        }



        [TestMethod]
        public void RecordMarketStatus_Logs_Exception()
        {
            // Assign
            _mockAlphaVantageAPI.Setup(av => av.GetMarketStatus()).Throws(new Exception());
            // Act
            _service.RecordMarketStatus();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.StartsWith("EXCEPTION:"))),
                Times.Once,
                "Logging did not indicate Exception once");
        }
        #endregion
    }

}