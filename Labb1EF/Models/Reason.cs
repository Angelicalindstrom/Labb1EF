using System.ComponentModel.DataAnnotations;

namespace Labb1EF.Models
{
    public class Reason
    {
        [Key]
        public int ReasonId { get; set; }
        public string ReasonTitle { get; set;}


        public ICollection <Application>? Applications { get; set; } // länk FFRAMÅT (Application får FKReasonId)  mellan Reason och Application
    }
}