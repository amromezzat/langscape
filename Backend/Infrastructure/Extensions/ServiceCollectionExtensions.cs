using Application.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddUserAccessors();
        }

        private static void AddUserAccessors(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccessor, UserAccessor>();
        }
    }
}