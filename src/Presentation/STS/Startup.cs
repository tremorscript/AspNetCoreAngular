// <copyright file="Startup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AspNetCoreAngular.Application;
using AspNetCoreAngular.Common;
using AspNetCoreAngular.Infrastructure;
using AspNetCoreAngular.Infrastructure.Identity;
using AspNetCoreAngular.Infrastructure.Services;
using AspNetCoreAngular.STS.Seed;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreAngular.STS
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            this.HostingEnvironment = environment;
        }

        public static IConfiguration Configuration { get; set; }

        public IWebHostEnvironment HostingEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IProfileService, CustomProfileService>();
            services.AddTransient<IIdentitySeedData, IdentitySeedData>();

            services
                .AddApplication()
                .AddInfrastructure(Configuration, this.HostingEnvironment)
                .AddHealthChecks()
                .AddDbContextCheck<IdentityServerDbContext>();

            services.AddStsServer(Configuration, this.HostingEnvironment);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseInfrastructure(this.HostingEnvironment);
            app.UseRouting();
            app.UseCors(Constants.DefaultCorsPolicy);
            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
