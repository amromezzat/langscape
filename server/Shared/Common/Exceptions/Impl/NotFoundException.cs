using System;
using System.Net;

namespace Shared.Common.Exceptions.Impl
{
    public class NotFoundException : HandledException
    {
        private const HttpStatusCode Code = HttpStatusCode.NotFound;
        private const string DefaultMessage = "Resource not found";

        public NotFoundException() : base(DefaultMessage, Code)
        {
        }

        public NotFoundException(string message) : base(message, Code)
        {
        }

        public NotFoundException(string message, Exception exception) : base(message, exception, Code)
        {
        }
    }
}