using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Authenticate_Models
{
    public class LoginModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}