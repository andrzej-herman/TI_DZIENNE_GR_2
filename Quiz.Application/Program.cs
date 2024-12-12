using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Quiz.Application;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://quizsan-api-ahcwd9chgadmbbfb.polandcentral-01.azurewebsites.net/") });
builder.Services.AddScoped<IGameService, GameService>();
await builder.Build().RunAsync();
