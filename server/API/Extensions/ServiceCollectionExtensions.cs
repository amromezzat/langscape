using System.Linq;
using System.Net;
using API.Services.ErrorHandling;
using API.Services.ErrorHandling.Impl;
using Langscape.Shared.Impl;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Common.Impl;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static string CorsPolicyName => "CorsPolicy";

        public static void AddApiLayer(this IServiceCollection services) 
        {
            services.AddCorsPolicy();
            services.AddExceptionHandler();
        }

        private static void AddCorsPolicy(this IServiceCollection services) 
        {
            services.AddCors(opt => {
                opt.AddPolicy(CorsPolicyName, policy => {
                    policy
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:3000");
                });
            });
        }

        private static void AddExceptionHandler(this IServiceCollection services)
        {
            services.AddTransient<IExceptionHandlerService, ExceptionHandlerService>();
            services.AddTransient<IErrorDetailsFactory, ErrorDetailsFactory>();
            
            services.Configure<ApiBehaviorOptions>(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = context.ModelState
                            .Where(e => e.Value.Errors.Count > 0)
                            .SelectMany(x => x.Value.Errors.Select(e => new ValidationError(x.Key, e.ErrorMessage)))
                            .ToArray();
                        var errorDetails = Result<Unit>.Failure("One or more validation errors occurred.", errors)
                            .WithCode(HttpStatusCode.UnprocessableEntity);
                        return new BadRequestObjectResult(errorDetails);
                    };
                }
            );
        }
    }
}