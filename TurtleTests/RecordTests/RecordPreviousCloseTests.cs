using BusinessLogic;
using BusinessLogic.Logging;
using MarketDomain;
using MarketDomain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Security.Authentication;
using TurtleAPI.AlphaVantage;
using TurtleAPI.Exceptions;
using TurtleAPI.FinnhubIO;
using TurtleAPI.PolygonIO;
using TurtleSQL.Interfaces;

namespace TurtleTests.RecordTests
{
    [TestClass]
    public class RecordPreviousCloseTests : BaseTest
    {
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
            _mockTrackedTickerRepo.Verify(
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
    }
}