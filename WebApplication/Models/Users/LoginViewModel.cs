using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Users
{
    public class LoginViewModel
    {
        [Display(Name = "User Login")]
        [Required]
        public string user_name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public bool? Remember_me { get; set; }
    }
}
