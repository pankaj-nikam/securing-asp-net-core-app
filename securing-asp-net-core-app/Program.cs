using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using securing_asp_net_core_app.Data;
using securing_asp_net_core_app.Models;

namespace securing_asp_net_core_app
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                            .AddRoles<IdentityRole>() //NOTE: 1A - Adding the roles for identity services.
                            .AddEntityFrameworkStores<ApplicationDbContext>();
            
            builder.Services.AddControllersWithViews();

            //TODO: 3A - Don't add this
            //builder.Services.AddDirectoryBrowser();

            //NOTE: 4A - Rate limiting - add here | https://learn.microsoft.com/en-us/aspnet/core/performance/rate-limit?view=aspnetcore-7.0
            builder.Services.AddRateLimiter(_ => _
                            .AddFixedWindowLimiter(policyName: "fixed", options =>
                            {
                                options.PermitLimit = 4;
                                options.Window = TimeSpan.FromSeconds(1);
                                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                                options.QueueLimit = 2;
                            }));

            var app = builder.Build();

            //NTOE: 1B - Adding the roles required for the app.
            using (var scope = app.Services.CreateScope())
            {
                var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (await roleManger.RoleExistsAsync(Constants.ADMIN_ROLE_NAME).ConfigureAwait(false) == false)
                {
                    await roleManger.CreateAsync(new IdentityRole(Constants.ADMIN_ROLE_NAME)).ConfigureAwait(false);
                }
                if (await roleManger.RoleExistsAsync(Constants.EMPLOYEE_ROLE_NAME).ConfigureAwait(false) == false)
                {
                    await roleManger.CreateAsync(new IdentityRole(Constants.EMPLOYEE_ROLE_NAME)).ConfigureAwait(false);
                }
            }

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

            //TODO: 3B - and this
            //app.UseDirectoryBrowser();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}