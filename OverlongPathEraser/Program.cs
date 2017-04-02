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

            foreach (var path in args)
            {
                var overlongPath = new OverlongPath(path);

                if (ConfirmDeletion(path))
                {
                    var operationResult = overlongPath.Erase();

                    Console.Write(Environment.NewLine);
                    Console.WriteLine(operationResult.Report());
                    Console.Write(Environment.NewLine);
                }
            }

            Console.WriteLine("Press 'Enter' to close...");
            Console.ReadLine();

        }


        private static bool ConfirmDeletion(string path)
        {
            Console.WriteLine("Do you want to erase: " + path + "?" + Environment.NewLine);
            Console.WriteLine("(Y)es or (N)o?");
            var userResponse = Console.ReadLine();

            if (userResponse.ToUpper() == "Y")
                return true;

            return false;
        }



    }
}
