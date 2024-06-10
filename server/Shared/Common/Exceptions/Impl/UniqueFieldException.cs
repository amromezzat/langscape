using System;

namespace Shared.Common.Exceptions.Impl
{
    public class UniqueFieldException : HandledValidationException
    {
        public UniqueFieldException(string message) : base(message)
        {
        }

        public UniqueFieldException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}