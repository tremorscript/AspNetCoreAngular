using System;
using AspNetCoreAngular.Application.Abstractions;
using AspNetCoreAngular.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAngular.Web.Controllers
{
    [AllowAnonymous]
    public class AppController : BaseController
    {
        private readonly IApplicationService _applicationService;

        public AppController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect("~/");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ApplicationDataViewModel> GetApplicationData()
        {
            var appData = _applicationService.GetApplicationData();

            return Ok(appData);
        }
    }
}
