using Azure.Identity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            //services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddScoped<IUser, CurrentUser>();

            services.AddHttpContextAccessor();

            //services.AddHealthChecks()
            //    .AddDbContextCheck<ApplicationDbContext>();

            //services.AddExceptionHandler<CustomExceptionHandler>();

            //// Customise default API behaviour
            //services.Configure<ApiBehaviorOptions>(options =>
            //    options.SuppressModelStateInvalidFilter = true);

            //services.AddOpenApiDocument((configure, sp) =>
            //{
            //    configure.Title = "CleanArchitecture API";

            //#if (UseApiOnly)
            //// Add JWT
            //configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            //{
            //    Type = OpenApiSecuritySchemeType.ApiKey,
            //    Name = "Authorization",
            //    In = OpenApiSecurityApiKeyLocation.Header,
            //    Description = "Type into the textbox: Bearer {your JWT token}."
            //});

            //configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            //#endif
            //});

            services.AddScoped<IMediator, Mediator>();

            return services;
        }

        public static IServiceCollection AddKeyVaultIfConfigured(this IServiceCollection services, ConfigurationManager configuration)
        {
            //var keyVaultUri = configuration["KeyVaultUri"];
            //if (!string.IsNullOrWhiteSpace(keyVaultUri))
            //{
            //    configuration.AddAzureKeyVault(
            //        new Uri(keyVaultUri),
            //        new DefaultAzureCredential());
            //}

            return services;
        }
    }
}
