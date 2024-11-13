using LeaveManagementSystem.Web.Models.LeaveRequests;

namespace LeaveManagementSystem.Web.Services.LeaveRequests
{
    public interface ILeaveRequestsService
    {
        Task<EmployeeLeaveRequestListVM> AdminGetAllLeaveRequests();

        Task CreateLeaveRequests(LeaveRequestCreateVM request);

        Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequests();

        Task CancelLeaveRequest(int leaveRequestId);
        Task<ReviewLeaveRequestVM> ReviewLeaveRequest(int leaveRequestId);

        Task ReviewLeaveRequest(int leaveRequestId, bool approved);

        Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model);
    }
}