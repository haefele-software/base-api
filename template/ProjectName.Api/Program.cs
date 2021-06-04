using System;
using System.IO;
using System.Threading.Tasks;
using Destructurama;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjectName.Application.Persistence;
using Serilog;

namespace ProjectName.Api
{
    public class Program
    {

        private static readonly string s_namespace = typeof(Program).Namespace;
        private static readonly string s_appName = s_namespace[(s_namespace.LastIndexOf('.', s_namespace.LastIndexOf('.') - 1) + 1)..];

        public static async Task Main(string[] args)
        {
            try
            {
                Log.Information("Configuring api host ({ApplicationContext})...", s_appName);
                var host = CreateHostBuilder(args).Build();

                Log.Information("Starting api host ({ApplicationContext})...", s_appName);

                Log.Information("Apply DB Migrations if any...", s_appName);
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    try
                    {
                        var context = services.GetRequiredService<ApplicationDbContext>();

                        if (context.Database.IsSqlServer())
                        {
                            context.Database.Migrate();
                            await ApplicationDbInitialiser.Initialize(context).ConfigureAwait(false);
                            Log.Information("DB Migrations complete.", s_appName);
                        }
                    }
                    catch (Exception ex)
                    {
                        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                        logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                        throw;
                    }
                }

                await host.RunAsync().ConfigureAwait(false);
                Log.Information("Api host ({ApplicationContext}) started successfully.", s_appName);

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", s_appName);

            }
            finally
            {
                Log.CloseAndFlush();
            }
        }



        // EF Core uses this method at design time to access the DbContext
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var configuration = GetConfiguration();
            return Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(
                     webBuilder => webBuilder.UseStartup<Startup>()
                                             .CaptureStartupErrors(false)
                                             .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                                             .UseContentRoot(Directory.GetCurrentDirectory())
                                             .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                                                 .ReadFrom.Configuration(hostingContext.Configuration)
                                                 .Destructure.UsingAttributes()
                                                 .WriteTo.ApplicationInsights(new TelemetryConfiguration { InstrumentationKey = configuration["ApplicationInsights:InstrumentationKey"] }, TelemetryConverter.Traces)
                                                 .WriteTo.Console()));
        }


        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            var keyVaultEndpoint = config.GetValue("KeyVault", string.Empty);
            if (!string.IsNullOrEmpty(keyVaultEndpoint))
            {
                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                builder.AddAzureKeyVault(keyVaultEndpoint, keyVaultClient, new DefaultKeyVaultSecretManager());
            }


            return builder.Build();
        }
    }
}
