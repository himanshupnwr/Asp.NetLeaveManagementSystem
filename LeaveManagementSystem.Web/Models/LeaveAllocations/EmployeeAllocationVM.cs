using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveAllocations
{
    public class EmployeeAllocationVM
    {
        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date Joined")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        public List<LeaveAllocationVM> LeaveAllocations { get; set; }

        public bool IsCompletedAllocation { get; set; }
    }
}
