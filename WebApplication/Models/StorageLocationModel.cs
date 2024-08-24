using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class StorageLocationModel
    {
        [Key]
        [Required]
        [DisplayName("Branch Id")]
        public string location_id { get; set; }
        [Required]
        [DisplayName("Branch Name")]
        public string location_name { get; set; }
    }
}
