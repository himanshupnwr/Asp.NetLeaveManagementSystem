using AutoMapper;
using Azure.Core;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Services.LeaveRequests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Models.LeaveRequests
{
    public class LeaveRequestsService(IMapper _mapper, 
        UserManager<ApplicationUser> _userManager,
        IHttpContextAccessor _httpContextAccessor,
        ApplicationDbContext _dbContext) : ILeaveRequestsService
    {
        public async Task CancelLeaveRequest(int leaveRequestId)
        {
            var leaveRequest = await _dbContext.LeaveRequests.FindAsync(leaveRequestId);
            leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Canceled;

            var numberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber;
            var allocationToDeduct = await _dbContext.LeaveAllocation
                .FirstAsync(q => q.LeaveTypeId == leaveRequest.LeaveTypeId && q.EmployeeId == leaveRequest.EmployeeId);

            allocationToDeduct.Days = allocationToDeduct.Days + numberOfDays;

            _dbContext.SaveChangesAsync();
        }

        public async Task CreateLeaveRequests(LeaveRequestCreateVM request)
        {
            //map data to leave request data model
            var leaveRequest = _mapper.Map<LeaveRequest>(request);

            //get logged in employee id
            var user  = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
            leaveRequest.EmployeeId = user.Id;

            //set LeaveRequestStatusId to pending
            leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Pending;

            //save leave request
            _dbContext.LeaveRequests.Add(leaveRequest);
            

            //deduct allocation days based on request
            var currentDate = DateTime.UtcNow;
            var period = await _dbContext.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
            var numberOfDays = request.EndDate.DayNumber - request.StartDate.DayNumber;
            var allocationToDeduct = await _dbContext.LeaveAllocation
                .FirstAsync(q => q.LeaveTypeId == request.LeaveTypeId && 
                q.EmployeeId == user.Id &&
                q.PeriodId == period.Id);

            allocationToDeduct.Days = allocationToDeduct.Days - numberOfDays;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<EmployeeLeaveRequestListVM> AdminGetAllLeaveRequests()
        {
            var leaveRequests = await _dbContext.LeaveRequests
                .Include(q=>q.LeaveType)
                .ToListAsync();

            var leaveRequestsModels = leaveRequests.Select(q => new LeaveRequestReadOnlyVM
            {
                StartDate = q.StartDate,
                EndDate = q.EndDate,
                Id = q.Id,
                LeaveType = q.LeaveType.Name,
                LeaveRequestStatus = (LeaveRequestStatusEnum)q.LeaveRequestStatusId,
                NumberOfDays = q.EndDate.DayNumber - q.StartDate.DayNumber
            }).ToList();

            var model = new EmployeeLeaveRequestListVM
            {
                ApprovedRequests = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Approved),
                PendingRequests = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Pending),
                DeclinedRequests = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Declined),
                TotalRequests = leaveRequests.Count,
                LeaveRequests = leaveRequestsModels
            };

            return model;
        }

        public async Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequests()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);
            var leaveRequests = await _dbContext.LeaveRequests
                .Include(q => q.LeaveType)
                .Where(q => q.EmployeeId == user.Id)
                .ToListAsync();

            var model = leaveRequests.Select(q => new LeaveRequestReadOnlyVM
            {
                StartDate = q.StartDate,
                EndDate = q.EndDate,
                Id = q.Id,
                LeaveType = q.LeaveType.Name,
                LeaveRequestStatus = (LeaveRequestStatusEnum)q.LeaveRequestStatusId,
                NumberOfDays = q.EndDate.DayNumber - q.StartDate.DayNumber
            }).ToList();

            return model;
        }

        public async Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM leaveRequestCreate)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
            var currentDate = DateTime.UtcNow;
            var period = await _dbContext.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
            var numberOfDays = leaveRequestCreate.EndDate.DayNumber - leaveRequestCreate.StartDate.DayNumber;
            var allocationToDeduct = await _dbContext.LeaveAllocation
                .FirstAsync(q => q.LeaveTypeId == leaveRequestCreate.LeaveTypeId 
                && q.EmployeeId == user.Id
                && q.PeriodId == period.Id);

            return allocationToDeduct.Days < numberOfDays;
        }

        public async Task<ReviewLeaveRequestVM> ReviewLeaveRequest(int leaveRequestId)
        {
            var leaveRequest = await _dbContext.LeaveRequests
                .Include(q => q.LeaveType)
                .FirstAsync(q => q.Id == leaveRequestId);

            var user = await _userManager.FindByIdAsync(leaveRequest.EmployeeId);

            var model = new ReviewLeaveRequestVM
            {
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                Id = leaveRequest.Id,
                LeaveType = leaveRequest.LeaveType.Name,
                LeaveRequestStatus = (LeaveRequestStatusEnum)leaveRequest.LeaveRequestStatusId,
                NumberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber,
                ReviewComments = leaveRequest.RequestComments,
                Employee = new LeaveAllocations.EmployeeListVM
                {
                    Id = leaveRequest.EmployeeId,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                }
            };

            return model;
        }

        public async Task ReviewLeaveRequest(int leaveRequestId, bool approved)
        {
            var leaveRequest = await _dbContext.LeaveRequests.FindAsync(leaveRequestId);
            leaveRequest.LeaveRequestStatusId = approved ?
                (int)LeaveRequestStatusEnum.Approved : (int)LeaveRequestStatusEnum.Declined;

            var user = await _userManager.FindByIdAsync(leaveRequest.EmployeeId);

            leaveRequest.ReviewerId = user.Id;
            

            if(!approved)
            {
                var numberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber;
                var currentDate = DateTime.UtcNow;
                var period = await _dbContext.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
                var allocationToDeduct = await _dbContext.LeaveAllocation
                .FirstAsync(q => q.LeaveTypeId == leaveRequest.LeaveTypeId 
                && q.EmployeeId == leaveRequest.EmployeeId
                && q.PeriodId == period.Id);

                allocationToDeduct.Days = allocationToDeduct.Days + numberOfDays;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
