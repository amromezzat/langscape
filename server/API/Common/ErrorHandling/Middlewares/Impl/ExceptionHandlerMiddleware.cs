using System;
using System.Net;
using System.Threading.Tasks;
using API.Services.ErrorHandling;
using Microsoft.AspNetCore.Http;
using Shared.Common.Exceptions.Impl;

namespace API.Common.ErrorHandling.Middlewares.Impl
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionHandlerService _exceptionHandlerService;

        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            IExceptionHandlerService exceptionHandlerService)
        {
            _next = next;
            _exceptionHandlerService = exceptionHandlerService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                if(context.Response.StatusCode >= 400 && !context.Response.HasStarted)
                {
                    await _exceptionHandlerService.HandleErrorCodeAsync(context, (HttpStatusCode)context.Response.StatusCode);
                }
            }
            catch (HandledException handledException)
            {
                await _exceptionHandlerService.HandleExceptionAsync(context, handledException);
            }
            catch (Exception exception)
            {
                await _exceptionHandlerService.HandleExceptionAsync(context, exception);
            }
        }
    }
}