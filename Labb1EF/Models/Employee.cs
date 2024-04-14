using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb1EF.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; } // anställningsdag
        public int Salary { get; set; }


        // FK
        public int RoleId { get; set; } 
        public virtual Role? Role { get; set; }  //Sluter kopplingen Mellan Role



        public virtual ICollection <Application>? Applications { get; set; } // länk FFRAMÅT (Application får FKEmployeeId)  mellan Employee och Application
    }
}
