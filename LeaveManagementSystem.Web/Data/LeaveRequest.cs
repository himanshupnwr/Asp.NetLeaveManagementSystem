using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.Web.Data
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public LeaveType? LeaveType { get; set; }
        [ForeignKey("LeaveTypeId")]
        public int LeaveTypeId { get; set; }

        public LeaveRequestStatus? LeaveRequestStatus { get; set; }
        [ForeignKey("LeaveRequestStatusId")]
        public int LeaveRequestStatusId { get; set; }

        public ApplicationUser? Employee { get; set; }
        public string? EmployeeId { get; set; }

        public ApplicationUser? Reviewer { get; set; }
        public string? ReviewerId { get; set; }

        public string? RequestComments { get; set; }
    }
}