using System.Reflection;
using Application.Features.FlashCards.Services;
using Application.Features.FlashCards.Services.Impl;
using Application.Interceptors.Impl;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Interceptors;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddMediator();
            services.AddValidators();
            services.AddInterceptors();
            services.AddServices();
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        private static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

        private static void AddValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private static void AddInterceptors(this IServiceCollection services)
        {
            services.AddScoped<IAuditableEntitiesInterceptor, AuditableEntitiesInterceptor>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IFavoriteSetsService, FavoriteSetsService>();
        }
    }
}