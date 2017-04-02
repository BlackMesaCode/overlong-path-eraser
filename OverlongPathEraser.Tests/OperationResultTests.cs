using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace OverlongPathEraser.Tests
{
    [TestClass]
    public class OperationResultTests
    {
        [TestMethod]
        public void Report_GivenAListOfErrors_ShouldReturnAConcatedString()
        {
            // Arrange
            var errors = new List<string>() { "404", "501" };
            var result = new OperationResult(errors);

            // Act
            var report = result.Report();

            // Assert
            Assert.AreEqual(report, "404" + Environment.NewLine + "501" + Environment.NewLine);
        }
    }
}
