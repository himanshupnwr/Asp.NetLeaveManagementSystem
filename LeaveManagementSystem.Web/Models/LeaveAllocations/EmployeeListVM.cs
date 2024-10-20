using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveAllocations
{
    public class EmployeeListVM
    {
        public string Id { get; set; } = string.Empty;

        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Email Address")]
        public string? Email { get; set; }
    }
}
