
using AutoMapper;
using LeaveManagementSystem.Web.Common;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveAllocations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations
{
    public class LeaveAllocationService(ApplicationDbContext _context, 
        IHttpContextAccessor _httpContextAccessor,
        UserManager<ApplicationUser> _userManager,
        IMapper _mapper) : ILeaveAllocationService
    {
        public async Task AllocateLeave(string employeeId)
        {
            // get all the leave types
            var leaveTypes = await _context.LeaveTypes
                .Where(q => !q.LeaveAllocations.Any(x=>x.EmployeeId == employeeId))
                .ToListAsync();

            // fet the current period based on the year
            var currentDate = DateTime.Now;

            //.First will now throw an exception if condition matches for more than one record
            //.Single will throw an exception if condition matches for more than one record
            var period = await _context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
            var monthsRemaining = period.EndDate.Month - currentDate.Month;

            // foreach leave type, create an allocation entry
            foreach (var leaveType in leaveTypes)
            {
                var accuralRate = decimal.Divide(leaveType.NumberOfDays, 12);
                var leaveAllocation = new LeaveAllocation
                {
                    EmployeeId = employeeId,
                    LeaveType = leaveType,
                    PeriodId = period.Id,
                    Days = (int)Math.Ceiling(accuralRate * monthsRemaining)
                };
                _context.Add(leaveAllocation);
                
            }
            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeAllocationVM> GetEmployeeAllocation(string? userId)
        {
            var user = string.IsNullOrEmpty(userId)
                ? await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User)
                : await _userManager.FindByIdAsync(userId);

            var allocations = await GetAllocation(user.Id);
            var allocationVmList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);
            //var leaveTypes = await _context.LeaveTypes.CountAsync();

            var employeeVM = new EmployeeAllocationVM
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                LeaveAllocations = allocationVmList,
                IsCompletedAllocation = allocationVmList.Count() > 0 ? true : false
            };

            return employeeVM;
        }

        public async Task<List<EmployeeListVM>> GetEmployees()
        {
            var users = await _userManager.GetUsersInRoleAsync(Roles.Employee);
            List<EmployeeListVM> employeeList = new List<EmployeeListVM>();

            foreach (var user in users)
            {
                var employee = new EmployeeListVM
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };
                employeeList.Add(employee);
            }

            return employeeList;
        }

        public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId)
        {
            var allocation = await _context.LeaveAllocation
                .Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .FirstOrDefaultAsync(q => q.Id  == allocationId);

            var model = _mapper.Map<LeaveAllocationEditVM>(allocation);
            return model;
        }


        public async Task EditAllocation(LeaveAllocationEditVM allocationEditVM)
        {
            /* var leaveAllocation = await GetEmployeeAllocation(allocationEditVM.Id);
            if (leaveAllocation == null)
            {
                throw new Exception("Leave allocation record does not exist");
            }
            leaveAllocation.Days = allocationEditVM.Days;*/

            //option 1 - this updates all entries whether they are updated or noe
            //_context.Update(leaveAllocation);

            //option 2 this updates only the value that is changed
            //_context.Entry(leaveAllocation).State = EntityState.Modified;

            //await _context.SaveChangesAsync();

            await _context.LeaveAllocation
                .Where(q => q.Id == allocationEditVM.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(e => e.Days, allocationEditVM.Days));
        }

        private async Task<List<LeaveAllocation>> GetAllocation(string? userId)
        {
            string employeeId = string.Empty;
            if (string.IsNullOrEmpty(userId))
            {
                employeeId = userId;
            }
            else
            {
                //get user info from httpcontext
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
                employeeId = user.Id;
            }

            var currentDate = DateTime.Now;

            return await _context.LeaveAllocation
                .Include(l => l.LeaveType)
                .Include(p => p.Period)
                .Where(la => la.EmployeeId == employeeId && la.Period.EndDate.Year == currentDate.Year)
                .ToListAsync();
        }

        private async Task<bool> AllocationExists(string userId, int periodId, int leaveTypeId)
        {
            return await _context.LeaveAllocation.AnyAsync(q =>
            q.EmployeeId == userId && q.PeriodId == periodId && q.LeaveTypeId == leaveTypeId);
        }
    }
}
