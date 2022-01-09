using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;

namespace TestePloomes.Services
{
    public class DatabaseService
    {
        public static string getConnectionString()
        {
            SecretClientOptions options = new SecretClientOptions()
            {
                Retry =
                {
                    Delay= TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                 }
            };
            var client = new SecretClient(new Uri("https://TestePloomes-vault.vault.azure.net/"), new DefaultAzureCredential(), options);
            KeyVaultSecret secret = client.GetSecret("ConnectionString");

            return secret.Value;
        }
    }
}
