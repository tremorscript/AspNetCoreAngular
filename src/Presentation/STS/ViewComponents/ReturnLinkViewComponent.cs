// <copyright file="ReturnLinkViewComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.STS.ViewComponents
{
    using AspNetCoreAngular.Application.Abstractions;
    using Microsoft.AspNetCore.Mvc;

    public class ReturnLinkViewComponent : ViewComponent
    {
        private readonly IClientInfoService clientInfoService;

        public ReturnLinkViewComponent(IClientInfoService clientInfoService)
        {
            this.clientInfoService = clientInfoService;
        }

        public IViewComponentResult Invoke()
        {
            return this.View<string>(this.clientInfoService.GetClient().ClientUri);
        }
    }
}
