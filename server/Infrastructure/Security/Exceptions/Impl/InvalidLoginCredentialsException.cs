using System.Net;
using Shared.Common.Exceptions.Impl;

namespace Infrastructure.Security.Exceptions.Impl
{
    public class InvalidLoginCredentialsException : HandledException
    {
        private const HttpStatusCode Code = HttpStatusCode.BadRequest;
        private const string DefaultMessage = "Invalid login credentials";

        public InvalidLoginCredentialsException() : base(DefaultMessage, Code)
        {
        }
    }
}