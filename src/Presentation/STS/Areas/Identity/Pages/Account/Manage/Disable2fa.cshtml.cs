﻿// <copyright file="Disable2fa.cshtml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreAngular.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AspNetCoreAngular.STS.Areas.Identity.Pages.Account.Manage
{
    public class Disable2faModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<Disable2faModel> logger;

        public Disable2faModel(
            UserManager<ApplicationUser> userManager,
            ILogger<Disable2faModel> logger)
        {
            this.userManager = userManager;
            this.logger = logger;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!await this.userManager.GetTwoFactorEnabledAsync(user))
            {
                throw new InvalidOperationException(
                    $"Cannot disable 2FA for user with ID '{this.userManager.GetUserId(this.User)}' as it's not currently enabled.");
            }

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            var disable2faResult = await this.userManager.SetTwoFactorEnabledAsync(user, false);
            if (!disable2faResult.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Unexpected error occurred disabling 2FA for user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            this.logger.LogInformation(
                "User with ID '{UserId}' has disabled 2fa.",
                this.userManager.GetUserId(this.User));
            this.StatusMessage =
                "2fa has been disabled. You can reenable 2fa when you setup an authenticator app";
            return this.RedirectToPage("./TwoFactorAuthentication");
        }
    }
}
