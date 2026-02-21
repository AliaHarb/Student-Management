using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Student_Management.Models;
using Student_Management.Repository;

namespace Student_Management
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Option =>
            {
                Option.Password.RequiredLength = 4;
                Option.Password.RequireNonAlphanumeric = false;
            }
            ).AddEntityFrameworkStores<ApplicationContext>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>(); 

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
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            using (var scope = app.Services.CreateScope())
            {
                await RoleesSEeder.SeedRolesAsync(scope.ServiceProvider);
            }
            app.Run();
        }
    }
}
