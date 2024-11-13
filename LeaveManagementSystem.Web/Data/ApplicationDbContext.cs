using LeaveManagementSystem.Web.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

            /*builder.ApplyConfiguration(new LeaveRequestStatusConfiguration());
            builder.ApplyConfiguration(new IdentityRoleConfiguration());
            builder.ApplyConfiguration(new IdentityUserRoleConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());*/

            //This will scan the project assembly for the entity configurations and load them all instead of calling them individually
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocation { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<LeaveRequestStatus> leaveRequestStatuses { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
    }
}
