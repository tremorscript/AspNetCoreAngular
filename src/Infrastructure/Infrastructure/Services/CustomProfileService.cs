// <copyright file="CustomProfileService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.Infrastructure.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using AspNetCoreAngular.Infrastructure.Identity.Entities;
    using Duende.IdentityServer.Extensions;
    using Duende.IdentityServer.Models;
    using Duende.IdentityServer.Services;
    using Microsoft.AspNetCore.Identity;

    public class CustomProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory;
        private readonly UserManager<ApplicationUser> userManager;

        public CustomProfileService(
            UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            this.userManager = userManager;
            this.claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await this.userManager.FindByIdAsync(sub);
            var principal = await this.claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();

            if (context.Caller == "ClaimsProviderIdentityToken")
            {
                context.IssuedClaims = claims;
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await this.userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
