using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security.Services.Impl
{
    public class TokenService : ITokenService
    {
        private const string _tokenConfigurationKey = "AuthenticationKey";
        private const int _expiryDays = 7;

        private readonly IConfiguration _config; 

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string CreateToken(AppUser appUser)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, appUser.UserName),
                new(ClaimTypes.NameIdentifier, appUser.Id),
                new(ClaimTypes.Email, appUser.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[_tokenConfigurationKey]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(_expiryDays),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static void AddToAuthenticationService(IServiceCollection services, IConfiguration configuration) 
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[_tokenConfigurationKey]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => 
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });
        }
    }
}