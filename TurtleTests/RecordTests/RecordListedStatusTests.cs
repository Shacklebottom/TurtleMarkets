using MarketDomain;
using MarketDomain.Enums;
using Moq;

namespace TurtleTests.RecordTests
{
    [TestClass]
    public class RecordListedStatusTests : BaseTest
    {

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

}