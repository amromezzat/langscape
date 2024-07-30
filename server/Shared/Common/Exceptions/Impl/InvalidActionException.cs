using System;
using System.Net;

namespace Shared.Common.Exceptions.Impl
{
    public class InvalidActionException : HandledException
    {
        private const HttpStatusCode Code = HttpStatusCode.BadRequest;
        private const string DefaultMessage = "Invalid Action";

        public InvalidActionException() : base(DefaultMessage, Code)
        {
        }

        public InvalidActionException(string message) : base(message, Code)
        {
        }

        public InvalidActionException(string message, Exception exception) : base(message, exception, Code)
        {
        }
    }
}