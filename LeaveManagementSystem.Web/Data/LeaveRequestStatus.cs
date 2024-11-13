using LeaveManagementSystem.Web.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Data
{
    //instead of gloablly in dbcontext file we can also define the configuration for the class using the attributes
    //[EntityTypeConfiguration(typeof(LeaveRequestStatusConfiguration))]
    public class LeaveRequestStatus
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Status { get; set; }
    }
}