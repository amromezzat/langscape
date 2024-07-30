using System;

namespace Shared.Common.Exceptions.Impl
{
    public class RequiredFieldException : HandledValidationException
    {
        public RequiredFieldException(string message) : base(message)
        {
        }

        public RequiredFieldException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}