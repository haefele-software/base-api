using Clean.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectName.Application.Contracts.Interfaces;
using ProjectName.Application.Domain.Infrastructure;
using ProjectName.Application.Infrastructure.Cryptography;
using ProjectName.Application.Infrastructure.Identity;
using ProjectName.Application.Infrastructure.Services;
using ProjectName.Common.Configuration;

namespace ProjectName.Application.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<CryptographySettings>().Configure<IConfiguration>((options, configuration) =>
            {
                configuration.GetSection(nameof(CryptographySettings)).Bind(options);
            });

            services.AddScoped<IDomainEventService, DomainEventService>();

            //services.AddScoped<ICryptographyService, KeyVaultCryptographyService>();
            services.AddScoped<ICryptographyService, MockCryptoService>();


            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });

            return services;
        }
    }
}
