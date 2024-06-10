using System.Net;

namespace Shared.Common.Exceptions.Impl
{
    public class AuthorizationFailedException : HandledException
    {
        private const HttpStatusCode Code = HttpStatusCode.Unauthorized;
        private const string DefaultMessage = "Access Denied";
    
        public AuthorizationFailedException() : base(DefaultMessage, Code)
        {
        }
    }
}