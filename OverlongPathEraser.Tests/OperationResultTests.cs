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
            var errors = new List<PathError>() { PathError.IsDriveLetter, PathError.IsNoFolder};
            var result = new OperationResult<PathError>(errors);

            // Act
            var report = result.Report();

            // Assert
            Assert.AreEqual(report, 
                PathError.IsDriveLetter + Environment.NewLine + 
                PathError.IsNoFolder + Environment.NewLine);
        }
    }
}
