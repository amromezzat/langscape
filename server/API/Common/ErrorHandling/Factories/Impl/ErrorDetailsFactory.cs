using System;
using System.IO;
using System.Net;
using Langscape.Shared.Impl;
using MediatR;
using Shared.Common.Exceptions.Impl;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Services.ErrorHandling.Impl
{
    public class ErrorDetailsFactory : IErrorDetailsFactory
    {
        public Result<Unit> Create(HandledException handledException)
        {
            return Result<Unit>.Failure(handledException.Message)
                .WithException(handledException)
                .WithCode(handledException.StatusCode);
        }

        public Result<Unit> Create(Exception exception)
        {
            return Result<Unit>.Failure(exception.Message)
                .WithException(exception)
                .WithCode(GetErrorCode(exception));
        }

        public Result<Unit> Create(HttpStatusCode statusCode)
        {
            return Result<Unit>.Failure(GetMessage(statusCode))
                .WithCode(statusCode);
        }

        private static HttpStatusCode GetErrorCode(Exception exception)
        {
            return exception switch
            { 
                KeyNotFoundException or
                    FileNotFoundException or 
                    NullReferenceException => HttpStatusCode.NotFound,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                ArgumentException or 
                    InvalidOperationException or 
                    FormatException or 
                    OverflowException or
                    ValidationException => HttpStatusCode.BadRequest,
                TimeoutException => HttpStatusCode.RequestTimeout,
                _ => HttpStatusCode.InternalServerError
            };
        }

        private static string GetMessage(HttpStatusCode statusCode)
        {
            return statusCode switch
            {
                HttpStatusCode.Unauthorized => "Access Denied",
                HttpStatusCode.UnsupportedMediaType or
                    HttpStatusCode.BadRequest => "Bad Request",
                HttpStatusCode.NotFound => "Entity not found",
                HttpStatusCode.NotAcceptable => "Content type isn't acceptable",
                _ => ""
            };
        }
    }
}