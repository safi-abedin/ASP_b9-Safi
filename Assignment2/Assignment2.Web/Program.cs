using Assignment2.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;
using System.Net;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((ctx, lc) => lc
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
             .Enrich.FromLogContext()
             .ReadFrom.Configuration(builder.Configuration)
           ) ;

/*{
    "Name": "Email",
        "Args": {
        "ConnectionInfo": {
            "FromEmail": "app@example.com",
            "ToEmail": "safiiitju47@gmail.com",
            "Port": "25",
            "MailServer": "localhost",
            "EnableSsl": false,
            "EmailSubject": "Exception in Serilog Log in Assignment2.Web"
          },
          "RestrictedToMinimumLevel": "Fatal",
          "OutputTemplate": "{Timestamp:yyyy-MM-dd HH:mm} [{Level}] {Message}{NewLine}{Exception}",
          "batchPostingLimit": 100
        }
}*/


try
{
    Log.ForContext<Program>().Error("Test Number {Parm}", "1");

    Serilog.Debugging.SelfLog.Enable(Console.WriteLine);

    Serilog.Debugging.SelfLog.Enable(Console.WriteLine);
    var emailInfo = new EmailConnectionInfo
    {
         FromEmail = "app@example.com",
            ToEmail= "safiiitju47@gmail.com",
            Port = 25,
            MailServer = "localhost",
            EnableSsl=false,
            EmailSubject= "Exception in Serilog Log in Assignment2.Web"
    };

    using (var logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Email(emailInfo)
            .CreateLogger())
    {
        for (var i = 1; i <= 100; i++)
        {
            logger.Information($"Log #{i}");
        }
    }

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.AddControllersWithViews();

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

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapRazorPages();

    app.Run();
}
catch(Exception ex)
{
    Log.Information(ex,"something went wrong");
}
finally
{
    Log.CloseAndFlush();
}