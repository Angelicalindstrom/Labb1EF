using System.ComponentModel.DataAnnotations;

namespace Labb1EF.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleTitle { get; set; }



        // kan spara när jag tar bort denna=?
        public virtual ICollection <Employee>? Employees { get; set; } // länk FFRAMÅT (Employee får FKRoleId)  mellan Role och Employees
    }
}
