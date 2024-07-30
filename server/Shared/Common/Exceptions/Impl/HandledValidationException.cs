using System;
using System.Net;

namespace Shared.Common.Exceptions.Impl
{
    public class HandledValidationException : HandledException
    {
        private const HttpStatusCode Code = HttpStatusCode.BadRequest;
        private const string DefaultMessage = "Invalid request parameters";

        public HandledValidationException() : base(DefaultMessage, Code)
        {
        }

        public HandledValidationException(string message) : base(message, Code)
        {
        }

        public HandledValidationException(Exception innerException) : base(DefaultMessage, innerException, Code)
        {
        }

        public HandledValidationException(string message, Exception innerException) : base(message, innerException, Code)
        {
        }
    }
}