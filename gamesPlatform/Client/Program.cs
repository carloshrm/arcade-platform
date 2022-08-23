using Blazored.LocalStorage;
using gamesPlatform.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace gamesPlatform.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddSingleton<IScoreService, ScoreService>();
            await builder.Build().RunAsync();
        }
    }
}