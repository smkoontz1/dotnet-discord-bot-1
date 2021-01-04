using BoxcatBot.Services.ChartLyricsApi;
using BoxcatBot.Services.Common;
using BoxcatBot.Services.SpotifyApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BoxcatBot
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        internal static async Task MainAsync()
        {
            IConfiguration configuration = BuildConfiguration();

            var serviceCollection = new ServiceCollection()
                .AddSingleton(configuration)
                .AddSingleton<ChartLyricsApiService>()
                .AddSingleton<SpotifyApiService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var discordClientService = new DiscordClientService(configuration);
            
            await discordClientService.InitializeClientAsync(serviceProvider);
        }

        internal static IConfiguration BuildConfiguration()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environmentName}.json")
                .AddUserSecrets("0e6d8396-f83f-4e5c-adaf-5913728d2f58")
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            return configuration;
        }
    }
}
