
using Microsoft.AspNetCore.Authentication;
using Serilog.Core;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using Serilog.Sinks;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Duende.IdentityServer.Models;
using OAOPS.Client.Services;

// Centralized Logging Framework
// https://datalust.co/seq

try
{
    var builder = WebApplication.CreateBuilder(args);
    var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    builder.Host.UseSerilog((ctx, lc) => lc
       .ReadFrom.Configuration(config));

    Serilog.Log.Information("======== Starting up. ========");

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    //builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //                options.UseSqlServer(connectionString));



    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" || Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }
    else
    {
        builder.Services.AddDbContext<TestingDbContext>(options =>
        {
            options.UseInMemoryDatabase("TestingDbContext");
        });
    }

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

    builder.Services
        .AddIdentityServer()
        .AddClients()
        .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(opt =>
        {
            opt.IdentityResources["openid"].UserClaims.Add("role");
            opt.ApiResources.Single().UserClaims.Add("role");
        });

    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");

    builder.Services.AddAuthentication()
        .AddIdentityServerJwt();

    builder.Services.AddTransient<IEmailSender, EmailSender>();
    builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

    builder.Services.ConfigureApplicationCookie(o =>
    {
        o.ExpireTimeSpan = TimeSpan.FromDays(5);
        o.SlidingExpiration = true;
    });

    builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
           o.TokenLifespan = TimeSpan.FromHours(3));

    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();

    builder.Services.AddSwaggerGen(opt =>
    {
        opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Protected API", Version = "v1" });
        opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri("https://localhost:7127/connect/authorize"),
                    TokenUrl = new Uri("https://localhost:7127/connect/token"),
                    Scopes = new Dictionary<string, string>
                    {
                        {
                            "OAOPS.ServerAPI", "Demo API - full access"
                        }
                    }
                }
            }
        });
        opt.OperationFilter<AuthorizeCheckOperationFilter>();
    });


    // add custom Services
    builder.Services.AddScoped<ILoggerService, LoggerService>();
    builder.Services.AddScoped<IEmailSender, EmailSender>();
    //builder.Services.AddScoped<IEmailService, EmailService>();
    builder.Services.AddScoped<IErrorCodeService, ErrorCodeService>();
    builder.Services.AddScoped<ISuggestionService, SuggestionService>();
    builder.Services.AddScoped<IPriceService, PriceService>();
    builder.Services.AddScoped<IArticleService, ArticleService>();
    builder.Services.AddScoped<IStorageService, StorageService>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<IUserService, UserService>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
        app.UseWebAssemblyDebugging();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

            options.OAuthClientId("api_swagger");
            options.OAuthAppName("Demo API - Swagger");
            options.OAuthUsePkce();
        });
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseIdentityServer();
    app.UseAuthentication();
    app.UseAuthorization();


    app.MapRazorPages();
    app.MapControllers();
    app.MapDefaultControllerRoute();
    app.MapFallbackToFile("index.html");


    app.Run();

}
catch (Exception ex)
{
    Serilog.Log.Fatal(ex, "Unhandled exception");
    // Comment out the throw statement to prevent the exception from propagating and causing the unit test to fail.
    // throw;
}
finally
{
    Serilog.Log.Information("======== Successful shutdown ========");
    Serilog.Log.CloseAndFlush();
}