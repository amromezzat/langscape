using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shared.Common.Exceptions.Impl;

namespace API.Services.ErrorHandling
{
    public interface IExceptionHandlerService
    {
        /// <summary>
        /// Write error message to result context from handled exception
        /// </summary>
        Task HandleExceptionAsync(HttpContext context, HandledException handledException);

        /// <summary>
        /// Write error message to result context from unhandled exception
        /// </summary>
        Task HandleExceptionAsync(HttpContext context, Exception exception);

        /// <summary>
        /// Write error message to result context from error code
        /// </summary>
        Task HandleErrorCodeAsync(HttpContext context, HttpStatusCode errorCode);
    }
}