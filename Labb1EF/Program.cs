using Labb1EF.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Labb1EF
{
    public class Program // Program.cs Middleware
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("NitroStoreConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not fount.");
            
            builder.Services.AddDbContext<NitroStoreDbContext>(optionsAction: options =>
            options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.Run();
        }
    }
}
