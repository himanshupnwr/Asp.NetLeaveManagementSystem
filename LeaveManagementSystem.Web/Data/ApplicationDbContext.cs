using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { 
                    Id = "3c07dac8-1279-4790-ab57-da007eeb91cb",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
                new IdentityRole {
                    Id = "1cf5cd75-5863-47dc-9a97-c5e5245e60b8",
                    Name = "Supervisor",
                    NormalizedName = "SUPERVISOR"
                },
                new IdentityRole {
                    Id = "67520f68-f21b-40b5-a298-43e115b819b6",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                });

            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = "4d3fa1bc-ffa1-4e69-982b-3dd4978452fe",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                UserName = "admin@localhost.com",
                PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                EmailConfirmed = true,
                FirstName = "Default",
                LastName = "Admin",
                DateOfBirth = new DateOnly(19, 12, 01)
            });

            //assigning the role of admin to the user
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "67520f68-f21b-40b5-a298-43e115b819b6",
                    UserId = "4d3fa1bc-ffa1-4e69-982b-3dd4978452fe"
                }
            );
        }

        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocation { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<LeaveRequestStatus> leaveRequestStatuses { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
    }
}
