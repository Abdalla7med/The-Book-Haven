using Application.DAL;
using Application.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Web
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            /// Application Context 
            builder.Services.AddDbContext<BookHavenContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            /// Identity Context
            builder.Services.AddDbContext<IdentityContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            var app = builder.Build();

            /// adding seed rules 
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = app.Services.GetRequiredService<IConfiguration>(); 
                await SeedRolesAndUsers(services, configuration);
            }

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


        public static async Task SeedRolesAndUsers(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Admin", "Librarian", "Member" };

            /// adding seed roles 
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            /// Secure Admin Data 
            var email = configuration["Admin:Email"];
            var pass = configuration["Admin:Password"];
            var userName = configuration["Admin:userName"];
            var FName = configuration["Admin:FName"];
            var LName = configuration["Admin:LName"];


            // Create default admin user
            var adminUser = await userManager.FindByEmailAsync(email);
            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName =userName ,
                    Email = email,
                    FName = FName,
                    LName = LName
                };

                var createUser = await userManager.CreateAsync(user, pass);

                if (createUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin"); /// adding user to identity with admin role
                }
            }
        }
    }
}
