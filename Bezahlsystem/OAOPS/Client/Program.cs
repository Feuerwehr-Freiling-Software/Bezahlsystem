
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using OAOPS.Client.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.Configure<ClientOptions>(builder.Configuration.GetSection("configuration"));
// add services to builder

var config = new ClientOptions();
builder.Configuration.GetSection("configuration").Bind(config);

builder.Services.AddMudServices();

builder.Services.AddHttpClient("OAOPS.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("OAOPS.ServerAPI"));

builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
