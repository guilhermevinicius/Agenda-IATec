using System.Text;
using Agenda.Domain.Services;
using Agenda.Infra.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Agenda.Infra.IoC;

public static class ServicesIoC
{
    public static void Services(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IGenerateJwt, GenerateJwtAdapter>();
        services.AddTransient<IHashPassword, HashPasswordAdapter>();

        var key = Encoding.ASCII.GetBytes(configuration["JwtKey"]!);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }
}