using Microsoft.AspNetCore.Authentication;
using Serilog.Core;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using Serilog.Sinks;

// Centralized Logging Framework
// https://datalust.co/seq
//
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

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

    builder.Services.AddIdentityServer()
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


    // add custom Services
    builder.Services.AddScoped<ILoggerService, LoggerService>();
    builder.Services.AddScoped<IErrorCodeService, ErrorCodeService>();
    builder.Services.AddScoped<ISuggestionService, SuggestionService>();
    builder.Services.AddScoped<IPriceService, PriceService>();
    builder.Services.AddScoped<IArticleService, ArticleService>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
        app.UseWebAssemblyDebugging();
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
}
finally 
{
    Serilog.Log.Information("======== Successful shutdown ========");
    Serilog.Log.CloseAndFlush();
}