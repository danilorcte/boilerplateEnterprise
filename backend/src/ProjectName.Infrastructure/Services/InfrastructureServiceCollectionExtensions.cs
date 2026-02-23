using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProjectName.Application.Abstractions;
using ProjectName.Application.Interfaces;
using ProjectName.Domain.Repositories;
using ProjectName.Domain.Services;
using ProjectName.Infrastructure.Cache;
using ProjectName.Infrastructure.MultiTenancy;
using ProjectName.Infrastructure.Persistence;
using ProjectName.Infrastructure.Persistence.Repositories;
using ProjectName.Infrastructure.Security;
using System.Text;

namespace ProjectName.Infrastructure.Services;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));

        services.AddStackExchangeRedisCache(options =>
            options.Configuration = configuration.GetConnectionString("Redis"));

        services.AddHttpContextAccessor();
        services.AddScoped<ITenantContext, HttpTenantContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IRefreshTokenStore, RedisRefreshTokenStore>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!))
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Product.Read", policy => policy.RequireClaim("permission", "product.read"));
            options.AddPolicy("Product.Write", policy => policy.RequireClaim("permission", "product.write"));
        });

        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Postgres")!)
            .AddRedis(configuration.GetConnectionString("Redis")!);

        return services;
    }
}
