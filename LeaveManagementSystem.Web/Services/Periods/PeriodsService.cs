
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.Periods
{
    public class PeriodsService(ApplicationDbContext _dbContext) : IPeriodsService
    {
        public async Task<Period> GetCurrentPeriod()
        {
            var currentDate = DateTime.Now;
            var period = await _dbContext.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
            return period;
        }
    }
}
