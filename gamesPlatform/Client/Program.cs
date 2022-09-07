using Blazored.LocalStorage;
using cmArcade.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace cmArcade.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<IScoreService, ScoreService>();
            await builder.Build().RunAsync();
        }
    }
}