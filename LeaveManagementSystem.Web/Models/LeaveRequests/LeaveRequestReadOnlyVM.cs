using System.ComponentModel;

namespace LeaveManagementSystem.Web.Models.LeaveRequests
{
    public class LeaveRequestReadOnlyVM
    {
        public int Id { get; set; }

        [DisplayName("Start Date")]
        public DateOnly StartDate { get; set; }

        [DisplayName("End Date")]
        public DateOnly EndDate { get; set; }

        [DisplayName("Total Days")]
        public int NumberOfDays { get; set; }

        [DisplayName("LeaveType")]
        public string LeaveType { get; set; } = string.Empty;

        public LeaveRequestStatusEnum LeaveRequestStatus { get; set; }
    }
}
