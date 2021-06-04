
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.Generation.Processors.Security;
using ProjectName.Api.Controllers;
using ProjectName.Api.Filters;
using ProjectName.Api.Services;
using ProjectName.Application;
using ProjectName.Application.Contracts.Interfaces;
using ProjectName.Application.Infrastructure;
using ProjectName.Application.Persistence;
using Sieve.Models;

namespace ProjectName.Api
{
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddPersistence(Configuration);

            services.AddDataProtection();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                    .AddDbContextCheck<ApplicationDbContext>();

            services.Configure<SieveOptions>(Configuration.GetSection("Sieve"));

            services
                .AddResponseCompression(options =>
                {
                    options.EnableForHttps = true;
                })
                .AddResponseCaching()
                .AddControllers(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>())
                       .AddFluentValidation()
                       .AddNewtonsoftJson(x =>
                                    {
                                        x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                                        x.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                                    });

            services.AddApiVersioning(
               options =>
               {
                   options.ReportApiVersions = true;
                   options.DefaultApiVersion = new ApiVersion(1, 0, "Active");
                   options.AssumeDefaultVersionWhenUnspecified = true;
                   options.Conventions.Controller<TodoItemsController>().HasApiVersion(1, 0);
                   options.Conventions.Controller<TodoListsController>().HasApiVersion(1, 0);
               });

            //Helth Checks
            services.AddHealthChecks()
               .AddCheck("self", () => HealthCheckResult.Healthy());

            services.AddMvc();

            // Register the Swagger services
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Title = $"API";
                    document.Info.Description = "API Description";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new OpenApiContact
                    {
                        Name = "Haefele Software",
                        Email = "info@haefelesoftware.com",
                        Url = "https://www.haefelesoftware.com"
                    };
                };

                config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT"));
                config.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT", new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}"
                }));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseResponseCaching();
            app.UseResponseCompression();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
