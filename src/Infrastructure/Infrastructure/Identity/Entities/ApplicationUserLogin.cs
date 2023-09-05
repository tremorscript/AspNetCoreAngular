﻿// <copyright file="ApplicationUserLogin.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreAngular.Infrastructure.Identity.Entities
{
    public class ApplicationUserLogin : IdentityUserLogin<Guid>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
