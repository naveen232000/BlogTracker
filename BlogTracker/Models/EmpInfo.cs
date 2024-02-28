using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogTracker.Models
{
    [Table("EmpInfo")]
    public class EmpInfo
    {
        [Key]
        public string EmailId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfJoining { get; set; }
        public int PassCode { get; set; }
    }
}
