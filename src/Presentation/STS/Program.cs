﻿// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.STS
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AspNetCoreAngular.Application;
    using AspNetCoreAngular.Application.Abstractions;
    using AspNetCoreAngular.Application.Features.System.Commands.SeedLocalizationData;
    using AspNetCoreAngular.STS.Seed;
    using MediatR;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                try
                {
                    var mediator = services.GetRequiredService<IMediator>();

                    var localizationDbContext =
                        services.GetRequiredService<ILocalizationDbContext>();
                    localizationDbContext.Database.Migrate();
                    var env = services.GetRequiredService<IWebHostEnvironment>();
                    await mediator.Send(
                        new LocalizationDataSeederCommand { ContentRoot = env.ContentRootPath },
                        CancellationToken.None);

                    logger.LogInformation("Seeding STS database");
                    var seedData = services.GetRequiredService<IIdentitySeedData>();
                    seedData.Seed(services);
                }
                catch (Exception ex)
                {
                    logger.LogCritical("Error creating/seeding STS database - " + ex);
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(
                    (host, configuration) =>
                        configuration.AddCustomAppSettings(
                            host.HostingEnvironment.ContentRootPath,
                            host.HostingEnvironment.EnvironmentName))
                .UseStartup<Startup>();
    }
}
