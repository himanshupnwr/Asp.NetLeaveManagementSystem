using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.Web.Data
{
    public class LeaveAllocation
    {
        public int Id { get; set; }
        [ForeignKey("LeaveTypeId")]
        public LeaveType? LeaveType { get; set; }
        public int LeaveTypeId { get; set; }

        [ForeignKey("EmployeeId")]
        public ApplicationUser? Employee { get; set; }
        public required string EmployeeId { get; set; }

        [ForeignKey("PeriodId")]
        public Period? Period { get; set; }
        public int PeriodId { get; set; }

        public int Days { get; set; }
    }
}
