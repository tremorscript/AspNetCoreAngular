// <copyright file="IdentitySeedData.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.STS.Seed
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using AspNetCoreAngular.Infrastructure.Identity;
    using AspNetCoreAngular.Infrastructure.Identity.Entities;
    using Duende.IdentityServer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class IdentitySeedData : IIdentitySeedData
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public IdentitySeedData(
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            // Seeding Users and roles
            var appContext = scope.ServiceProvider.GetService<IdentityServerDbContext>();
            appContext.Database.Migrate();
            this.CreateRoles();
            this.CreateUsers(appContext);
        }

        private void CreateRoles()
        {
            var rolesToAdd = new List<ApplicationRole>()
            {
                new ApplicationRole { Name = "Admin", Description = "Role with full rights" },
                new ApplicationRole { Name = "User", Description = "Role with limited rights" },
            };
            foreach (var role in rolesToAdd)
            {
                if (!this.roleManager.RoleExistsAsync(role.Name).Result)
                {
                    this.roleManager.CreateAsync(role).Result.ToString();
                }
            }
        }

        private void CreateUsers(IdentityServerDbContext context)
        {
            if (!context.Users.Any())
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    Mobile = "0123456789",
                    EmailConfirmed = true,
                };
                this.userManager.CreateAsync(adminUser, "P@ssw0rd!").Result.ToString();
                this.userManager
                    .AddClaimAsync(
                        adminUser,
                        new Claim(
                            IdentityServerConstants.StandardScopes.Phone,
                            adminUser.Mobile.ToString(),
                            ClaimValueTypes.Integer))
                    .Result.ToString();
                this.userManager
                    .AddToRoleAsync(
                        this.userManager.FindByNameAsync("admin@admin.com").GetAwaiter().GetResult(),
                        "Admin")
                    .Result.ToString();

                var normalUser = new ApplicationUser
                {
                    UserName = "user@user.com",
                    Email = "user@user.com",
                    Mobile = "0123456789",
                    EmailConfirmed = true,
                };
                this.userManager.CreateAsync(normalUser, "P@ssw0rd!").Result.ToString();
                this.userManager
                    .AddClaimAsync(
                        adminUser,
                        new Claim(
                            IdentityServerConstants.StandardScopes.Phone,
                            adminUser.Mobile.ToString(),
                            ClaimValueTypes.Integer))
                    .Result.ToString();
                this.userManager
                    .AddToRoleAsync(
                        this.userManager.FindByNameAsync("user@user.com").GetAwaiter().GetResult(),
                        "User")
                    .Result.ToString();
            }
        }
    }
}
