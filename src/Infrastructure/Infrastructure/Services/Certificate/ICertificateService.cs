// <copyright file="ICertificateService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.Infrastructure.Services.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    public interface ICertificateService
    {
        X509Certificate2 GetCertificateFromKeyVault(string vaultCertificateName);
    }
}
