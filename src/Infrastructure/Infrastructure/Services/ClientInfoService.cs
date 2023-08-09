// <copyright file="ClientInfoService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.Infrastructure.Services
{
    using System;
    using System.Linq;
    using AspNetCoreAngular.Application.Abstractions;
    using AspNetCoreAngular.Application.Models;
    using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    public class ClientInfoService : IClientInfoService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IClientRequestParametersProvider clientRequestParameters;

        public ClientInfoService(
            IHttpContextAccessor httpContextAccessor,
            IClientRequestParametersProvider clientRequestParameters)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.clientRequestParameters = clientRequestParameters;
        }

        public ClientInfo GetClient()
        {
            const string ReturnUrl = "returnUrl";
            const string ClientId = "client_id";
            const string CallbackClientId = "/connect/authorize/callback?client_id";

            var query = this.httpContextAccessor.HttpContext.Request.Query;

            if (query.ContainsKey(ReturnUrl))
            {
                var returnUrl = query[ReturnUrl];
                var parsedQuery = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(
                    returnUrl);

                var clientId = parsedQuery.FirstOrDefault(
                    q => q.Key == ClientId || q.Key == CallbackClientId);

                if (!string.IsNullOrEmpty(clientId.Value))
                {
                    var clientParameters = this.clientRequestParameters.GetClientParameters(
                        this.httpContextAccessor.HttpContext,
                        clientId.Value);
                    clientParameters.TryGetValue("redirect_uri", out var clientRedirectUri);
                    var uri = new Uri(clientRedirectUri);
                    var clientUrl = uri.AbsoluteUri[..uri.AbsoluteUri.IndexOf("authentication")];
                    var clientInfo = new ClientInfo
                    {
                        ClientId = clientId.Value,
                        ClientUri = clientUrl,
                    };

                    return clientInfo;
                }
            }

            return new ClientInfo();
        }
    }
}
