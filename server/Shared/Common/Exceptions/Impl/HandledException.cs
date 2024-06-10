using System;
using System.Net;

namespace Shared.Common.Exceptions.Impl
{
    public class HandledException : Exception
    {
        public HttpStatusCode StatusCode { get; protected set; }

        public HandledException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public HandledException(string message, Exception innerException, HttpStatusCode statusCode) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}