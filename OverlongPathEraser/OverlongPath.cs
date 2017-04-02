using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OverlongPathEraser
{
    /// <summary>
    /// OverlongPath represents a path in the filesystem on which we can perform recursive deletion even if the path should exceed windows 256 chars limit
    /// </summary>
    public class OverlongPath
    {
        
        public OverlongPath(string path)
        {
            this.FullPath = path;
        }

        public OperationResult Validate()
        {
            var errors = new List<string>();

            if (!IsValidPath(FullPath))
            {
                errors.Add("Invalid argument: path to delete does not exist");
            }
            else // we need to ensure FullPath is valid before executing further validations
            {
                if (!PathIsFolder(FullPath))
                    errors.Add("Invalid argument: path needs to point to a folder");

                if (FolderIsRootFolder(FullPath))
                    errors.Add("Invalid argument: path must not be a root folder / drive letter");
            }

            if (errors.Any())
                return new OperationResult(errors);

            return new OperationResult(successMessage: "Path is valid");
        }


        public OperationResult Erase()
        {
            var operationResult = Validate();
            if (operationResult.Success)
            {
                var folderToDelete = new DirectoryInfo(FullPath);

                // we need this empty folder so that robocopy can sync with that empty folder which will eventually delete our folder contents
                var emptyTempDirectory = CreateTempDirectory();

                // configure the robocopy process
                var arguments = string.Format("\"{0}\" \"{1}\" /MIR", emptyTempDirectory.FullName, folderToDelete.FullName); // if /MIR is not working, try /purge ??
                var processStartInfo = new ProcessStartInfo("Robocopy32")
                {
                    Arguments = arguments,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WorkingDirectory = Environment.CurrentDirectory
                };

                // start robocopy process and perform deletion while showing a spinner
                using (var process = Process.Start(processStartInfo))
                {
                    var spinner = new Spinner();
                    spinner.Start();
                    process.WaitForExit();
                    spinner.Stop();
                }

                // cleanup
                folderToDelete.Delete();
                emptyTempDirectory.Delete();
            }
            return operationResult;
        }

        private static DirectoryInfo CreateTempDirectory()
        {
            var tempDirectory = new DirectoryInfo(
                Path.Combine(Path.GetTempPath(), "OverlongPathEraserHelperDirectory"));

            if (!Directory.Exists(tempDirectory.FullName))
                tempDirectory.Create();

            return tempDirectory;
        }


        public bool IsValidPath(string path)
        {
            if (File.Exists(path) || Directory.Exists(path))
                return true;
            return false;
        }


        public bool PathIsFolder(string path)
        {
            FileAttributes pathAttributes = File.GetAttributes(path);
            if (pathAttributes.HasFlag(FileAttributes.Directory))
                return true;
            return false;
        }


        public bool FolderIsRootFolder(string path)
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            if (folder.Parent == null || (Directory.GetDirectoryRoot(path) == path) || Regex.IsMatch(path, "^[A-Za-z]:$"))
                return true;
            return false;
        }


        public string FullPath { get; private set; }
    }
}
