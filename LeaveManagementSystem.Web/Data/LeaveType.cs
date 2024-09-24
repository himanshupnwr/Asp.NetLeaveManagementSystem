using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.Web.Data
{
    public class LeaveType
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(150)]
        //[Column(TypeName="nvarchar(150)")] //set nvarchar size if we want
        public string Name { get; set; }

        public int NumberOfDays { get; set; }
    }
}
