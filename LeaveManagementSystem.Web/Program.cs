using LeaveManagementSystem.Web.Common;
using LeaveManagementSystem.Web.Models.LeaveRequests;
using LeaveManagementSystem.Web.Services.Email;
using LeaveManagementSystem.Web.Services.LeaveAllocations;
using LeaveManagementSystem.Web.Services.LeaveRequests;
using LeaveManagementSystem.Web.Services.LeaveTypeService;
using LeaveManagementSystem.Web.Services.Periods;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

namespace LeaveManagementSystem.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            //Add automapper using dependency injection container
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddHttpContextAccessor();

            //add service dependencies
            builder.Services.AddScoped<ILeaveTypesService, LeaveTypesService>();
            builder.Services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();
            builder.Services.AddScoped<ILeaveRequestsService, LeaveRequestsService>();
            builder.Services.AddScoped<IPeriodsService, PeriodsService>();
            builder.Services.AddTransient<IEmailSender, EmailSender>();

            //Serilog configuration
            builder.Host.UseSerilog((ctx, config) => config.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

            //Add policy for authorization
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminSupervisorOnly", policy =>
                {
                    policy.RequireRole(Roles.Administrator, Roles.Supervisor); //admin or supervisor

                    //Admin and supervisor
                    /*policy.RequireRole(Roles.Administrator);
                    policy.RequireRole(Roles.Supervisor);*/
                });
            });

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireNonAlphanumeric = true;
            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

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
    }
}
