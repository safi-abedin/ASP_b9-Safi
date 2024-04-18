using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using StackOverFlow.Application;
using StackOverFlow.Infrastructure;
using StackOverFlow.Web;
using System.Reflection; 
using Microsoft.AspNetCore.Builder;
using StackOverFlow.Infrastructure.Extensions;
using StackOverFlow.Infrastructure.Email;

var builder = WebApplication.CreateBuilder(args);


try
{
    Log.Information("Application Starting...");

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    var migrationAssembly = Assembly.GetExecutingAssembly().FullName;


    builder.Host.UseSerilog((ctx, lc) => lc
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(builder.Configuration));

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new ApplicationModule());
        containerBuilder.RegisterModule(new InfrastructureModule(connectionString,
            migrationAssembly));
        containerBuilder.RegisterModule(new WebModule());
    });

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString,
        (m) => m.MigrationsAssembly(migrationAssembly)));

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddControllersWithViews();
    builder.Services.AddIdentity();
    builder.Services.AddCookieAuthentication();

    builder.Services.AddAuthorization(options =>
    {

        options.AddPolicy("CreateQuestion", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("CreateQuestion", "true");
        });

        options.AddPolicy("CreateAnswer", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("CreateAnswer", "true");
        });


        options.AddPolicy("EditQuestion", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("EditQuestion", "true");
        });


        options.AddPolicy("DeleteQuestion", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("DeleteQuestion", "true");
        });

    });


    builder.Services.Configure<Smtp>(builder.Configuration.GetSection("Smtp")); 

    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection()
       .UseStaticFiles()
       .UseRouting()
       .UseAuthentication()
       .UseAuthorization()
       .UseSession();

    app.MapControllerRoute(
        name: "areas",
          pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
         );

    app.MapRazorPages();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
        );

    app.MapRazorPages();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Failed to start application.");
}
finally
{
    Log.CloseAndFlush();
}





