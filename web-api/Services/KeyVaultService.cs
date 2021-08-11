using System;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using web_api.Models;

namespace web_api.Services
{
    public class KeyVaultService : IKeyVaultService
    {
        private AppSettings _appSettings { get; set; }


        private SecretClient _secretClient { get; set; }

        public KeyVaultService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            var kvUri = _appSettings.KeyVaultUrl;

            if (kvUri != null)
            {
#if DEBUG
                _secretClient = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
#else
            _secretClient = new SecretClient(new Uri(kvUri), new EnvironmentCredential());
#endif
            }
        }

        public KeyVaultSecret GetKeyVaultSecret(string key)
        {
            try
            {
                return _secretClient.GetSecretAsync(key).Result;
            }
            catch (CredentialUnavailableException e)
            {
                throw new Exception($"Credentials Unavailable. {e.Message}");
            }
            catch (AuthenticationFailedException e)
            {
                throw new Exception($"Authentication Failed. {e.Message}");
            }
        }
    }
}