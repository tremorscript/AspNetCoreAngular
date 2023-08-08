// <copyright file="KeyVaultCertificateService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.Infrastructure.Services.Certificate
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using Microsoft.Azure.KeyVault;
    using Microsoft.IdentityModel.Clients.ActiveDirectory;

    public class KeyVaultCertificateService : ICertificateService
    {
        private readonly string vaultAddress;
        private readonly string vaultClientId;
        private readonly string vaultClientSecret;

        public KeyVaultCertificateService(
            string vaultAddress,
            string vaultClientId,
            string vaultClientSecret)
        {
            this.vaultAddress = vaultAddress;
            this.vaultClientId = vaultClientId;
            this.vaultClientSecret = vaultClientSecret;
        }

        public X509Certificate2 GetCertificateFromKeyVault(string vaultCertificateName)
        {
            var keyVaultClient = new KeyVaultClient(this.AuthenticationCallback);

            var certBundle = keyVaultClient
                .GetCertificateAsync(this.vaultAddress, vaultCertificateName)
                .Result;
            var certContent = keyVaultClient
                .GetSecretAsync(certBundle.SecretIdentifier.Identifier)
                .Result;
            var certBytes = Convert.FromBase64String(certContent.Value);
            var cert = new X509Certificate2(certBytes);
            return cert;
        }

        private async Task<string> AuthenticationCallback(
            string authority,
            string resource,
            string scope)
        {
            var clientCredential = new ClientCredential(this.vaultClientId, this.vaultClientSecret);

            var context = new AuthenticationContext(authority, TokenCache.DefaultShared);
            var result = await context.AcquireTokenAsync(resource, clientCredential);

            return result.AccessToken;
        }
    }
}
