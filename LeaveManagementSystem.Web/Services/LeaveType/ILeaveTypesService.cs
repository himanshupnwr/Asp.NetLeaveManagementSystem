﻿using LeaveManagementSystem.Web.Models.LeaveTypes;

namespace LeaveManagementSystem.Web.Services.LeaveTypeService
{
    public interface ILeaveTypesService
    {
        Task<bool> CheckIfLeaveTypeAlreadyExists(string name);
        Task<bool> CheckIfLeaveTypeAlreadyExistsForEdit(LeaveTypeEditVM leaveTypeEditVM);
        Task Create(LeaveTypeCreateVM model);
        Task<bool> DaysExceedMaximum(int leaveTypeId, int days);
        Task Edit(LeaveTypeEditVM model);
        Task<T?> Get<T>(int id) where T : class;
        Task<List<LeaveTypeReadOnlyVM>> GetAllAsync();
        Task<bool> LeaveTypeExists(int id);
        Task Remove(int id);
    }
}