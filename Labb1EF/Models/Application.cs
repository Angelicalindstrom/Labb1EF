using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb1EF.Models
{
    public class Application
    {
        [Key]
        public int ApplicationId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ApplicationDateStart { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ApplicationDateEnd { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SubmittDate { get; set; }

        [StringLength(100, ErrorMessage = "Message longer than 100 characters.")]
        public string? Message { get; set;}


        // FK 
        public int ReasonId { get; set; }
        public virtual Reason? Reason { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
