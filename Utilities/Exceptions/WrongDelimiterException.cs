using System;

namespace Utilities.Exceptions
{
    public class WrongDelimiterException : Exception
    {
        public WrongDelimiterException(string msg) : base(msg) { }
    }
}
