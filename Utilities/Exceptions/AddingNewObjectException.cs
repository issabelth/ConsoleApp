using System;

namespace Utilities.Exceptions
{
    public class AddingNewObjectException : Exception
    {
        public AddingNewObjectException(string msg) : base(msg) { }
    }
}
