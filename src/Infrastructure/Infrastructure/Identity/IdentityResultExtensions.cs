// <copyright file="IdentityResultExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.Infrastructure.Identity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AspNetCoreAngular.Application.Common.Models;
    using Microsoft.AspNetCore.Identity;

    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
    }
}
