using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OverlongPathEraser.Tests
{
    [TestClass]
    public class OverlongPathTests
    {
        [TestMethod]
        public void Validate_GivenNonExistingDirectory_ReturnsWithoutSuccess()
        {
            // Arrange
            var path = @"X:\Just\A\Non\Existing\Directory";
            var overlongPath = new OverlongPath(path);

            // Act
            var result = overlongPath.Validate();

            // Assert
            Assert.IsFalse(result.Success);
        }


        [TestMethod]
        public void Validate_GivenExistingHarddriveRoot_ReturnsWithoutSuccess()
        {
            // Arrange
            var path = @"C:\";
            var overlongPath = new OverlongPath(path);

            // Act
            var result = overlongPath.Validate();

            // Assert
            Assert.IsFalse(result.Success);
        }
    }
}
