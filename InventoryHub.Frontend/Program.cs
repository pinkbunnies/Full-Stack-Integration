using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using InventoryHub.Frontend;
using InventoryHub.Frontend.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configurar HttpClient con la URL base del back-end
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://api-servers.cloud:5040") });

// Registrar el ProductService como scoped (para que consuma HttpClient sin conflictos)
builder.Services.AddScoped<ProductService>();

await builder.Build().RunAsync();
