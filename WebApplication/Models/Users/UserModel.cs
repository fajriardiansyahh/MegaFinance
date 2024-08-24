using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Users
{
    public class UserModel
    {
        [Key]
        public int user_id { get; set; }
        [Required]
        [DisplayName("User Name")]
        public string user_name { get; set; }
        [Required]
        [DisplayName("Password")]
        public string password { get; set; }
        public bool is_active { get; set; } = true;
    }
}
