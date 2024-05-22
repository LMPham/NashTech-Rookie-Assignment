using Application.Common.Interfaces;
using Domain.Constants;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace Infrastructure
{
    /// <summary>
    /// Extension of the <see cref="IServiceCollection"/> interface
    /// for injecting the dependencies of the Infrastructure layer.
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

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            //services.AddScoped<ApplicationDbContextInitialiser>();

            // Combines Bearer Token and Cookie Authentication
            services.AddAuthentication(options =>
                {
                    // Custom scheme defined in .AddPolicyScheme() below
                    options.DefaultScheme = "BEARER_OR_COOKIE";
                    options.DefaultChallengeScheme = "BEARER_OR_COOKIE";
                })
                .AddCookie(IdentityConstants.ApplicationScheme, options =>
                {
                    //options.LoginPath = "/";
                    //options.LogoutPath = "/";
                    //options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                })
                .AddBearerToken(IdentityConstants.BearerScheme)
                .AddPolicyScheme("BEARER_OR_COOKIE", "BEARER_OR_COOKIE", options =>
                {
                    // Filter auth type to choose Bearer auth or Cookie auth
                    options.ForwardDefaultSelector = context =>
                    {
                        string? authorization = context.Request.Headers[HeaderNames.Authorization];
                        if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                        {
                            // Returns Token auth
                            return IdentityConstants.BearerScheme;
                        }
                        // Otherwise, returns Cookie auth
                        return IdentityConstants.ApplicationScheme;
                    };
                });

            services.AddAuthorizationBuilder();

            services
                .AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddApiEndpoints();

            //services.AddSingleton(TimeProvider.System);
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthorization();
            //services.AddAuthorization(options =>
            //    options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

            //services.AddAuthorizationBuilder()
            //  .AddPolicy("AtLeast21", policy =>
            //  {
            //  policy.Requirements.Add(new MinimumAgeRequirement(21)));
            //  });

            return services;
        }
    }
}
