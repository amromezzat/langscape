using System.IO;
using Infrastructure.Logging;
using Infrastructure.Logging.Impl;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions  
    {
        private const string LogFileName = "nlog.config";

        public static void AddLogging(this IServiceCollection services)
        {
            services.AddSingleton<ILogger, Logger>();

            services.AddLogging(configure =>
            {
                configure.AddNLog(Path.Combine(Directory.GetCurrentDirectory(), LogFileName));
            });
        }
    }
}