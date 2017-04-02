using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace OverlongPathEraser.Tests
{
    [TestClass]
    public class OverlongPathTests
    {
        [TestMethod]
        public void Validate_GivenNonExistingDirectory_ReturnsWithError()
        {
            // Arrange
            var path = @"X:\Just\A\Non\Existing\Directory";
            var overlongPath = new OverlongPath(path);

            // Act
            var result = overlongPath.Validate();

            // Assert
            Assert.IsTrue(result.Errors.Contains(PathError.NotExisting));
        }


        [TestMethod]
        public void Validate_GivenExistingHarddriveRoot_ReturnsWithError()
        {
            // Arrange
            var path = @"C:\";
            var overlongPath = new OverlongPath(path);

            // Act
            var result = overlongPath.Validate();

            // Assert
            Assert.IsTrue(result.Errors.Contains(PathError.IsDriveLetter));
        }


        [TestMethod]
        public void Validate_GivenNoValidFolder_ReturnsWithError()
        {
            // Arrange
            var path = Path.GetTempFileName(); // we are doing an integration test here instead of mocking the the folder validation
            var overlongPath = new OverlongPath(path);

            // Act
            var result = overlongPath.Validate();

            // Assert
            Assert.IsTrue(result.Errors.Contains(PathError.IsNoFolder));

            // Cleanup
            File.Delete(path);
        }


        [TestMethod]
        public void Erase_GivenAValidedPath_ReturnsWithSuccess()
        {
            // Arrange
            // we are doing an integration test here instead of mocking file system calls
            var path = Path.Combine(Path.GetTempPath(), "OverlongPathEraserTestFolder");
            Directory.CreateDirectory(path);
            var overlongPath = new OverlongPath(path);

            // Act
            var result = overlongPath.Erase();

            // Assert
            Assert.IsTrue(result.Success);

            // Cleanup (in case Erase() failed)
            if (Directory.Exists(path))
                File.Delete(path);
        }


        [TestMethod]
        public void Erase_GivenAValidedPath_DeletesPath()
        {
            // Arrange
            // we are doing an integration test here instead of mocking file system calls
            var path = Path.Combine(Path.GetTempPath(), "OverlongPathEraserTestFolder");
            Directory.CreateDirectory(path);
            var overlongPath = new OverlongPath(path);

            // Act
            var result = overlongPath.Erase();

            // Assert
            Assert.IsFalse(Directory.Exists(path));

            // Cleanup (in case Erase() failed)
            if (Directory.Exists(path))
                File.Delete(path);
        }


        [TestMethod]
        public void Erase_GivenAnInvalidPath_ReturnsWithoutSuccess()
        {
            // Arrange
            var path = @"X:\Just\A\Non\Existing\Directory";
            var overlongPath = new OverlongPath(path);

            // Act
            var result = overlongPath.Erase();

            // Assert
            Assert.IsFalse(result.Success);
        }

    }
}
