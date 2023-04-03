
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using OAOPS.Client.Configuration;
using OAOPS.Client.Services;
using Serilog;
using Serilog.Core;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var levelSwitch = new LoggingLevelSwitch
{
    MinimumLevel = Serilog.Events.LogEventLevel.Debug
};
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.ControlledBy(levelSwitch)
    .Enrich.WithProperty("InstanceId", Guid.NewGuid().ToString("n"))
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

builder.Services.AddLogging(loggingbuilder => loggingbuilder.AddSerilog(dispose: true));

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.Configure<ClientOptions>(builder.Configuration.GetSection("configuration"));
// add services to builder

var config = new ClientOptions();
builder.Configuration.GetSection("configuration").Bind(config);

builder.Services.AddMudServices();

builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddHttpClient("OAOPS.ServerAPI", client => { { client.BaseAddress = new Uri(config.ApiEndpoints.BaseUri); } })
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("OAOPS.ServerAPI"));

builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
