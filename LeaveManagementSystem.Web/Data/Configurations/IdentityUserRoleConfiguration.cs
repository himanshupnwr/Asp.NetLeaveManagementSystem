using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Web.Data.Configurations
{
    //need to specify key in the identity user role
    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            //assigning the role of admin to the user
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "67520f68-f21b-40b5-a298-43e115b819b6",
                    UserId = "4d3fa1bc-ffa1-4e69-982b-3dd4978452fe"
                }
            );
        }
    }
}
