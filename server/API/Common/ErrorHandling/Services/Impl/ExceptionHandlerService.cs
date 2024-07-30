using System;
using System.Threading.Tasks;
using Infrastructure.Logging;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Shared.Common.Exceptions.Impl;
using MediatR;
using Langscape.Shared.Impl;
using System.Net;

namespace API.Services.ErrorHandling.Impl
{
    public class ExceptionHandlerService : IExceptionHandlerService
    {
        private const string ContantType = "application/json";

        private readonly IErrorDetailsFactory _errorDetailsFactory;
        private readonly ILogger _logger;

        public ExceptionHandlerService(IErrorDetailsFactory errorDetailsFactory, ILogger logger)
        {
            _errorDetailsFactory = errorDetailsFactory;
            _logger = logger;
        }

        public async Task HandleExceptionAsync(HttpContext context, HandledException exception)
        {
            _logger.LogError($"A handled exception occurred: {exception}");

            var errorDetails = _errorDetailsFactory.Create(exception);
            await WriteResponse(context, errorDetails);
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError($"An unhandled exception occurred: {exception}");

            var errorDetails = _errorDetailsFactory.Create(exception);
            await WriteResponse(context, errorDetails);
        }

        public async Task HandleErrorCodeAsync(HttpContext context, HttpStatusCode statusCode)
        {
            _logger.LogError($"An error code was returned {statusCode}");

            var errorDetails = _errorDetailsFactory.Create(statusCode);
            await WriteResponse(context, errorDetails);
        }

        private static async Task WriteResponse(HttpContext context, Result<Unit> errorDetails)
        {
            context.Response.StatusCode = (int)errorDetails.Code;
            context.Response.ContentType = ContantType;
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorDetails));
        }
    }
}