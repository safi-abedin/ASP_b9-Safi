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
             .WriteTo.Email(new EmailConnectionInfo
             {
                 FromEmail = "SerilogIssue.com",
                 ToEmail = "Safiiitju47@gmail.com",
                 MailServer = "smtp.gmail.com",
                 NetworkCredentials = new NetworkCredential
                 {
                     UserName = "safiiitju47@gmail.com",
                     Password = "kzsr xfzu vpoj mbix"
                 },
                 EnableSsl = true,
                 Port = 465,
                 EmailSubject = "Assignment 2"
             },
             batchPostingLimit: 10
             , restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Fatal)
             .ReadFrom.Configuration(builder.Configuration)
             );


try
{
    Log.ForContext<Program>().Error("Test Number {Parm}", "1");

    Serilog.Debugging.SelfLog.Enable(Console.WriteLine);

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