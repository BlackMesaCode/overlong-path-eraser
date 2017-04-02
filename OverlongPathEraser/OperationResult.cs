using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OverlongPathEraser
{
    public sealed class OperationResult
    {
        public OperationResult(List<string> errors)
        {
            Errors = errors;
        }

        public OperationResult(string successMessage)
        {
            SuccessMessage = successMessage;
        }

        public bool Success => (SuccessMessage != null);
        public string SuccessMessage { get; }
        public List<string> Errors { get; }

        public void ReportAlternative() => Errors.ForEach((error) => Console.WriteLine(error));

        public string Report()
        {
            if (Success)
            {
                return SuccessMessage;
            }
            else
            {
                var builder = new StringBuilder();
                foreach (var errorMessage in Errors)
                {
                    builder.AppendLine(errorMessage);
                }
                return builder.ToString();
            }
        }
    }
}