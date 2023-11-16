using MarketDomain;
using Moq;

namespace TurtleTests.RecordTests
{
    [TestClass]
    public class RecordRecommendedTrendTests : BaseTest
    {
        #region RecordRecommendedTrend
        [TestMethod]
        public void RecordRecommendedTrend_Logs_Start()
        {
            // Assign

            // Act
            _service.RecordRecommendedTrend();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.StartsWith("Starting"))),
                Times.Once,
                "Logging did not indicate start exactly once");
        }

        [TestMethod]
        public void RecordRecommendedTrend_Repo_GetAll_Once()
        {
            // Assign

            // Act
            _service.RecordRecommendedTrend();

            // Assert
            _mockTrackedTickerRepo.Verify(ls => ls.GetAll(),
                Times.Once,
                "GetAll() was not called exactly once");
        }

        [TestMethod]
        public void RecordRecommendedTrend_Logs_RecordCount()
        {
            // Assign

            // Act
            _service.RecordRecommendedTrend();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s == "...working on 2 records.")),
                Times.Once,
                "Logging did not track records received");
        }

        [TestMethod]
        public void RecordRecommendedTrend_Get_ForEachTicker()
        {
            // Assign

            // Act
            _service.RecordRecommendedTrend();

            // Assert
            _mockFinnhubAPI.Verify(fh => fh.GetRecommendedTrend("MSFT"),
                Times.Once,
                "GetRecommendedTrend(\"MSFT\") was not called exactly once");
            _mockFinnhubAPI.Verify(fh => fh.GetRecommendedTrend("WYNN"),
                Times.Once,
                "GetRecommendedTrend(\"MSFT\") was not called exactly once");
        }

        [TestMethod]
        public void RecordRecommendedTrend_Logs_QueryTicker()
        {
            // Assign

            // Act
            _service.RecordRecommendedTrend();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.StartsWith("...Querying"))),
                Times.Exactly(2),
                "Logging did not indicate querying exactly twice");
        }

        [TestMethod]
        public void RecordRecommendedTrend_Save_ForEachTicker()
        {
            // Assign

            // Act
            _service.RecordRecommendedTrend();

            // Assert
            _mockRecommendedTrendRepo.Verify(rt => rt.Save(It.IsAny<RecommendedTrend>()),
                Times.Exactly(2),
                "Save was not called exactly twice");
        }

        [TestMethod]
        public void RecordRecommendedTrend_Logs_End()
        {
            // Assign

            // Act
            _service.RecordRecommendedTrend();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.EndsWith("complete."))),
                Times.Once,
                "Logging did not indicate complete exactly once");
        }
        [TestMethod]
        public void RecordRecommendedTrend_Logs_Exception()
        {
            // Assign
            _mockRecommendedTrendRepo.Setup(rt => rt.Save(It.IsAny<RecommendedTrend>())).Throws(new Exception());

            // Act
            _service.RecordRecommendedTrend();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.StartsWith("EXCEPTION:"))),
                Times.Once,
                "Logging did not indicate Exception exactly once");
        }
        #endregion
    }

}