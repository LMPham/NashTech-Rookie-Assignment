using Application.Common.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure
{
    /// <summary>
    /// Extension of the <see cref="IServiceCollection"/> interface
    /// for injecting the dependencies of the Infrastructure layer
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

            //services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            //services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                //options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
            });

            //services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            //services.AddScoped<ApplicationDbContextInitialiser>();

            //#if (UseApiOnly)
            //    services.AddAuthentication()
            //        .AddBearerToken(IdentityConstants.BearerScheme);

            //    services.AddAuthorizationBuilder();

            //    services
            //        .AddIdentityCore<ApplicationUser>()
            //        .AddRoles<IdentityRole>()
            //        .AddEntityFrameworkStores<ApplicationDbContext>()
            //        .AddApiEndpoints();
            //#else
            //services
            //    .AddDefaultIdentity<ApplicationUser>()
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            //#endif

            //services.AddSingleton(TimeProvider.System);
            //services.AddTransient<IIdentityService, IdentityService>();

            //services.AddAuthorization(options =>
            //    options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

            return services;
        }
    }
}
