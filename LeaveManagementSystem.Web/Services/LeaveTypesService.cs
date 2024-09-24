using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services
{
    public class LeaveTypesService(ApplicationDbContext context, IMapper mapper) : ILeaveTypesService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<LeaveTypeReadOnlyVM>> GetAllAsync()
        {
            var data = await _context.LeaveTypes.ToListAsync();
            //convert the datamodel into a view model
            //Custom mapper
            /*var viewData = data.Select(q => new IndexVM
            {
                Id = q.Id,
                Name = q.TypeName,
                NumberOfDays = q.NumberOfDays
            });*/

            //convert the data model into a view model
            var viewData = _mapper.Map<List<LeaveTypeReadOnlyVM>>(data);
            return viewData;
        }

        public async Task Remove(int id)
        {
            var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                _context.LeaveTypes.Remove(data);
                _context.SaveChanges();
            }
        }

        public async Task<T?> Get<T>(int id) where T : class
        {
            var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
            {
                return null;
            }
            return _mapper.Map<T>(data);
        }

        public async Task Edit(LeaveTypeEditVM model)
        {
            var leaveType = _mapper.Map<LeaveType>(model);
            _context.Update(leaveType);
            await _context.SaveChangesAsync();
        }

        public async Task Create(LeaveTypeCreateVM model)
        {
            var leaveType = _mapper.Map<LeaveType>(model);
            _context.Add(leaveType);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> LeaveTypeExists(int id)
        {
            return await _context.LeaveTypes.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> CheckIfLeaveTypeAlreadyExists(string name)
        {
            return await _context.LeaveTypes.AnyAsync(m => m.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }


        public async Task<bool> CheckIfLeaveTypeAlreadyExistsForEdit(LeaveTypeEditVM leaveTypeEditVM)
        {
            return await _context.LeaveTypes.AnyAsync(m => m.Name.Equals(leaveTypeEditVM.Name,
                StringComparison.InvariantCultureIgnoreCase) && m.Id != leaveTypeEditVM.Id);
        }
    }
}
