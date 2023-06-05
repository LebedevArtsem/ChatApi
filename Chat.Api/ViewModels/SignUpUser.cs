using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels
{
    public class SignUpUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password),ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }
    }
}
