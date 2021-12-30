using Azure.Identity;
using FinancialTracker.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;

namespace FinanceTrackerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SqlAuthenticationProvider.SetProvider(SqlAuthenticationMethod.ActiveDirectoryInteractive, new SqlAppAuthenticationProvider());
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureAppConfiguration((context, config) =>
                {
                    var buildConfig = config.Build();
                    var vaultName = buildConfig["KeyVaultConfig:KeyVaultUrl"];
                    var keyVaultClient = new KeyVaultClient(async (authority, resource, scope) =>
                    {
                        var credential = new DefaultAzureCredential(false);
                        var token = credential.GetToken(
                                new Azure.Core.TokenRequestContext(new[] { "https://vault.azure.net/.default" }
                                ));
                        return token.Token;
                    });
                    config.AddAzureKeyVault(vaultName, keyVaultClient, new DefaultKeyVaultSecretManager());
                });
    }
}
