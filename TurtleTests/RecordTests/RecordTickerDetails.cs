using MarketDomain;
using Moq;

namespace TurtleTests.RecordTests
{
    [TestClass]
    public class RecordTickerDetails : BaseTest
    {

        #region RecordTickerDetails
        [TestMethod]
        public void RecordTickerDetails_Logs_Start()
        {
            // Assign

            // Act
            _service.RecordTickerDetails();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.StartsWith("Starting"))),
                Times.Once,
                "Logging did not indicate start exactly once");
        }
        [TestMethod]
        public void RecordTickerDetails_Repo_GetAll_Once()
        {
            // Assign

            // Act
            _service.RecordTickerDetails();

            // Assert
            _mockTrackedTickerRepo.Verify(ls => ls.GetAll(),
                Times.Once,
                "GetAll() was not called exactly once");
        }

        [TestMethod]
        public void RecordTickerDetails_Logs_RecordCount()
        {
            // Assign

            // Act
            _service.RecordTickerDetails();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.StartsWith("...working on 2 records"))),
                Times.Once,
                "Logging did not track records received");
        }

        [TestMethod]
        public void RecordTickerDetails_Logs_QueryTicker()
        {
            // Assign

            // Act
            _service.RecordTickerDetails();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.StartsWith("...Querying"))),
                Times.Exactly(2),
                "Logging did not indicate querying exactly twice");

        }

        [TestMethod]
        public void RecordTickerDetails_Get_ForEachTicker()
        {
            // Assign

            // Act
            _service.RecordTickerDetails();

            // Assert
            _mockPolygonAPI.Verify(pg => pg.GetTickerDetails("MSFT"),
                Times.Once,
                "GetTickerDetails(\"MSFT\") was not called exactly once");
        }

        [TestMethod]
        public void RecordTickerDetails_Save_ForEachDetail()
        {
            // Assign

            // Act
            _service.RecordTickerDetails();

            // Assert
            _mockTickerDetailRepo.Verify(td => td.Save(It.IsAny<TickerDetail>()),
                Times.Exactly(2),
                "Save was not called exactly twice :(");
        }


        [TestMethod]
        public void RecordTickerDetails_Logs_Stop()
        {
            // Assign

            // Act
            _service.RecordTickerDetails();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.EndsWith("complete."))),
                Times.Once,
                "Logger did not indicate complete exactly once");
        }

        [TestMethod]
        public void RecordTickerDetails_Logs_Exception()
        {
            // Assign
            _mockTickerDetailRepo.Setup(td => td.Save(It.IsAny<TickerDetail>())).Throws(new Exception());

            // Act
            _service.RecordTickerDetails();

            // Assert
            _mockLogger.Verify(l => l.Log(It.Is<string>(s => s.StartsWith("EXCEPTION:"))),
                Times.Once,
                "Logging did not indicate Exception once");
        }
        #endregion
    }

}