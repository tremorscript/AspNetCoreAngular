// <copyright file="DeploymentEnvironment.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.Infrastructure.Environment
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using AspNetCoreAngular.Application.Abstractions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;

    public class DeploymentEnvironment : IDeploymentEnvironment
    {
        private readonly ILogger<DeploymentEnvironment> logger;
        private readonly IWebHostEnvironment hostingEnv;

        public DeploymentEnvironment(
            IWebHostEnvironment hostingEnv,
            ILogger<DeploymentEnvironment> logger)
        {
            this.hostingEnv = hostingEnv;
            this.logger = logger;
        }

        public string OS =>
            $"{RuntimeInformation.OSDescription} {RuntimeInformation.OSArchitecture}";

        public string MachineName => System.Environment.MachineName;

        public string RuntimeFramework =>
            $"{RuntimeInformation.FrameworkDescription} {RuntimeInformation.ProcessArchitecture}";

        public string EnvironmentName => this.hostingEnv.EnvironmentName;

        public string CommitSha => ThisAssembly.Git.Commit;

        public string Branch => ThisAssembly.Git.Branch;

        public string Tag => ThisAssembly.Git.Tag;
    }
}
