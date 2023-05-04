using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SimpleVendingMachine.Web;
using SimpleVendingMachine.Web.Services;
using SimpleVendingMachine.Web.Services.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7271/") });

builder.Services.AddSingleton<IStateContainerService, StateContainerService>();
builder.Services.AddScoped<IProductService, ProductService>();

await builder.Build().RunAsync();
