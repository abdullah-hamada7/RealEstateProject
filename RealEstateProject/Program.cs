using BusinessLogicLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace RealEstateProject;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        //dependency injections register
        ServiceModules.Regsiter(builder.Services);

        //Db context class register
        builder.Services.AddDbContext<RealEstateContext>(x => x.UseSqlServer("Data Source=DESKTOP-ISE9P0J;Initial Catalog=RealEstate;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));

        //register session
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(20); // Session timeout period
                                                            // Add other session options here if needed
        });
        builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseSession();
        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
