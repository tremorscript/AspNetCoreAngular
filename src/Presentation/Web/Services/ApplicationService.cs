using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AspNetCoreAngular.Application.Abstractions;
using AspNetCoreAngular.Application.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace AspNetCoreAngular.Web.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly RequestLocalizationOptions locOptions;
        private readonly IStringLocalizer<ApplicationService> stringLocalizer;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IMemoryCache cache;
        private readonly IDeploymentEnvironment deploymentEnvironment;

        public ApplicationService(
            IOptions<RequestLocalizationOptions> locOptions,
            IStringLocalizer<ApplicationService> stringLocalizer,
            IHttpContextAccessor contextAccessor,
            IMemoryCache memoryCache,
            IDeploymentEnvironment deploymentEnvironment)
        {
            this.locOptions = locOptions.Value;
            this.stringLocalizer = stringLocalizer;
            this.contextAccessor = contextAccessor;
            cache = memoryCache;
            this.deploymentEnvironment = deploymentEnvironment;
        }

        public ApplicationDataViewModel GetApplicationData()
        {
            var applicationData = new ApplicationDataViewModel
            {
                Content = GetContentByCulture(),
                CookieConsent = GetCookieConsent(),
                Cultures = locOptions.SupportedUICultures
                    .Select(
                        c =>
                            new CulturesDisplayViewModel
                            {
                                Value = c.Name,
                                Text = c.NativeName,
                                Current = c.Name == Thread.CurrentThread.CurrentCulture.Name,
                            })
                    .ToList(),
                EnvironmentInfo = new EnvironmentInformation
                {
                    OS = deploymentEnvironment.OS,
                    MachineName = deploymentEnvironment.MachineName,
                    EnvironmentName = deploymentEnvironment.EnvironmentName,
                    FrameworkVersion = deploymentEnvironment.RuntimeFramework,
                    CommitHash = deploymentEnvironment.CommitSha,
                    Branch = deploymentEnvironment.Branch,
                    Tag = deploymentEnvironment.Tag,
                },
            };

            return applicationData;
        }

        private Dictionary<string, string> GetContentByCulture()
        {
            var requestCulture =
                contextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();

            // Culture contains the information of the requested culture
            var culture = requestCulture.RequestCulture.Culture;
            var cACHE_KEY = $"Content-{culture.Name}";

            Dictionary<string, string> cacheEntry;

            // Look for cache key.
            if (!cache.TryGetValue(cACHE_KEY, out cacheEntry))
            {
                // Key not in cache, so get & set data.
                var culturalContent = stringLocalizer
                    .GetAllStrings()
                    .Select(c => new { Key = c.Name, Value = c.Value })
                    .ToDictionary(x => x.Key, x => x.Value);
                cacheEntry = culturalContent;

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()

                // Keep in cache for this time, reset time if accessed.
                .SetSlidingExpiration(TimeSpan.FromMinutes(30));

                // Save data in cache.
                cache.Set(cACHE_KEY, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        private object GetCookieConsent()
        {
            var consentFeature =
                contextAccessor.HttpContext.Features.Get<ITrackingConsentFeature>();
            var showConsent = !consentFeature?.CanTrack ?? false;
            var cookieString = consentFeature?.CreateConsentCookie();
            return new { showConsent, cookieString };
        }
    }
}
