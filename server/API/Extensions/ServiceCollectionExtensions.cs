using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static string CorsPolicyName => "CorsPolicy";

        public static void AddApiLayer(this IServiceCollection services) 
        {
            services.AddCorsPolicy();
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
    }
}