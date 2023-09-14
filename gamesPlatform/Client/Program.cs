using cmArcade.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Runtime.InteropServices.JavaScript;

namespace cmArcade.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        if (OperatingSystem.IsBrowser())
        {
            await JSHost.ImportAsync("GameCanvas", "../GameCanvas.js");
            await JSHost.ImportAsync("CanvasContext", "../CanvasContext.js");
            await JSHost.ImportAsync("TouchPad", "../TouchPad.js");
        }
        builder.Services.AddScoped<IScoreService, ScoreService>();
        await builder.Build().RunAsync();
    }
}