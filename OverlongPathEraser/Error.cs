using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OverlongPathEraser
{
    public sealed class PathError : IEquatable<PathError>
    {

        private PathError(string message)
        {
            Message = message;
        }

        public string Message { get; }

        public override string ToString() => Message;

        public override bool Equals(object obj) => this.Equals(obj as PathError);

        public bool Equals(PathError other) =>
            (other != null) &&
            (Message == other.Message);

        public override int GetHashCode() => Message.GetHashCode();

        public static PathError NotExisting => 
            new PathError("Path to delete does not exist");

        public static PathError IsNoFolder =>
            new PathError("Path needs to point to a folder");

        public static PathError IsDriveLetter =>
            new PathError("Path must not be a root folder / drive letter");
    }
}