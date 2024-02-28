using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogTracker.Models
{
    [Table("AdminInfo")]
    public class AdminInfo
    {
        [Key]
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}
