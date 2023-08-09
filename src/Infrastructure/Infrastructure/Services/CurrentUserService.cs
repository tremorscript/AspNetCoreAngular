// <copyright file="CurrentUserService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.Infrastructure.Services
{
    using System;
    using System.Security.Claims;
    using AspNetCoreAngular.Application.Abstractions;
    using Microsoft.AspNetCore.Http;

    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _ = Guid.TryParse(
                httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier),
                out var userId);
            this.UserId = userId;
            this.IsAuthenticated = this.UserId != default;
        }

        public Guid UserId { get; }

        public bool IsAuthenticated { get; }
    }
}
