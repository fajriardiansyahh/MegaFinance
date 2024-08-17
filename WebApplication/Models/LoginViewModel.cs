using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class LoginViewModel
    {
        [Display(Name = "User Login")]
        [Required]
        public string User_name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool? Remember_me { get; set; }
    }
}
