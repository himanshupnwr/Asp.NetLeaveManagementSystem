using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveRequests;
using LeaveManagementSystem.Web.Services.LeaveRequests;
using LeaveManagementSystem.Web.Services.LeaveTypeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize]
    public class LeaveRequestsController(ILeaveTypesService _leaveTypesService,
        ILeaveRequestsService _leaveRequestsService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = await _leaveRequestsService.GetEmployeeLeaveRequests();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var leaveTypes = await _leaveTypesService.GetAllAsync();
            var leaveTypesList = new SelectList(leaveTypes, "Id", "Name");
            var model = new LeaveRequestCreateVM
            {
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now),
                LeaveTypes = leaveTypesList,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestCreateVM leaveRequest)
        {
            //validate that the days don't exceed the allocation
            if(await _leaveRequestsService.RequestDatesExceedAllocation(leaveRequest))
            {
                ModelState.AddModelError(nameof(leaveRequest.EndDate), "The number of days requested is invalid");
            }

            if(ModelState.IsValid)
            {
                await _leaveRequestsService.CreateLeaveRequests(leaveRequest);
                return RedirectToAction(nameof(Index));
            }

            var leaveTypes = await _leaveTypesService.GetAllAsync();
            leaveRequest.LeaveTypes = new SelectList(leaveTypes, "Id", "Name");
            return View(leaveRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int leaveRequestId)
        {
            await _leaveRequestsService.CancelLeaveRequest(leaveRequestId);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "AdminSupervisorOnly")]
        public async Task<IActionResult> ListRequests()
        {
            var model = await _leaveRequestsService.AdminGetAllLeaveRequests();
            return View(model);
        }

        
        public async Task<IActionResult> Review(int Id)
        {
            var model = await _leaveRequestsService.ReviewLeaveRequest(Id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Review(int id, bool approved)
        {
            await _leaveRequestsService.ReviewLeaveRequest(id, approved);
            return RedirectToAction(nameof(ListRequests));
        }
    }
}
