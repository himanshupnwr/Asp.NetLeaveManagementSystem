using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveTypes
{
    public class LeaveTypeEditVM : BaseLeaveTypeVM
    {

        [Required]
        //[MaxLength(150)]
        [Length(4, 150, ErrorMessage = "You have violated the length requriement")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 90)]
        [Display(Name = "Number Of Days")]
        public int NumberOfDays { get; set; }
    }
}
