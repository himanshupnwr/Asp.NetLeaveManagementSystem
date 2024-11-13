using LeaveManagementSystem.Web.Models.LeaveAllocations;

namespace LeaveManagementSystem.Web.Models.LeaveRequests
{
    public class ReviewLeaveRequestVM : LeaveRequestReadOnlyVM
    {
        public EmployeeListVM Employee { get; set; } = new EmployeeListVM();
        public string ReviewComments { get; set; }
    }
}