using System.ComponentModel.DataAnnotations;

namespace PermissionApi.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Email is not valid.")]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Minimum password length is 6.")]
        public string Password { get; set; }
    }
}
