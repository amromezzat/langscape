using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Infrastructure.Security.Authorization;
using Infrastructure.Security.Services.Impl;

namespace Infrastructure.Security.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddTokenService()
                .AddAccessors()
                .AddAuthentication(configuration)
                .AddAuthorizations();
        }

        private static IServiceCollection AddTokenService(this IServiceCollection services)
        {
            return services.AddScoped<ITokenService, TokenService>();
        }

        private static IServiceCollection AddAccessors(this IServiceCollection services)
        {
            return services
                .AddHttpContextAccessor()
                .AddScoped<IUserAccessor, UserAccessor>()
                .AddScoped<IUserManager, UserManager>();
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<AppUser>(opt => 
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => 
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }

        private static IServiceCollection AddAuthorizations(this IServiceCollection services)
        {
            services.AddControllers(opt => 
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddAuthorization(opt => 
            {
                opt.AddPolicy(nameof(IsOwnerRequirement<FlashCardsSet>), policy => 
                {
                    policy.Requirements.Add(new IsOwnerRequirement<FlashCardsSet>());
                });
            });

            services.AddTransient<IAuthorizationHandler, IsOwnerRequirementHandler<FlashCardsSet>>();

            return services;
        }
    }
}