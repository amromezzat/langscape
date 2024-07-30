using System;
using System.Net;
using Langscape.Shared.Impl;
using MediatR;
using Shared.Common.Exceptions.Impl;

namespace API.Services.ErrorHandling
{
    public interface IErrorDetailsFactory
    {
        /// <returns>Response from a handled error</returns>
        Result<Unit> Create(HandledException handledException);

        /// <returns>Response from an unhandled error</returns>
        Result<Unit> Create(Exception exception);

        /// <returns>Response from a status code</returns>
        Result<Unit> Create(HttpStatusCode statusCode);
    }
}