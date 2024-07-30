using System.Reflection;
using Application.Features.FlashCards.Services;
using Application.Features.FlashCards.Services.Impl;
using Application.Features.Users.Services;
using Application.Features.Users.Services.Impl;
using Application.Interceptors.Impl;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Interceptors;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            return services
                .AddAutoMapper()
                .AddMediator()
                .AddValidators()
                .AddInterceptors()
                .AddServices();
        }

        private static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        private static IServiceCollection AddMediator(this IServiceCollection services)
        {
            return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            return services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private static IServiceCollection AddInterceptors(this IServiceCollection services)
        {
            return services.AddScoped<IAuditableEntitiesInterceptor, AuditableEntitiesInterceptor>();
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IFavoriteSetsService, FavoriteSetsService>()
                .AddTransient<IUserService, UserService>();
        }
    }
}