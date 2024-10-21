using Application.DAL.UnitOfWork;
using Application.DAL;
using Application.BLL;
using Application.Shared;
using Application.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
namespace Application.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            /// Register AutoMapper, dll classes , Book, BookDto , mapping ( map based on properties name ) 
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
           
            
            /// Application Context 
            builder.Services.AddDbContext<BookHavenContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<BookHavenContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ILoanService, LoanService>();
            builder.Services.AddScoped<IPenaltyService, PenaltyService>();
            builder.Services.AddScoped<IUserService, UserService>();


            /// Adding Authorization Policies 
            builder.Services.AddAuthorization(
               options =>
                           {
                               options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                               options.AddPolicy("RequireAuthorRole", policy => policy.RequireRole("Author"));
                               options.AddPolicy("RequireMemberRole", policy => policy.RequireRole("Member"));
                           }
           );

           /// DI Registration
          

            var app = builder.Build();

            // middleware registrations...
            // adding it as first middleware to handle all exceptions 

            app.UseMiddleware<ExceptionHandler>();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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
