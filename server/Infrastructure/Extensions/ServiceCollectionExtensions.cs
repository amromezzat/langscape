using System.IO;
using Infrastructure.Logging.Impl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using IExternalLogger = NLog.ILogger;
using ILogger = Infrastructure.Logging.ILogger;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions  
    {
        private const string LogFileName = "nlog.config";

        public static void AddExternalLogging(this IServiceCollection services)
        {
            services.AddLogging(configure =>
            {
                configure.ClearProviders();
                configure.SetMinimumLevel(LogLevel.Trace);
                configure.AddNLog(Path.Combine(Directory.GetCurrentDirectory(), LogFileName));
            });
            services.AddSingleton<ILogger, Logger>();
        }
    }
}