using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Config;
using Server.Data;
using System.Runtime.CompilerServices;

var seed = args.Contains("/seed");
if (seed)
{
    args = args.Except(new[] { "/seed" }).ToArray();
}

var builder = WebApplication.CreateBuilder(args);
var defaultConnString = builder.Configuration.GetConnectionString("DefaultConnection");

if (seed)
{
    SeedData.EnsureSeedData(defaultConnString);
}

var assembly = typeof(Program).Assembly.GetName().Name;


builder.Services.AddDbContext<AspNetIdentityDbContext>(options =>
{
    options.UseSqlServer(defaultConnString,
        b => b.MigrationsAssembly(assembly));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
       .AddEntityFrameworkStores<AspNetIdentityDbContext>();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(opt =>
        {
            opt.ConfigureDbContext = b =>
            b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assembly));
        }).AddOperationalStore(opt =>
        {
            opt.ConfigureDbContext = b =>
            b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assembly));
        })
        .AddDeveloperSigningCredential();

builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.UseEndpoints(opt =>
{
    opt.MapDefaultControllerRoute();
});

app.Run();
