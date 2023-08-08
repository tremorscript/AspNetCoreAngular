// <copyright file="BaseTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.Integration.Tests.IntegrationTests
{
    using System.Threading.Tasks;
    using AspNetCoreAngular.Web;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Xunit;

    public class BasicTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public BasicTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Theory(Skip = "TODO: enable npm script in Web Startup and remove skip")]
        [InlineData("/")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = this.factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var contentType = response.Content.Headers.ContentType.ToString();
            Assert.Equal("text/html; charset=UTF-8", contentType);
        }
    }
}
