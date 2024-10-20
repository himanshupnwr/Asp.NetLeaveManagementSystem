using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.Web.Data
{
    public class LeaveType
    {
        public int Id { get; set; }

        [MaxLength(150)]
        //[Column(TypeName="nvarchar(150)")] //set nvarchar size if we want
        public string Name { get; set; }

        public int NumberOfDays { get; set; }

        public List<LeaveAllocation>? LeaveAllocations { get; set; }
    }
}
