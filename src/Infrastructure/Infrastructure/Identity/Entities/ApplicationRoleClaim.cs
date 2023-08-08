// <copyright file="ApplicationRoleClaim.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.Infrastructure.Identity.Entities
{
    using System;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
    {
        public virtual ApplicationRole Role { get; set; }
    }
}
