using Azure.Security.KeyVault.Secrets;

namespace web_api.Services
{
    public interface IKeyVaultService
    {
        KeyVaultSecret GetKeyVaultSecret(string key);
    }
}