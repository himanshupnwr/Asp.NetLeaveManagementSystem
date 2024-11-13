using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LeaveManagementSystem.Web.Data.Configurations
{
    public class LeaveRequestStatusConfiguration : IEntityTypeConfiguration<LeaveRequestStatus>
    {
        public void Configure(EntityTypeBuilder<LeaveRequestStatus> builder)
        {
            builder.HasData(new LeaveRequestStatus
            {
                Id = 1,
                Status = "Pending"
            },
            new LeaveRequestStatus
            {
                Id = 2,
                Status = "Approved"
            },
            new LeaveRequestStatus
            {
                Id = 3,
                Status = "Declined"
            },
            new LeaveRequestStatus
            {
                Id = 4,
                Status = "Canceled"
            });
        }
    }
}
