using Microsoft.EntityFrameworkCore;
using Task4U.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Task4U
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("ConexaoPostgres");

            builder.Services.AddDbContext<TskDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                            .AddCookie(options =>
            {
                options.LoginPath = "/Usuarios/Login"; 
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true; 
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication(); // quem e você ?
            app.UseAuthorization(); // o que você pode fazer.

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
