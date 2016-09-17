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
    class Program
    {
        static void Main(string[] args)
        {
            var proceed = true;

            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Missing argument: path to delete");
                proceed = false;
            }

            Console.WriteLine("Passed arguments:");
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine("\t" + args[i]);
            }

            if (args.Length > 1)
            {
                Console.WriteLine("Too many arguments: please pass only one path to delete");
                proceed = false;
            }

            if (!IsValidPath(args[0]))
            {
                Console.WriteLine("Invalid argument: path to delete does not exist");
                proceed = false;
            }

            if (!PathIsFolder(args[0]))
            {
                Console.WriteLine("Invalid argument: path needs to point to a folder");
                proceed = false;
            }

            if (FolderIsRootFolder(args[0]))
            {
                Console.WriteLine("Invalid argument: path must not be a root folder / drive letter");
                proceed = false;
            }

            if (proceed)
            {
                Console.Write("If you want to proceed type 'DELETE' without quotes and hit 'Enter':  ");
                var userResponse = Console.ReadLine();

                if (userResponse == "DELETE")
                {
                    Console.WriteLine("Deletion successfull!");
                    DeleteDirectory(args[0]);
                }
                else
                {
                    Console.Write("Deletion aborted!");
                }
            }

            Console.WriteLine("Press 'Enter' to close...");
            Console.ReadLine();

        }

        

        public static bool IsValidPath(string path)
        {
            if (File.Exists(path) || Directory.Exists(path))
                return true;
            return false;
        }


        public static bool PathIsFolder(string path)
        {
            FileAttributes pathAttributes = File.GetAttributes(path);
            if (pathAttributes.HasFlag(FileAttributes.Directory))
                return true;
            return false;
        }

        public static bool FolderIsRootFolder(string path)
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            if (folder.Parent == null || (Directory.GetDirectoryRoot(path) == path) || Regex.IsMatch(path, "^[A-Za-z]:$"))
                return true;
            return false;
        }

        public static void DeleteDirectory(string path)
        {
            var folderToDelete = new DirectoryInfo(path);
            var emptyTempDirectory = new DirectoryInfo(Path.Combine(Path.GetTempPath(), "OverlongPathEraserHelperDirectory"));
            if (!Directory.Exists(emptyTempDirectory.FullName))
                emptyTempDirectory.Create();

            var arguments = string.Format("\"{0}\" \"{1}\" /MIR", emptyTempDirectory.FullName, folderToDelete.FullName); // if /MIR is not working, try /purge ??

            using (var process = Process.Start(new ProcessStartInfo("robocopy")
            {
                Arguments = arguments,
                CreateNoWindow = true,
                UseShellExecute = false,
            }))
            process.WaitForExit();
            folderToDelete.Delete();
            emptyTempDirectory.Delete();
        }
    }
}
